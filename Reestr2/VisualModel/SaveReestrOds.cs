using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using Reestr2.Model;

namespace Reestr2.VisualModel
{
    public static class SaveReestrOds
    {
        public static int GetRowNumberForInsert(DataTable table,string[] headers)
        {
            for (int row = 1; row < table.Rows.Count; row++)
            {
                var cells = table.Rows[row].ItemArray.Select(a=>a==DBNull.Value?string.Empty:a.ToString()).ToArray();
                if (headers.All(a => cells.Contains(a))) return row;
            }
            return -1;
        }
        public static int[] OrdersOfHeaders(string[] headers,string[] unOrderHeader)=>headers.Select(header => Array.IndexOf(unOrderHeader, header)).ToArray();
        public static void SaveReestr(DataSet shablon,VisualReestr reestr)
        {
            var sheet = shablon.Tables[0];
            var headers=reestr.Lines[0].GetType().GetProperties()
                .Where(p => p.GetCustomAttribute<DisplayAttribute>() != null)
                .Select(p => $"#{p.GetCustomAttribute<DisplayAttribute>().Name.ToUpper()}").ToArray();
            var rowIndex = GetRowNumberForInsert(sheet, headers);
            var orderOfHeaders = OrdersOfHeaders(sheet.Rows[rowIndex].ItemArray.Cast<string>().ToArray(), headers);
            for(int row = reestr.Lines.Count - 1; row >= 0; row--)
            {
                var values = reestr.Lines[row].GetArray();
                var newRow = sheet.NewRow();
                newRow.ItemArray= Enumerable.Range(0, sheet.Columns.Count-1).Select(a => values[a]).ToArray();
                sheet.Rows.InsertAt(newRow, rowIndex);
            }
            new OdsReaderWriter().WriteOdsFile(shablon, @"d:\1.ods");
        }

    }
}
