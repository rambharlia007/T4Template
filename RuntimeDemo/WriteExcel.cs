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
        public static void WriteExcelFile(Excel excel)
        {
            using (var package = new ExcelPackage())
            {
                var summary = package.Workbook.Worksheets.Add("Test_Runner_Summary");
                PrefillSummarySheet(summary, excel.SummaryData);

                var inputSheet = package.Workbook.Worksheets.Add("Test_Runner_Input");
                PrefillInputSheet(inputSheet, excel.InputData);

                var outputSheet = package.Workbook.Worksheets.Add("Test_Runner_Output");
                PrefillOutputSheet(outputSheet, excel.OutputData);

                FileInfo file = new FileInfo(@"D:\\Output.xlsx");
                package.SaveAs(file);
            }
        }

        private static void PrefillSummarySheet(ExcelWorksheet summary, SummaryData summaryData)
        {
            summary.Cells[1, 1].Value = "Date";
            summary.Cells[2, 1].Value = "Time";
            summary.Cells[3, 1].Value = "Regulation";
            summary.Cells[4, 1].Value = "Sequence Number";
            summary.Cells[7, 1].Value = "Overall test result";
            summary.Cells[9, 1].Value = "Total number of tests";
            summary.Cells[10, 1].Value = "Number of tests passing";
            summary.Cells[11, 1].Value = "Number of tests failing";

            summary.Cells[1, 2].Value = summaryData.Date;
            summary.Cells[2, 2].Value = summaryData.Time;
            summary.Cells[3, 2].Value = summaryData.Regulation;
            summary.Cells[4, 2].Value = summaryData.Sequence;
            summary.Cells[7, 2].Value = summaryData.Result;
            summary.Cells[9, 2].Value = summaryData.Total;
            summary.Cells[10, 2].Value = summaryData.Passed;
            summary.Cells[11, 2].Value = summaryData.Failed;
        }

        private static void PrefillOutputSheet(ExcelWorksheet outputSheet, List<List<ExcelOutputData>> outputData)
        {
            var propertyNames = outputData.FirstOrDefault().Select(e => e.Name).ToList();
            var rows = propertyNames.Count;
            var emptyRowCount = 3;
            var firstRow = 1;
            var secoundRow = 2;
            for (int i = 0; i < propertyNames.Count; i++)
            {
                outputSheet.Cells[i + emptyRowCount, 1].Value = propertyNames[i];
            }

            // First two rows are reserved for header value. 1st row consists of test{index}m value
            // secound row consist of Expected value and Actual value header
            for (int i = 0; i < outputData.Count; i++)
            {
                var expectedColumn = (i + 1) * 2;
                var actualColumn = ((i + 1) * 2) + 1;
                outputSheet.Cells[firstRow, expectedColumn].Value = $"Test {i + 1}";
                outputSheet.Cells[secoundRow, expectedColumn].Value = "Expected Value";
                outputSheet.Cells[secoundRow, actualColumn].Value = "Actual Value";
                for (int j = 0; j < rows; j++)
                {
                    outputSheet.Cells[j + emptyRowCount, expectedColumn].Value = outputData[i][j].ExpectedValue;
                    outputSheet.Cells[j + emptyRowCount, actualColumn].Value = outputData[i][j].ActualValue;
                }
            }
        }

        private static void PrefillInputSheet(ExcelWorksheet inputSheet, List<Dictionary<string, string>> inputData)
        {
            var columns = inputData.FirstOrDefault().Select(e => e.Key).ToList();
            var emptyRow = 2;
            var emptyColumn = 2;

            for (int i = 0; i < columns.Count; i++)
            {
                inputSheet.Cells[i + emptyRow, 1].Value = columns[i];
            }

            var rows = columns.Count;
            for (int i = 0; i < inputData.Count; i++)
            {
                inputSheet.Cells[1, i + emptyColumn].Value = $"Test {i + 1}";
                for (int j = 0; j < rows; j++)
                {
                    inputSheet.Cells[j + emptyRow, i + emptyColumn].Value = inputData[i].Values.ElementAt(j);
                }
            }
        }
    }
}
