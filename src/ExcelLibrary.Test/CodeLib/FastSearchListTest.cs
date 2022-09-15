using System;
using System.Collections.Generic;
using QiHe.CodeLib;
using Xunit;

namespace ExcelLibrary.Test.CodeLib
{
    public class FastSearchListTest
    {
        [Fact]
        public void SimpleTest()
        {
            IList<string> list = new FastSearchList<string>();

            list.Add("Item 1");
            list.Add("Item 2");
            list.Add("Item 3");

            Assert.Equal(3, list.Count);
            Assert.Equal("Item 1", list[0]);
            Assert.Equal("Item 2", list[1]);
            Assert.Equal("Item 3", list[2]);

            Assert.Equal(0, list.IndexOf("Item 1"));
            Assert.Equal(1, list.IndexOf("Item 2"));
            Assert.Equal(2, list.IndexOf("Item 3"));
        }

        [Fact]
        public void AddDuplicateTest()
        {
            IList<string> list = new FastSearchList<string>();

            list.Add("Item 1");
            list.Add("Item 1");

            Assert.Equal(2, list.Count);
            Assert.Equal(0, list.IndexOf("Item 1"));
        }

        [Fact]
        public void InsertDuplicateTest()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                IList<string> list = new FastSearchList<string>();

                list.Add("Item 1");
                list.Insert(1, "Item 1");
            });
        }


        [Fact]
        public void RemoveAtTest()
        {
            IList<string> list = new FastSearchList<string>();

            list.Add("Item 1");
            list.Add("Item 2");
            list.Add("Item 3");

            list.RemoveAt(0);

            Assert.Equal(2, list.Count);
            Assert.Equal("Item 2", list[0]);
            Assert.Equal("Item 3", list[1]);
            Assert.Equal(0, list.IndexOf("Item 2"));
            Assert.Equal(1, list.IndexOf("Item 3"));

            list.Add("Item 1");

            Assert.Equal(3, list.Count);
            Assert.Equal("Item 2", list[0]);
            Assert.Equal("Item 3", list[1]);
            Assert.Equal("Item 1", list[2]);
            Assert.Equal(0, list.IndexOf("Item 2"));
            Assert.Equal(1, list.IndexOf("Item 3"));
            Assert.Equal(2, list.IndexOf("Item 1"));
        }

        [Fact]
        public void RemoveTest()
        {
            IList<string> list = new FastSearchList<string>();

            list.Add("Item 1");
            list.Add("Item 2");
            list.Add("Item 3");

            list.Remove("Item 2");

            Assert.Equal(2, list.Count);
            Assert.Equal("Item 1", list[0]);
            Assert.Equal("Item 3", list[1]);
            Assert.Equal(0, list.IndexOf("Item 1"));
            Assert.Equal(1, list.IndexOf("Item 3"));

            list.Add("Item 4");

            Assert.Equal(3, list.Count);
            Assert.Equal("Item 1", list[0]);
            Assert.Equal("Item 3", list[1]);
            Assert.Equal("Item 4", list[2]);
            Assert.Equal(0, list.IndexOf("Item 1"));
            Assert.Equal(1, list.IndexOf("Item 3"));
            Assert.Equal(2, list.IndexOf("Item 4"));
        }

        [Fact]
        public void InsertTest()
        {
            IList<string> list = new FastSearchList<string>();

            list.Add("Item 1");
            list.Add("Item 2");
            list.Add("Item 3");

            list.Insert(1, "Item 1.5");

            Assert.Equal(4, list.Count);
            Assert.Equal("Item 1", list[0]);
            Assert.Equal("Item 1.5", list[1]);
            Assert.Equal("Item 2", list[2]);
            Assert.Equal("Item 3", list[3]);
            Assert.Equal(0, list.IndexOf("Item 1"));
            Assert.Equal(1, list.IndexOf("Item 1.5"));
            Assert.Equal(2, list.IndexOf("Item 2"));
            Assert.Equal(3, list.IndexOf("Item 3"));
        }
    }
}