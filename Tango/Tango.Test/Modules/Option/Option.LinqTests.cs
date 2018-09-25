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
    public class OptionLinqTests

    {
        Func<int, bool> _isEven = element => element % 2 == 0;
        Func<int, Option<int>> _squareWhenEven = element => element % 2 == 0 ? element * element : Option<int>.None();

        [TestMethod]
        public void LinqOptionToNullableWhenNone()
        {
            Option<int> optionValue = Option<int>.None();
            int? result = optionValue.ToNullable();

            Assert.IsFalse(result.HasValue);
        }

        [TestMethod]
        public void LinqOptionToNullableWhenSome()
        {
            int expected = 35;
            Option<int> optionValue = expected;
            int? result = optionValue.ToNullable();

            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod]
        public void LinqOptionAsEnumerableWhenNone()
        {
            Option<int> optionValue = Option<int>.None();
            IEnumerable<int> result = optionValue.AsEnumerable();

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void LinqOptionAsEnumerableWhenSome()
        {
            Option<int> optionValue = 35;
            IEnumerable<int> result = optionValue.AsEnumerable();

            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void LinqOptionToArrayWhenNone()
        {
            Option<int> optionValue = Option<int>.None();
            int[] result = optionValue.ToArray();

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void LinqOptionToArrayWhenSome()
        {
            Option<int> optionValue = 35;
            int[] result = optionValue.ToArray();

            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void LinqOptionToListWhenNone()
        {
            Option<int> optionValue = Option<int>.None();
            List<int> result = optionValue.ToList();

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void LinqOptionToListWhenSome()
        {
            Option<int> optionValue = 35;
            List<int> result = optionValue.ToList();

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void LinqOptionIterateWhenNone()
        {
            Option<int> optionValue = Option<int>.None();
            int result = 0;
            optionValue.Iterate(value => result += value);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void LinqOptionIterateWhenSome()
        {
            Option<int> optionValue = 35;
            int result = 0;
            optionValue.Iterate(value => result += value);

            Assert.AreEqual(35, result);
        }

        [TestMethod]
        public void LinqOptionCountWhenNone()
        {
            Option<int> optionValue = Option<int>.None();
            int result = optionValue.Count();

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void LinqOptionCountWhenSome()
        {
            Option<int> optionValue = 35;
            int result = optionValue.Count();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void LinqOptionFilterWhenNone()
        {
            Option<int> optionValue = Option<int>.None();
            Option<int> result = optionValue.Filter(_isEven);

            Assert.IsTrue(result.IsNone);
        }

        [TestMethod]
        public void LinqOptionFilterWhenFalse()
        {
            Option<int> optionValue = 35;
            Option<int> result = optionValue.Filter(_isEven);

            Assert.IsTrue(result.IsNone);
        }

        [TestMethod]
        public void LinqOptionFilterWhenTrue()
        {
            int expected = 36;
            Option<int> optionValue = expected;
            Option<int> optionResult = optionValue.Filter(_isEven);
            int result = optionResult.Match(value => value, () => 0);


            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqOptionExistsWhenNone()
        {
            bool expected = false;
            Option<int> optionValue = Option<int>.None();
            bool result = optionValue.Exists(_isEven);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqOptionExistsWhenFalse()
        {
            bool expected = false;
            Option<int> optionValue = 35;
            bool result = optionValue.Exists(_isEven);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqOptionExistsWhenTrue()
        {
            bool expected = true;
            Option<int> optionValue = 36;
            bool result = optionValue.Exists(_isEven);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqOptionMapWhenNone()
        {
            Option<int> optionValue = Option<int>.None();
            Option<bool> optionResult = optionValue.Map(_isEven);

            Assert.IsTrue(optionResult.IsNone);
        }

        [TestMethod]
        public void LinqOptionMapWhenSomeFalse()
        {
            string expected = "Odd";
            Option<int> optionValue = 35;
            Option<string> optionResult = optionValue.Map((value) => _isEven(value) ? "Even" : "Odd");
            string result = optionResult.Match(value => value, () => string.Empty);


            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqOptionMapWhenSomeTrue()
        {
            Option<int> optionValue = 36;
            Option<bool> optionResult = optionValue.Map(_isEven);
            bool result = optionResult.Match(value => value, () => false);


            Assert.IsTrue(optionResult.IsSome && result);
        }

        [TestMethod]
        public void LinqOptionApplyWhenSomeFunctionSomeValue()
        {
            Option<Func<int, bool>> optionFunction = _isEven;
            Option<int> optionValue = 36;
            Option<bool> optionResult =
                optionValue.Apply(optionFunction);

            bool result = optionResult.Match(value => value, () => false);
            Assert.IsTrue(optionResult.IsSome && result);
        }

        [TestMethod]
        public void LinqOptionApplyWhenSomeFunctionNoneValue()
        {
            Option<Func<int, bool>> optionFunction = _isEven;
            Option<int> optionValue = Option<int>.None();
            Option<bool> optionResult =
                optionValue.Apply(optionFunction);

            bool result = optionResult.Match(value => value, () => false);
            Assert.IsTrue(optionResult.IsNone);
        }

        [TestMethod]
        public void LinqOptionApplyWhenNoneFunctionSomeValue()
        {
            Option<Func<int, bool>> optionFunction = Option<Func<int, bool>>.None();
            Option<int> optionValue = 36;
            Option<bool> optionResult =
                optionValue.Apply(optionFunction);

            bool result = optionResult.Match(value => value, () => false);
            Assert.IsTrue(optionResult.IsNone);
        }

        [TestMethod]
        public void LinqOptionApplyWhenNoneFunctionNoneValue()
        {
            Option<Func<int, bool>> optionFunction = Option<Func<int, bool>>.None();
            Option<int> optionValue = Option<int>.None();
            Option<bool> optionResult =
                optionValue.Apply(optionFunction);

            bool result = optionResult.Match(value => value, () => false);
            Assert.IsTrue(optionResult.IsNone);
        }

        [TestMethod]
        public void LinqOptionBindWhenNone()
        {
            Option<int> optionValue = Option<int>.None();
            Option<int> optionResult = optionValue.Bind(_squareWhenEven);

            Assert.IsTrue(optionResult.IsNone);
        }

        [TestMethod]
        public void LinqOptionBindWhenSomeFalse()
        {
            int expected = 0;
            Option<int> optionValue = 5;
            Option<int> optionResult = optionValue.Bind(_squareWhenEven);
            int result = optionResult.Match(value => value, () => 0);


            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqOptionBindWhenSomeTrue()
        {
            int expected = 36;
            Option<int> optionValue = 6;
            Option<int> optionResult = optionValue.Bind(_squareWhenEven);
            int result = optionResult.Match(value => value, () => 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqOptionFoldWhenNone()
        {
            int state = 10;
            int expected = state;
            Option<int> optionValue = Option<int>.None();
            Option<int> optionResult = optionValue.Fold(state, (_state, value) => _state + value);
            int result = optionResult.Match(value => value, () => 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqOptionFoldWhenSome()
        {
            int initial = 25;
            int state = 10;
            int expected = state + initial;
            Option<int> optionValue = initial;
            Option<int> optionResult = optionValue.Fold(state, (_state, value) => _state + value);
            int result = optionResult.Match(value => value, () => 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqOptionFoldBackWhenNone()
        {
            int state = 10;
            int expected = state;
            Option<int> optionValue = Option<int>.None();
            Option<int> optionResult = optionValue.FoldBack((value, _state) => _state + value, state);
            int result = optionResult.Match(value => value, () => 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqOptionFoldBackWhenSome()
        {
            int initial = 25;
            int state = 10;
            int expected = state + initial;
            Option<int> optionValue = initial;
            Option<int> optionResult = optionValue.FoldBack((value, _state) => _state + value, state);
            int result = optionResult.Match(value => value, () => 0);

            Assert.AreEqual(expected, result);
        }

    }
}
