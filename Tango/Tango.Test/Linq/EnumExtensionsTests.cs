using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Tango.Modules;

namespace Tango.Linq
{
    internal enum TestEnum
    {
        ZeroValue =0,
        One = 1,
        Two = 2,
        Three = 3
    }

    [TestClass]
    public class EnumExtensionTests

    {
        [TestMethod]
        public void EnumExtensionAsEnumerable()
        {
            int expected = 4;
            IEnumerable<TestEnum> enums = 
                EnumExtensions.AsEnumerable<TestEnum>();

            Assert.AreEqual(expected, enums.Count());
        }

        [TestMethod]
        public void EnumExtensionAsEnumerableWithNonEnumValue()
        {
            int expected = 0;
            IEnumerable<int> enums =
                EnumExtensions.AsEnumerable<int>();

            Assert.AreEqual(expected, enums.Count());
        }

        [TestMethod]
        public void EnumExtensionAsEnumerableValues()
        {
            int expected = 6;
            IEnumerable<TestEnum> enums =
                EnumExtensions.AsEnumerable<TestEnum>();

            Assert.AreEqual(expected, enums.Sum(value => Convert.ToInt32(value)));
        }

        [TestMethod]
        public void EnumExtensionAsEnumerableSkipZero()
        {
            int expected = 3;
            IEnumerable<TestEnum> enums =
                EnumExtensions.AsEnumerableSkipZero<TestEnum>();

            Assert.AreEqual(expected, enums.Count());
        }

        [TestMethod]
        public void EnumExtensionAsEnumerableSkipZeroValues()
        {
            int expected = 6;
            IEnumerable<TestEnum> enums =
                EnumExtensions.AsEnumerableSkipZero<TestEnum>();

            Assert.AreEqual(expected, enums.Sum(value => Convert.ToInt32(value)));
        }
    }
}
