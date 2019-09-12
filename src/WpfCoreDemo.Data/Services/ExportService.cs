using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Office.Interop.Excel;
using WpfCoreDemo.Data.Domain;

namespace WpfCoreDemo.Data.Services
{
    public class ExportService
    {
        public void ExportTweets(IEnumerable<TweetDisplayData> tweets)
        {
            // Start Excel and get Application object.
            var excel = new Application();
            excel.Visible = true;

            // Get a new workbook.
            var workbook = excel.Workbooks.Add(Missing.Value);
            var sheet = (Worksheet)workbook.ActiveSheet;

            // Add table headers going cell by cell.
            var rowIndex = 1;
            sheet.Cells[rowIndex, 1] = "Author";
            sheet.Cells[rowIndex, 2] = "Tweet Text";
            rowIndex++;

            // Export data
            foreach (var tweet in tweets)
            {
                sheet.Cells[rowIndex, 1] = tweet.AuthorName;
                sheet.Cells[rowIndex, 2] = tweet.Text;
                rowIndex++;
            }

            // AutoFit columns A:D.
            var range = sheet.get_Range("A1", "B1");
            range.EntireColumn.AutoFit();

            // Make sure Excel is visible and give the user control
            // of Microsoft Excel's lifetime.
            excel.Visible = true;
            excel.UserControl = true;
        }
    }

}
