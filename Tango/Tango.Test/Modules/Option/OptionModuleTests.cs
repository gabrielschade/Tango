using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Tango.Linq;
using Tango.Modules;
using Tango.Types;

namespace Tango.Test.Modules
{
    [TestClass]
    public class OptionModuleTests

    {
        Func<int, bool> _isEven = element => element % 2 == 0;
        Func<int, Option<int>> _squareWhenEven = element => element % 2 == 0 ? element * element : Option<int>.None();

        [TestMethod]
        public void OptionOfNullableWhenNull()
        {
            int? value = null;
            Option<int> optionValue =
                OptionModule.OfNullable(value);

            Assert.IsTrue(optionValue.IsNone);
        }

        [TestMethod]
        public void OptionOfNullableWhenHasValue()
        {
            int? expected = 15;
            Option<int> optionValue =
                OptionModule.OfNullable(expected);

            int result =
                optionValue.Match(
                    number => number,
                    () => 0);

            Assert.AreEqual(expected.Value, result);
        }

        [TestMethod]
        public void OptionToNullableWhenNone()
        {
            Option<int> optionValue = Option<int>.None();
            int? result = OptionModule.ToNullable(optionValue);

            Assert.IsFalse(result.HasValue);
        }

        [TestMethod]
        public void OptionToNullableWhenSome()
        {
            int expected = 35;
            Option<int> optionValue = expected;
            int? result = OptionModule.ToNullable(optionValue);

            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod]
        public void OptionAsEnumerableWhenNone()
        {
            Option<int> optionValue = Option<int>.None();
            IEnumerable<int> result = OptionModule.AsEnumerable(optionValue);

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void OptionAsEnumerableWhenSome()
        {
            Option<int> optionValue = 35;
            IEnumerable<int> result = OptionModule.AsEnumerable(optionValue);

            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void OptionToArrayWhenNone()
        {
            Option<int> optionValue = Option<int>.None();
            int[] result = OptionModule.ToArray(optionValue);

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void OptionToArrayWhenSome()
        {
            Option<int> optionValue = 35;
            int[] result = OptionModule.ToArray(optionValue);

            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void OptionToListWhenNone()
        {
            Option<int> optionValue = Option<int>.None();
            List<int> result = OptionModule.ToList(optionValue);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void OptionToListWhenSome()
        {
            Option<int> optionValue = 35;
            List<int> result = OptionModule.ToList(optionValue);

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void OptionIterateWhenNone()
        {
            Option<int> optionValue = Option<int>.None();
            int result = 0;
            OptionModule.Iterate(value => result += value, optionValue);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void OptionIterateWhenSome()
        {
            Option<int> optionValue = 35;
            int result = 0;
            OptionModule.Iterate(value => result += value, optionValue);

            Assert.AreEqual(35, result);
        }

        [TestMethod]
        public void OptionCountWhenNone()
        {
            Option<int> optionValue = Option<int>.None();
            int result = OptionModule.Count(optionValue);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void OptionCountWhenSome()
        {
            Option<int> optionValue = 35;
            int result = OptionModule.Count(optionValue);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void OptionFilterWhenNone()
        {
            Option<int> optionValue = Option<int>.None();
            Option<int> result = OptionModule.Filter(_isEven, optionValue);

            Assert.IsTrue(result.IsNone);
        }

        [TestMethod]
        public void OptionFilterWhenFalse()
        {
            Option<int> optionValue = 35;
            Option<int> result = OptionModule.Filter(_isEven, optionValue);

            Assert.IsTrue(result.IsNone);
        }

        [TestMethod]
        public void OptionFilterWhenTrue()
        {
            int expected = 36;
            Option<int> optionValue = expected;
            Option<int> optionResult = OptionModule.Filter(_isEven, optionValue);
            int result = optionResult.Match(value => value, () => 0);


            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void OptionExistsWhenNone()
        {
            bool expected = false;
            Option<int> optionValue = Option<int>.None();
            bool result = OptionModule.Exists(_isEven, optionValue);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void OptionExistsWhenFalse()
        {
            bool expected = false;
            Option<int> optionValue = 35;
            bool result = OptionModule.Exists(_isEven, optionValue);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void OptionExistsWhenTrue()
        {
            bool expected = true;
            Option<int> optionValue = 36;
            bool result = OptionModule.Exists(_isEven, optionValue);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void OptionMapWhenNone()
        {
            Option<int> optionValue = Option<int>.None();
            Option<bool> optionResult = OptionModule.Map(_isEven, optionValue);

            Assert.IsTrue(optionResult.IsNone);
        }

        [TestMethod]
        public void OptionMapWhenSomeFalse()
        {
            string expected = "Odd";
            Option<int> optionValue = 35;
            Option<string> optionResult = OptionModule.Map((value) => _isEven(value) ? "Even" : "Odd", optionValue);
            string result = optionResult.Match(value => value, () => string.Empty);


            Assert.AreEqual(expected,result);
        }

        [TestMethod]
        public void OptionMapWhenSomeTrue()
        {
            Option<int> optionValue = 36;
            Option<bool> optionResult = OptionModule.Map(_isEven, optionValue);
            bool result = optionResult.Match(value => value, () => false);


            Assert.IsTrue(optionResult.IsSome && result);
        }

        [TestMethod]
        public void OptionApplyWhenSomeFunctionSomeValue()
        {
            Option<Func<int, bool>> optionFunction = _isEven;
            Option<int> optionValue = 36;
            Option<bool> optionResult =
                OptionModule.Apply(optionFunction, optionValue);

            bool result = optionResult.Match(value => value, () => false);
            Assert.IsTrue(optionResult.IsSome && result);
        }

        [TestMethod]
        public void OptionApplyWhenSomeFunctionNoneValue()
        {
            Option<Func<int, bool>> optionFunction = _isEven;
            Option<int> optionValue = Option<int>.None();
            Option<bool> optionResult =
                OptionModule.Apply(optionFunction, optionValue);

            bool result = optionResult.Match(value => value, () => false);
            Assert.IsTrue(optionResult.IsNone);
        }

        [TestMethod]
        public void OptionApplyWhenNoneFunctionSomeValue()
        {
            Option<Func<int, bool>> optionFunction = Option<Func<int, bool>>.None();
            Option<int> optionValue = 36;
            Option<bool> optionResult =
                OptionModule.Apply(optionFunction, optionValue);

            bool result = optionResult.Match(value => value, () => false);
            Assert.IsTrue(optionResult.IsNone);
        }

        [TestMethod]
        public void OptionApplyWhenNoneFunctionNoneValue()
        {
            Option<Func<int, bool>> optionFunction = Option<Func<int, bool>>.None();
            Option<int> optionValue = Option<int>.None();
            Option<bool> optionResult =
                OptionModule.Apply(optionFunction, optionValue);

            bool result = optionResult.Match(value => value, () => false);
            Assert.IsTrue(optionResult.IsNone);
        }

        [TestMethod]
        public void OptionBindWhenNone()
        {
            Option<int> optionValue = Option<int>.None();
            Option<int> optionResult = OptionModule.Bind(_squareWhenEven, optionValue);

            Assert.IsTrue(optionResult.IsNone);
        }

        [TestMethod]
        public void OptionBindWhenSomeFalse()
        {
            int expected = 0;
            Option<int> optionValue = 5;
            Option<int> optionResult = OptionModule.Bind(_squareWhenEven, optionValue);
            int result = optionResult.Match(value => value, () => 0);


            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void OptionBindWhenSomeTrue()
        {
            int expected = 36;
            Option<int> optionValue = 6;
            Option<int> optionResult = OptionModule.Bind(_squareWhenEven, optionValue);
            int result = optionResult.Match(value => value, () => 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void OptionFoldWhenNone()
        {
            int state = 10;
            int expected = state;
            Option<int> optionValue = Option<int>.None();
            Option<int> optionResult = OptionModule.Fold((_state, value) => _state + value, state, optionValue);
            int result = optionResult.Match(value => value, () => 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void OptionFoldWhenSome()
        {
            int initial = 25;
            int state = 10;
            int expected = state + initial;
            Option<int> optionValue = initial;
            Option<int> optionResult = OptionModule.Fold((_state, value) => _state + value, state, optionValue);
            int result = optionResult.Match(value => value, () => 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void OptionFoldBackWhenNone()
        {
            int state = 10;
            int expected = state;
            Option<int> optionValue = Option<int>.None();
            Option<int> optionResult = OptionModule.FoldBack((value, _state) => _state + value, optionValue, state);
            int result = optionResult.Match(value => value, () => 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void OptionFoldBackWhenSome()
        {
            int initial = 25;
            int state = 10;
            int expected = state + initial;
            Option<int> optionValue = initial;
            Option<int> optionResult = OptionModule.FoldBack((value, _state) => _state + value, optionValue, state);
            int result = optionResult.Match(value => value, () => 0);

            Assert.AreEqual(expected, result);
        }

    }
}
