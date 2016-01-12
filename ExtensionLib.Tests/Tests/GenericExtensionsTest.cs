using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

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

        [Test]
        public void Cloning_by_json_creates_new_object_with_same_values()
        {
            var nest = new MyNestedObject() { MyDateTimeProp = DateTime.Now, MyDoubleProp = 5.3 };


            var obj = new MyObject() { MyIntProp = 5, MyStringProp = "Shnogerdob", MyNestedObjectProp = nest };
            var objClone = obj.CloneByJson();

            Assert.False(obj == objClone); // references are different
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
    }
}
