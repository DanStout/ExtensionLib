using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Diagnostics;

namespace ExtensionLib.Tests
{
    [TestFixture]
    public class GenericExtensionsTest
    {
        private class MyObject
        {
            public int MyIntProp {get; set;}
            public string MyStringProp { get; set; }
            public MyNestedObject MyNestedObjectProp { get; set; }
        }
        
        private class MyNestedObject
        {
            public DateTime MyDateTimeProp { get; set; }
            public double MyDoubleProp { get; set; }
        }

        private class MySimpleObject
        {
            public int MyIntProp { get; set; }
            public string MyStringProp { get; set; }
        }

        [Test]
        public void CloneByJson_creates_new_object_with_same_values()
        {
            var nest = new MyNestedObject() { MyDateTimeProp = DateTime.Now, MyDoubleProp = 5.3 };

            var obj = new MyObject() { MyIntProp = 5, MyStringProp = "Shnogerdob", MyNestedObjectProp = nest };
            var objClone = obj.CloneByJson();

            Assert.False(obj == objClone, "References should be different");
            Assert.AreEqual(obj.MyIntProp, objClone.MyIntProp);
            Assert.AreEqual(obj.MyStringProp, objClone.MyStringProp);
            Assert.AreEqual(obj.MyNestedObjectProp.MyDateTimeProp, objClone.MyNestedObjectProp.MyDateTimeProp);
            Assert.AreEqual(obj.MyNestedObjectProp.MyDoubleProp, obj.MyNestedObjectProp.MyDoubleProp);
        }

        [Test]
        public void Wrap_wraps_object_with_value()
        {
            Assert.AreEqual("\"test\"", "test".Wrap('"'));
            Assert.AreEqual("\nanother test\n", "another test".Wrap('\n'));
            Assert.AreEqual("c\ntest\nc", "\ntest\n".Wrap('c'));
            Assert.AreEqual("testMyStringtest", "MyString".Wrap("test"));
        }

        [Test]
        public void MakeToString_works_correctly()
        {
            var simple = new MySimpleObject()
            {
                MyIntProp = 5,
                MyStringProp = "Hello"
            };

            var toStr = simple.MakeToString();
            var expected = "MySimpleObject: [MyIntProp:5, MyStringProp:\"Hello\"]";
            Assert.AreEqual(toStr, expected);
            var newToStr = simple.MakeToString(x => x.MyStringProp);
            var newExpected = "MySimpleObject: [MyIntProp:5]";
            Assert.AreEqual(newToStr, newExpected);
        }
    }
}
