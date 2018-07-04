using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuntimeDemo
{
   public class WriteExcel
    {
        public static void WriteExcelFile(List<Dictionary<string,string>> inputData)
        {
            using(var package = new ExcelPackage())
            {
               // var table = ToDataTable(inputData);
                var sheet = package.Workbook.Worksheets.Add("Test_Runner_Output");
                var columns = inputData.FirstOrDefault().Select(e => e.Key).ToList();


                for(int i=0; i < columns.Count; i++)
                {
                    sheet.Cells[i + 1, 1].Value = columns[i];
                }

                var rows = columns.Count;
                for (int i = 0; i < inputData.Count; i++)
                {
                    for (int j = 0; j < rows; j++)
                    {
                        sheet.Cells[j + 1, i + 2].Value = inputData[i].Values.ElementAt(j);
                        sheet.Cells[j + 1, i + 2].Style.Fill.PatternType = ExcelFillStyle.None;
                        sheet.Cells[j + 1, i + 2].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
                    }
                }

                FileInfo file = new FileInfo(@"D:\\Output.xlsx");
                package.SaveAs(file);
            }
        }


        public static void WriteExcelFile(List<List<ExcelOutputData>> outputData)
        {
            using (var package = new ExcelPackage())
            {
                // var table = ToDataTable(inputData);
                var sheet = package.Workbook.Worksheets.Add("Test_Runner_Output");
                var propertyNames = outputData.FirstOrDefault().Select(e => e.Name).ToList();

                for (int i = 0; i < propertyNames.Count; i++)
                {
                    sheet.Cells[i + 3, 1].Value = propertyNames[i];
                }

                var rows = propertyNames.Count;


                for(int i = 0; i < outputData.Count; i++)
                {
                    sheet.Cells[1, (i + 1) * 2].Value = $"Test {i + 1}";
                    sheet.Cells[2, (i + 1) * 2].Value = "Actual Value";
                    sheet.Cells[2, ((i + 1) * 2) + 1].Value = "Expected Value";
                    for (int j = 0; j < rows; j++)
                    {
                        sheet.Cells[j + 3, (i + 1) * 2].Value = outputData[i][j].ActualValue;
                        sheet.Cells[j + 3, ((i + 1) * 2) + 1].Value = outputData[i][j].ExpectedValue;
                    }

                }
                FileInfo file = new FileInfo(@"D:\\Output.xlsx");
                package.SaveAs(file);
            }
        }

        static DataTable ToDataTable(List<Dictionary<string, string>> list)
        {
            DataTable result = new DataTable();
            if (list.Count == 0)
                return result;

            var columnNames = list.SelectMany(dict => dict.Keys).Distinct();
            result.Columns.AddRange(columnNames.Select(c => new DataColumn(c)).ToArray());
            foreach (Dictionary<string, string> item in list)
            {
                var row = result.NewRow();
                foreach (var key in item.Keys)
                {
                    row[key] = item[key];
                }

                result.Rows.Add(row);
            }

            return result;
        }


    }
}
