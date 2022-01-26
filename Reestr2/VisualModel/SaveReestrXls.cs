using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Reestr2.VisualModel
{
    public class SaveReestrXls
    {
        public static string GetNameReestr(VisualReestr reestr) => $"Реестр {reestr.MoCod}.{reestr.Month} счета№{reestr.NChet} от {reestr.DChet?.ToString("d")??""} на {reestr.MnAll}.xls";
        
        //Получить текст ячейки экселя по координатам
        public static string GetCellValue(int rowIndex, int columnIndex, Excel.Worksheet _workSheet)
        {
            string cellValue = "";

            Excel.Range cellRange = (Excel.Range)_workSheet.Cells[rowIndex, columnIndex];
            if (cellRange.Value != null)
            {
                cellValue = cellRange.Value.ToString();
            }
            return cellValue;
        }
        //Получить строку экселя по номеру как массив строк
        public static string[] GetRowAsArray(int rowIndex, Excel.Worksheet _workSheet)
        {
            return Enumerable.Range(1, _workSheet.UsedRange.Columns.Count - 1)
                .Select(columnIndex => GetCellValue(rowIndex, columnIndex, _workSheet))
                .ToArray();
        }
        //Выводит строку в которой есть все элементы из массива заголовка(hraders)
        public static int GetRowNumberForInsert(Excel.Worksheet _workSheet, string[] headers)
        {
            for (int row = 1; row < _workSheet.UsedRange.Rows.Count; row++)
            {
                var cells = GetRowAsArray(row, _workSheet);
                if (headers.All(a => cells.Contains(a))) return row;
            }
            return -1;
        }
        //Выводит массив порядка вывода заголовков (ищет в какой колонке элемты массива заголовков находится и выводит его)
        //Нужно при несовпадении порядка заголовков в шаблоне экселя и в классе вывода
        public static int[] GetOrderHeaders(Excel.Worksheet _workSheet, string[] headers)
        {
            var cells = GetRowAsArray(GetRowNumberForInsert(_workSheet, headers), _workSheet);
            return headers.Select(a => Array.IndexOf(cells, a)).ToArray();
        }
        //Вставляет строку в заданную позицию со спуском строк вниз. Вставляется копия текщей строки
        public static void InsertRow(int rowIndex, Excel.Worksheet _workSheet)
        {
            _workSheet.Rows[rowIndex].Insert(Excel.XlInsertShiftDirection.xlShiftDown, _workSheet.Rows[rowIndex].Copy(Type.Missing));
        }
        //Заполняет строку в экселе из массива значений в соответствии с массивом порядка заголовков
        public static void FillRow(string[] values, int[] orders, int RowIndex, Excel.Worksheet _workSheet)
        {
            for (int i = 0; i < values.Length; i++)
                _workSheet.Cells[RowIndex, orders[i] + 1].Value = values[i];
        }
        //Заполняет строку пыстыми значениями (очищает)
        public static void FillRowEmptyValues(int[] orders, int RowIndex, Excel.Worksheet _workSheet)
        {
            FillRow(Enumerable.Range(1, orders.Length).Select(a => "").ToArray(), orders, RowIndex, _workSheet);
        }
        //Ищет и заменяет все значения
        public static void ReplaceTextInExcelFile(string oldVal, string newVal, Excel.Worksheet _workSheet)
        {
            object m = Type.Missing;

            Excel.Range range = _workSheet.UsedRange;

            bool success = (bool)range.Replace(
                oldVal,
                newVal,
                Excel.XlLookAt.xlPart,
                Excel.XlSearchOrder.xlByRows,
                true, m, m, m);

        }
        //Заменяет в экселе знячения из ключей словаря на значения
        public static void FillAttrsReestr(Dictionary<string, string> attrs, Excel.Worksheet _workSheet)
        {
            foreach (var attr in attrs) ReplaceTextInExcelFile(attr.Key, attr.Value, _workSheet);
        }
        //Вставить и заполнить значениями строку
        public static void InsertAndFillRow(string[] values, int[] orders, int RowIndex, Excel.Worksheet _workSheet)
        {
            InsertRow(RowIndex, _workSheet);
            FillRow(values, orders, RowIndex, _workSheet);
        }
        //Выгружает реестр в шаблон и сохраняет его с другим именем
        public static List<string> SaveReestr(VisualReestr reestr, FileInfo fileshablon,string newfilename)
        {
            List<string> errors = new List<string>();
            Excel.Application ExApp = new Excel.Application();
            ExApp.DisplayAlerts = false;
            Excel.Workbook ExWB;
            Excel.Worksheet _workSheet;

            var headers = reestr.Lines[0].GetHeader();
            var attrs = reestr.GetAttrReestr();

            try
            {
                ExWB = ExApp.Workbooks.Open(fileshablon.FullName);
                _workSheet = (Excel.Worksheet)ExApp.Worksheets.get_Item(1);
                var RowNumberForInsert = GetRowNumberForInsert(_workSheet, headers);
                var orderHeaders = GetOrderHeaders(_workSheet, headers);
                //Сортировка в обратном порядке чтобы вставлять строки выше а не ниже
                var dataSource = reestr.Lines.OrderByDescending(a => a.Npp).ToList();
                FillAttrsReestr(attrs, _workSheet);


                FillRow(dataSource[0].GetArray(), orderHeaders, RowNumberForInsert, _workSheet);

                for (int i = 1; i < dataSource.Count; i++)
                {
                    InsertAndFillRow(dataSource[i].GetArray(), orderHeaders, RowNumberForInsert, _workSheet);
                }
                ExWB.SaveAs(newfilename);
            }

            catch (Exception e) { errors.Add($"Неудалось выгрузить реестр {reestr.MoName} за {reestr.Month} на сумму {reestr.MnAll}. Ошибка {e.Message}"); }
            finally
            {
                ExApp.Quit();
                ExApp = null;
                ExWB = null;
                _workSheet = null;
                GC.Collect();
            }
            return errors;
        }
    }
}
