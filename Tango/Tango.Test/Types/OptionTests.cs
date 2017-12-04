using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tango.Types;

namespace Tango.Test.Types
{
    [TestClass]
    public class OptionTests
    {
        [TestMethod]
        public void OptionMatchWithActionWhenSome()
        {
            int expected = 25;
            int result = 10;
            Option<int> option = 15;
            option.Match(
                value => result += value,
                () => { result = 0; });

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void OptionMatchWithActionWhenNone()
        {
            int expected = 10;
            int result = 10;
            Option<int> option = Option<int>.None();
            option.Match(
                value => result += value,
                () => { result += 0; });

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void OptionMatch2WithActionWhenBothSome()
        {
            int expected = 35;
            int result = 10;
            Option<int> option = 15;
            Option<int> option2 = 10;
            option.Match2(
                option2,
                (value, value2) => result += value + value2,
                () => { result += 0; });

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void OptionMatch2WithActionWhenFirstSome()
        {
            int expected = 10;
            int result = 10;
            Option<int> option = 15;
            Option<int> option2 = Option<int>.None();
            option.Match2(
                option2,
                (value, value2) => result += value + value2,
                () => { result += 0; });

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void OptionMatch2WithActionWhenSecondSome()
        {
            int expected = 10;
            int result = 10;
            Option<int> option = Option<int>.None();
            Option<int> option2 = 10;
            option.Match2(
                option2,
                (value, value2) => result += value + value2,
                () => { result += 0; });

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void OptionMatch2WithActionWhenBothNone()
        {
            int expected = 10;
            int result = 10;
            Option<int> option = Option<int>.None();
            Option<int> option2 = Option<int>.None();
            option.Match2(
                option2,
                (value, value2) => result += value + value2,
                () => { result += 0; });

            Assert.AreEqual(expected, result);
        }
    }
}
