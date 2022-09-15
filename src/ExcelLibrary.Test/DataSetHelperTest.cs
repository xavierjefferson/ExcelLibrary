using System;
using System.Data;
using System.IO;
using Xunit;

namespace ExcelLibrary.Test
{
    public class DataSetHelperTest
    {
        // TODO:
        // 1. use real Excel file for testing
        // 2. use more complicated cell data types

        [Fact]
        public void SimpleCreateTest()
        {
            var tempFilePath = Path.GetTempFileName();

            {
                var ds = new DataSet();
                var dt1 = new DataTable("Table 1");
                dt1.Columns.Add("Column A", typeof(string));
                dt1.Columns.Add("Column B", typeof(string));
                dt1.Rows.Add("Test 1", "Test 2");
                dt1.Rows.Add("Test 3", "Test 4");
                ds.Tables.Add(dt1);

                var dt2 = new DataTable("Table 2");
                ds.Tables.Add(dt2);

                var dt3 = new DataTable("Table 3");
                dt3.Columns.Add("Column C", typeof(string));
                ds.Tables.Add(dt3);

                DataSetHelper.CreateWorkbook(tempFilePath, ds);
            }

            {
                var ds = DataSetHelper.CreateDataSet(tempFilePath);
                Assert.Equal(3, ds.Tables.Count);
                Assert.Equal("Table 1", ds.Tables[0].TableName);
                Assert.Equal("Table 2", ds.Tables[1].TableName);
                Assert.Equal("Table 3", ds.Tables[2].TableName);

                Assert.Equal(2, ds.Tables[0].Columns.Count);
                Assert.Single(ds.Tables[1].Columns);
                Assert.Single(ds.Tables[2].Columns);
                Assert.Equal("Column A", ds.Tables[0].Columns[0].ColumnName);
                Assert.Equal("Column B", ds.Tables[0].Columns[1].ColumnName);
                Assert.Equal("Column C", ds.Tables[2].Columns[0].ColumnName);

                Assert.Equal(2, ds.Tables[0].Rows.Count);
                Assert.Equal(0, ds.Tables[1].Rows.Count);
                Assert.Equal(0, ds.Tables[2].Rows.Count);
                Assert.Equal("Test 1", ds.Tables[0].Rows[0][0]);
                Assert.Equal("Test 2", ds.Tables[0].Rows[0][1]);
                Assert.Equal("Test 3", ds.Tables[0].Rows[1][0]);
                Assert.Equal("Test 4", ds.Tables[0].Rows[1][1]);
            }
        }

        [Fact]
        public void EmptyDataSetCreateTest()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var tempFilePath = Path.GetTempFileName();
                var ds = new DataSet();

                DataSetHelper.CreateWorkbook(tempFilePath, ds);
            });
        }
    }
}