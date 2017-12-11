using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tango.Functional;
using static Tango.Functional.QuickDelegateCast;

namespace Tango.Test.Functional
{
    [TestClass]
    public class QuickDelegateCastTests
    {
        [TestMethod]
        public void CastF0()
        {
            string TestFunction()
                => "Hello";

            Func<string> result = F(TestFunction);

            Assert.AreEqual("Hello", result());
        }

        [TestMethod]
        public void CastF1()
        {
            string TestFunction(int value1)
                => string.Concat(value1);

            Func<string> result =
            F<int, string>(TestFunction).PartialApply(1);

            Assert.AreEqual("1", result());


            int SampleAdd(int value1, int value2)
                => value1 + value2;


            F<int, int, int>(SampleAdd).Curry();
            F<int, int, int>(SampleAdd).PartialApply(1);
        }

        [TestMethod]
        public void CastF2()
        {
            string TestFunction(int value1, string value2)
                => string.Concat(value1, value2);

            Func<string> result =
            F<int, string, string>(TestFunction).PartialApply(1, "Hello");

            Assert.AreEqual("1Hello", result());
        }

        [TestMethod]
        public void CastF3()
        {
            string TestFunction(int value1, string value2, bool value3)
                => string.Concat(value1, value2, value3);

            Func<string> result =
            F<int, string, bool, string>(TestFunction).PartialApply(1, "Hello", true);

            Assert.AreEqual("1HelloTrue", result());
        }

        [TestMethod]
        public void CastF4()
        {
            string TestFunction(int value1, string value2, bool value3, double value4)
                => string.Concat(value1, value2, value3, value4);

            Func<string> result =
            F<int, string, bool, double, string>(TestFunction).PartialApply(1, "Hello", true, 3);

            Assert.AreEqual("1HelloTrue3", result());
        }




        [TestMethod]
        public void CastA0()
        {
            string stringResult = "";
            void TestFunction()
                => stringResult = "Hello";

            Action result = A(TestFunction);
            result();
            Assert.AreEqual("Hello", stringResult);
        }

        [TestMethod]
        public void CastA1()
        {
            string stringResult = "";
            void TestFunction(int value1)
                => stringResult = string.Concat(value1);

            Action result = A<int>(TestFunction).PartialApply(1);
            result();
            Assert.AreEqual("1", stringResult);
        }

        [TestMethod]
        public void CastA2()
        {
            string stringResult = "";
            void TestFunction(int value1, string value2)
                => stringResult = string.Concat(value1, value2);

            Action result = A<int, string>(TestFunction).PartialApply(1, "Hello");
            result();
            Assert.AreEqual("1Hello", stringResult);
        }

        [TestMethod]
        public void CastA3()
        {
            string stringResult = "";
            void TestFunction(int value1, string value2, bool value3)
                => stringResult = string.Concat(value1, value2, value3);

            Action result =
            A<int, string, bool>(TestFunction).PartialApply(1, "Hello", true);
            result();
            Assert.AreEqual("1HelloTrue", stringResult);
        }

        [TestMethod]
        public void CastA4()
        {
            string stringResult = "";
            void TestFunction(int value1, string value2, bool value3, double value4)
                => stringResult= string.Concat(value1, value2, value3, value4);

            Action result =
            A<int, string, bool, double>(TestFunction).PartialApply(1, "Hello", true, 3);
            result();

            Assert.AreEqual("1HelloTrue3", stringResult);
        }
    }
}
