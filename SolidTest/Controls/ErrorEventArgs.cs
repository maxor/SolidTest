using System;
using System.Collections.Generic;

namespace SolidTest.Controls
{
    public class ErrorEventArgs : EventArgs
    {
        Dictionary<int, string> Errors = new Dictionary<int, string>();
        
        public ErrorEventArgs(int ErrorNum)
        {
            FillDictionary();
            Message = GetError(ErrorNum);
        }

        public string Message { get; private set; }

        void FillDictionary()
        {
            Errors.Add(-1, "Общая ошибка приложения");
            Errors.Add(0, "Не удалось получить данные справочника валют с сайта CBR.ru");
            Errors.Add(1, "Не удалось получить данные котировок валют с сайта CBR.ru");
            Errors.Add(2, "Не удалось обработать XML с справочником валют");
            Errors.Add(3, "Не удалось обработать XML с котировками валют");
            Errors.Add(4, "Не удалось обновить базу данных с справочником валют");
            Errors.Add(5, "Не удалось обновить базу данных с котировками валют");
            Errors.Add(6, "Не удалось подключиться к сайту CBR.ru");
            Errors.Add(7, "Не удалось обработать ответ от CBR в виде XML");
            Errors.Add(8, "Не удалось сгенерировать ExcelFile");
        }

        string GetError(int error)
        {
            return Errors[error];
        }
    }

}