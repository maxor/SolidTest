using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidTest.Data;
using System.IO;
using ExcelLibrary.SpreadSheet;

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
        public ExcelGenerator(string date)
        {
            string[] split = date.Split('.');
            _date = split[2] + split[1] + split[0];
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
            Task<Worksheet>[] task = new Task<Worksheet>[data.Rates.Count];
            List<Worksheet> list = new List<Worksheet>();
            int i = 0;
            foreach (CBRRate rate in data.Rates)
            {
                task[i] = GenerateWorkSheetAsync(rate.ChaeCode, rate.Value, data);
                list.Add(task[i].Result);
                i++;
            }
            Task.WaitAll(task);
            SaveFile(list);
        }



        async Task<Worksheet> GenerateWorkSheetAsync(string charCode, double value, CBRXMLParser data)
        {
            Worksheet ws = new Worksheet(charCode);
            ws.Cells[0, 0] = new Cell("charCode");
            ws.Cells[0, 1] = new Cell("name");
            ws.Cells[0, 2] = new Cell("value");
            return await Task<Worksheet>.Run(() =>
            {
                int i = 1;
                foreach (CBRRate rate in data.Rates)
                {
                    ws.Cells[i, 0] = new Cell(rate.ChaeCode);
                    ws.Cells[i, 1] = new Cell(rate.Name);
                    ws.Cells[i, 2] = new Cell(value/rate.Value);
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
