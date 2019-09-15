using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidTest.Controls;
using System.IO;
using ExcelLibrary.SpreadSheet;
using SolidTest.Data;

namespace SolidTest.Controls
{
    public class ExcelGenerator
    {
        string _date;
        string _path;
        Workbook _wb;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date">format dd.MM.yyyy</param>
        public ExcelGenerator(DateTime date)
        {
            _date = date.ToString("yyyyMMdd");
            _path = AppDomain.CurrentDomain.BaseDirectory + _date + ".xls";
            CheckFileExists();
            _wb = new Workbook();
        }
        void CheckFileExists()
        {
            if (File.Exists(_path))
                try
                {
                    File.Delete(_path);
                }
                catch { }
            
        }
        public void GenerateExcelFile(CBRXMLParser data)
        {
            Task<Worksheet>[] task = new Task<Worksheet>[data.Rates.Count + 1];
            List<Worksheet> list = new List<Worksheet>();
            int i = 0;
            task[i] = GenerateWorkSheetAsync("RUR",  data);
            foreach (CBRRate rate in data.Rates)
            {
                i++;
                task[i] = GenerateWorkSheetAsync(rate.CharCode,  data);
            }
            Task.WaitAll(task);
            foreach (var item in task)
            {
                list.Add(item.Result);
            }

            SaveFile(list);
        }



        async Task<Worksheet> GenerateWorkSheetAsync(string charCode, CBRXMLParser data)
        {
            Worksheet ws = new Worksheet(charCode);
            ws.Cells[0, 0] = new Cell("КОД");
            ws.Cells[0, 1] = new Cell("Наименование");
            ws.Cells[0, 2] = new Cell("Номинал");
            ws.Cells[0, 3] = new Cell("Стоимость");
            return await Task<Worksheet>.Run(() =>
            {
                int i = 1;
                CBRRate rur = data.Rates.Where(c => c.CharCode == charCode).FirstOrDefault();
                if (rur != null)
                {
                    ws.Cells[i, 0] = new Cell("RUR");
                    ws.Cells[i, 1] = new Cell("Российский Рубль");
                    ws.Cells[i, 2] = new Cell(rur.Nominal);
                    ws.Cells[i, 3] = new Cell(1/rur.Value);
                    i++;
                }
                foreach (CBRRate rate in data.Rates)
                {
                    if (rate.CharCode == charCode)
                        continue;
                    ws.Cells[i, 0] = new Cell(rate.CharCode);
                    ws.Cells[i, 1] = new Cell(rate.Name);
                    ws.Cells[i, 2] = new Cell(rate.Nominal);
                    ws.Cells[i, 3] = new Cell((charCode == "RUR") ?  rate.Value : rur.Value / rate.Value);
                    i++;
                }
                return ws;
            });

        }

        void SaveFile(List<Worksheet> ws)
        {
            _wb.Worksheets.AddRange(ws);
            _wb.Save(_path);
        }
        

    }
}
