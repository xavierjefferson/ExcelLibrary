using System;
using System.IO;
using System.Text;
using ExcelLibrary.SpreadSheet;
using Xunit;

namespace ExcelLibrary.Test
{
    public class SimpleTest
    {
        [Fact]
        public void SimpleReadWriteTest()
        {
            var tempFilePath = Path.GetTempFileName();
            {
                var workbook = new Workbook();
                var worksheet = new Worksheet("Test1");
                worksheet.Cells[0, 1] = new Cell(100);
                worksheet.Cells[2, 0] = new Cell("Test String");
                workbook.Worksheets.Add(worksheet);
                workbook.Save(tempFilePath);
            }

            {
                var workbook = Workbook.Load(tempFilePath);
                Assert.Equal(1, workbook.Worksheets.Count);

                var worksheet = workbook.Worksheets[0];
                Assert.Equal("Test1", worksheet.Name);
                Assert.Equal(100, worksheet.Cells[0, 1].Value);
                Assert.Equal("Test String", worksheet.Cells[2, 0].Value);
            }
        }

        [Fact]
        public void SimpleMultipleSheetTest()
        {
            var worksheetToCreate = 10;

            var tempFilePath = Path.GetTempFileName();
            {
                var workbook = new Workbook();
                for (var i = 0; i < worksheetToCreate; i++)
                {
                    var worksheetWrite2 = new Worksheet(string.Format("Sheet {0}", i));
                    workbook.Worksheets.Add(worksheetWrite2);
                }

                workbook.Save(tempFilePath);
            }

            {
                var workbook = Workbook.Load(tempFilePath);

                Assert.Equal(worksheetToCreate, workbook.Worksheets.Count);
                for (var i = 0; i < worksheetToCreate; i++)
                    Assert.Equal(string.Format("Sheet {0}", i), workbook.Worksheets[i].Name);
            }
        }

        [Fact]
        public void WriteLongTextTest()
        {
            var longTextLength = 50000;

            var builder = new StringBuilder(longTextLength);
            for (var i = 0; i < longTextLength; i++)
                builder.Append('A');

            var longText = builder.ToString();

            var tempFilePath = Path.GetTempFileName();
            {
                var workbook = new Workbook();
                var worksheet = new Worksheet("Test");
                worksheet.Cells[0, 0] = new Cell(longText);

                workbook.Worksheets.Add(worksheet);
                workbook.Save(tempFilePath);
            }

            {
                var workbook = Workbook.Load(tempFilePath);
                Assert.Equal(longText, workbook.Worksheets[0].Cells[0, 0].Value);
            }
        }

        [Fact]
        public void WriteMultipleCellTest()
        {
            var worksheetToWrite = 30;
            var rowToWrite = 200;
            var columnToWrite = 15;

            var tempFilePath = Path.GetTempFileName();
            {
                var start = Environment.TickCount;

                var workbook = new Workbook();
                for (var sheet = 0; sheet < worksheetToWrite; sheet++)
                {
                    var worksheet = new Worksheet(string.Format("Sheet {0}", sheet));

                    for (var row = 0; row < rowToWrite; row++)
                    for (var column = 0; column < columnToWrite; column++)
                        worksheet.Cells[row, column] = new Cell(string.Format("{0}{1}", row, column));

                    workbook.Worksheets.Add(worksheet);
                }

                workbook.Save(tempFilePath);

                var end = Environment.TickCount;
                Console.WriteLine("Write tick count: {0}", end - start);
            }

            {
                var start = Environment.TickCount;

                var workbook = Workbook.Load(tempFilePath);
                for (var sheet = 0; sheet < worksheetToWrite; sheet++)
                for (var row = 0; row < rowToWrite; row++)
                for (var column = 0; column < columnToWrite; column++)
                    Assert.Equal(string.Format("{0}{1}", row, column),
                        workbook.Worksheets[sheet].Cells[row, column].Value);

                var end = Environment.TickCount;

                Console.WriteLine("Read tick count: {0}", end - start);
            }
        }

        [Fact]
        public void WriteFormulaTest()
        {
            var tempFilePath = Path.GetTempFileName();
            {
                var workbook = new Workbook();
                var worksheet = new Worksheet("Test");
                worksheet.Cells[0, 0] = new Cell(10);
                worksheet.Cells[0, 1] = new Cell(20);
                worksheet.Cells[0, 2] = new Cell("=A1+B1");

                workbook.Worksheets.Add(worksheet);
                workbook.Save(tempFilePath);
            }

            {
                var workbook = Workbook.Load(tempFilePath);
                Assert.Equal(10, workbook.Worksheets[0].Cells[0, 0].Value);
                Assert.Equal(20, workbook.Worksheets[0].Cells[0, 1].Value);
                Assert.Equal("=A1+B1", workbook.Worksheets[0].Cells[0, 2].Value);
            }
        }

        [Fact]
        public void WriteCellTest1()
        {
            var tempFilePath = Path.GetTempFileName();
            {
                var workbook = new Workbook();
                var worksheet1 = new Worksheet("Test 1");
                var worksheet2 = new Worksheet("Test 2");
                var worksheet3 = new Worksheet("Test 3");
                worksheet1.Cells[0, 1] = new Cell("Test");
                worksheet1.Cells[1, 0] = new Cell("Test");

                workbook.Worksheets.Add(worksheet1);
                workbook.Worksheets.Add(worksheet2);
                workbook.Worksheets.Add(worksheet3);
                workbook.Save(tempFilePath);
            }

            {
                var workbook = Workbook.Load(tempFilePath);
                Assert.Equal(3, workbook.Worksheets.Count);
            }
        }
    }
}