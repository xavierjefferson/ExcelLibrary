using System;
using ExcelLibrary.SpreadSheet;
using Xunit;

namespace ExcelLibrary.Test.Issue
{
    public class Issue10_Test
    {
        [Fact]
        public void Test()
        {
            var workbook = new Workbook();
            var worksheet = new Worksheet("Test");
            workbook.Worksheets.Add(worksheet);

            var date = new DateTime(2009, 2, 13, 11, 30, 45);
            worksheet.Cells[0, 0] = new Cell(date);
            worksheet.Cells[0, 0].DateTimeValue = date;

            Assert.Equal(date, worksheet.Cells[0, 0].DateTimeValue);
        }

        [Fact]
        public void RelatedTest()
        {
            var workbook = new Workbook();
            var worksheet = new Worksheet("Test");
            workbook.Worksheets.Add(worksheet);

            var date = new DateTime(2009, 2, 13, 11, 30, 45);
            worksheet.Cells[0, 0] = new Cell(date);


            Assert.NotNull(worksheet.Cells[0, 0].Format);
        }
    }
}