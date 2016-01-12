using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ExtensionLib.Tests
{
    [TestFixture]
    public class TypeExtensionsTest
    {
        Type intType;
        Type intTypeNullable;
        Type intTypeNullableByExt;

        Type strType;
        Type strTypeNullableByExt;

        Type dateTimeType;
        Type dateTimeTypeNullable;
        Type dateTimeTypeNullableByExt;

        [SetUp]
        public void Init()
        {
            intType = typeof(int);
            intTypeNullable = typeof(int?);
            intTypeNullableByExt = typeof(int).GetNullableType();

            strType = typeof(string);
            strTypeNullableByExt = typeof(string).GetNullableType();

            dateTimeType = typeof(DateTime);
            dateTimeTypeNullable = typeof(DateTime?);
            dateTimeTypeNullableByExt = typeof(DateTime).GetNullableType();
        }

        [Test]
        public void Is_works_for_nullable_value_types()
        {
            Assert.True(intType.Is(intType));
            Assert.True(intType.Is(intTypeNullable));
            Assert.True(intTypeNullable.Is(intType));

            Assert.True(strType.Is(strType));
            Assert.True(strType.Is(strTypeNullableByExt));

            Assert.False(intType.Is(strType));
            Assert.False(strType.Is(intType));
            Assert.False(strType.Is(intTypeNullable));

            Assert.True(dateTimeType.Is(dateTimeTypeNullable));
            Assert.True(dateTimeTypeNullable.Is(dateTimeType));
        }

        [Test]
        public void GetNullableType_works_for_value_types()
        {
            Assert.False(intType == intTypeNullable);
            Assert.True(intTypeNullable == intTypeNullableByExt);
            Assert.True(strType == strTypeNullableByExt);
            Assert.False(dateTimeType == dateTimeTypeNullable);
            Assert.True(dateTimeTypeNullable == dateTimeTypeNullableByExt);
        }


        private class MyEquatableClass : IEquatable<MyEquatableClass> { public bool Equals(MyEquatableClass other) { return true; } }
        private class MyNonEquatableClass { }
        private interface MyInterface { }
        private class MyInterfaceClass : MyInterface { }

        [Test]
        public void Implements_works_for_generic_interfaces()
        {
            Assert.True(typeof(MyEquatableClass).Implements(typeof(IEquatable<>)));
            Assert.False(typeof(MyNonEquatableClass).Implements(typeof(IEquatable<>)));

            var myEquObj = new MyEquatableClass();
            var myNonEquObj = new MyNonEquatableClass();

            Assert.True(myEquObj.GetType().Implements(typeof(IEquatable<>)));
            Assert.False(myNonEquObj.GetType().Implements(typeof(IEquatable<>)));

            Assert.True(typeof(MyInterfaceClass).Implements(typeof(MyInterface)));
            Assert.False(typeof(MyNonEquatableClass).Implements(typeof(MyInterface)));
        }
    }
}
