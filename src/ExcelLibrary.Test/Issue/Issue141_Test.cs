using System.IO;
using ExcelLibrary.CompoundDocumentFormat;
using Xunit;

namespace ExcelLibrary.Test.Issue
{
    public class Issue141_Test
    {
        [Fact]
        public void Test()
        {
            var filename = Path.GetTempFileName();

            try
            {
                // create invalid file for read
                using (var writer = new StreamWriter(filename))
                {
                    writer.WriteLine("test");
                }

                try
                {
                    // triggers exception
                    CompoundDocument.Open(filename);
                }
                catch
                {
                    Assert.True(true, "Expection trigger.");
                }
            }
            finally
            {
                // should not trigger any IO Exception
                File.Delete(filename);
            }
        }
    }
}