using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tango.Modules;
using Tango.Types;
using Tango.Linq;

namespace Tango.Test.Types
{
    [TestClass]
    public class EitherLinqTests

    {
        Func<int, bool> _isEven = element => element % 2 == 0;

        [TestMethod]
        public void LinqEitherToTupleWhenSomeLeft()
        {
            int expected = 10;
            Either<int, string> either = expected;
            (Option<int> Left, Option<string> Right) tuple = either.ToTuple();

            int result = tuple.Left.Match(value => value, () => 0);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherToTupleWhenSomeRight()
        {
            string expected = "Test";
            Either<int, string> either = expected;
            (Option<int> Left, Option<string> Right) tuple = either.ToTuple();

            string result = tuple.Right.Match(value => value, () => string.Empty);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherToTupleWhenNoneRight()
        {
            string expected = string.Empty;
            Either<int, string> either = 10;
            (Option<int> Left, Option<string> Right) tuple = either.ToTuple();

            string result = tuple.Right.Match(value => value, () => string.Empty);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherToTupleWhenNoneLeft()
        {
            int expected = 0;
            Either<int, string> either = "Test";
            (Option<int> Left, Option<string> Right) tuple = either.ToTuple();

            int result = tuple.Left.Match(value => value, () => 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherIterateLeftWhenLeft()
        {
            int expected = 20;
            int result = 10;
            Either<int, string> either = 10;
            either.IterateLeft(left => result += left);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherIterateLeftWhenRight()
        {
            int expected = 10;
            int result = 10;
            Either<int, string> either = "Hello";
            either.IterateLeft(
                left => result += left);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherIterateRightWhenLeft()
        {
            string expected = "World";
            string result = "World";
            Either<int, string> either = 10;
            either.IterateRight(
                right => result = string.Concat(right, result));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherIterateRightWhenRight()
        {
            string expected = "Hello World";
            string result = "World";
            Either<int, string> either = "Hello ";
            either.IterateRight(
                right => result = string.Concat(right, result));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherIterateWhenLeft()
        {
            int expected = 20;
            int result = 10;
            Either<int, string> either = 10;
            either.Iterate(
                left => result += left,
                right => string.Concat(right, right));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherIterateWhenRight()
        {
            string expected = "Hello World";
            string result = "World";
            Either<int, string> either = "Hello ";
            either.Iterate(
                left => left += left * 2,
                right => result = string.Concat(right, result));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherExistsTrueWhenLeft()
        {
            bool expected = true;
            Either<int, string> either = 10;
            bool result =
                EitherModule.Exists(
                left => left == 10,
                right => right == "Test"
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherExistsLeftFalseWhenLeft()
        {
            bool expected = false;
            Either<int, string> either = 20;
            bool result = either.ExistsLeft(left => left == 10);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherExistsLeftTrueWhenLeft()
        {
            bool expected = true;
            Either<int, string> either = 10;
            bool result = either.ExistsLeft(left => left == 10);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherExistsLeftFalseWhenRight()
        {
            bool expected = false;
            Either<int, string> either = "Hello";
            bool result = either.ExistsLeft(left => left == 10);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherExistsRightFalseWhenRight()
        {
            bool expected = false;
            Either<int, string> either = "Hello";
            bool result = either.ExistsRight(right => right == "World");

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherExistsRightTrueWhenRight()
        {
            bool expected = true;
            Either<int, string> either = "Hello";
            bool result = either.ExistsRight(right => right == "Hello");

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherExistsRightFalseWhenLeft()
        {
            bool expected = false;
            Either<int, string> either = 10;
            bool result = either.ExistsRight(right => right == "Hello");

            Assert.AreEqual(expected, result);
        }



        [TestMethod]
        public void LinqEitherExistsFalseWhenLeft()
        {
            bool expected = false;
            Either<int, string> either = 20;
            bool result =
                either.Exists(
                left => left == 10,
                right => right == "Test");

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherExistsTrueWhenRight()
        {
            bool expected = true;
            Either<int, string> either = "Test";
            bool result =
                either.Exists(
                left => left == 10,
                right => right == "Test");

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherExistsFalseWhenRight()
        {
            bool expected = false;
            Either<int, string> either = "Hello World";
            bool result = either.Exists(
                left => left == 10,
                right => right == "Test");

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherMapLeftWhenLeft()
        {
            bool expected = true;
            Either<int, string> either = 10;
            Either<bool, string> eitherResult = either.MapLeft(_isEven);

            bool result = eitherResult.Match(left => left, right => false);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherMapLeftWhenRight()
        {
            bool expected = false;
            Either<int, string> either = "10";
            Either<bool, string> eitherResult = either.MapLeft(_isEven);

            bool result = eitherResult.Match(left => left, right => false);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherMapRightWhenLeft()
        {
            double expected = 0;
            Either<int, string> either = 10;
            Either<int, double> eitherResult =
                either.MapRight(right => Convert.ToDouble(right));

            double result = eitherResult.Match(left => 0, right => right);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherMapRightWhenRight()
        {
            double expected = 10;
            Either<int, string> either = "10";
            Either<int, double> eitherResult =
                either.MapRight(right => Convert.ToDouble(right));

            double result = eitherResult.Match(left => 0, right => right);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherMapWhenLeft()
        {
            bool expected = true;
            Either<int, string> either = 10;
            Either<bool, int> eitherResult =
                either.Map(
                    _isEven,
                    right => Convert.ToInt32(right));

            bool result = eitherResult.Match(left => left, right => false);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherMapWhenRight()
        {
            int expected = 10;
            Either<int, string> either = "10";
            Either<bool, int> eitherResult =
                either.Map( _isEven, right => Convert.ToInt32(right));

            int result = eitherResult.Match(left => 0, right => right);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherFoldLeftWhenLeft()
        {
            int state = 10;
            int expected = 20;
            Either<int, string> either = 10;
            int result = either.FoldLeft(state, (_state, left) => left + _state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherFoldLeftWhenRight()
        {
            int state = 10;
            int expected = 10;
            Either<int, string> either = "10";
            int result = either.FoldLeft(state,(_state, left) => left + _state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherFoldRightWhenLeft()
        {
            string state = "Hello";
            string expected = "Hello";
            Either<int, string> either = 10;
            string result =
                either.FoldRight(state, (_state, right) => string.Concat(_state, right));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherFoldRightWhenRight()
        {
            string state = "Hello";
            string expected = "Hello World";
            Either<int, string> either = " World";
            string result =
            either.FoldRight(
                state,
                (_state, right) => string.Concat(_state, right));

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void LinqEitherFoldWhenLeft()
        {
            int state = 10;
            int expected = 20;
            Either<int, string> either = 10;
            int result =
            either.Fold(
                state,
                (_state, left) => left + _state,
                (_state, right) => _state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherFoldWhenRight()
        {
            int state = 10;
            int expected = 10;
            Either<int, string> either = "10";
            int result =
            either.Fold(
                state,
                (_state, left) => left + _state,
                (_state, right) => _state);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void LinqEitherFoldBackLeftWhenLeft()
        {
            int state = 10;
            int expected = 20;
            Either<int, string> either = 10;
            int result =
            either.FoldBackLeft(
                (left, _state) => left + _state,
                state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherFoldBackLeftWhenRight()
        {
            int state = 10;
            int expected = 10;
            Either<int, string> either = "10";
            int result =
            either.FoldBackLeft(
                (left, _state) => left + _state,
                state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherFoldBackRightWhenLeft()
        {
            string state = "Hello";
            string expected = "Hello";
            Either<int, string> either = 10;
            string result =
            either.FoldBackRight(
                (right, _state) => string.Concat(_state, right),
                state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherFoldBackRightWhenRight()
        {
            string state = "Hello";
            string expected = "Hello World";
            Either<int, string> either = " World";
            string result =
            either.FoldBackRight(
                (right, _state) => string.Concat(_state, right),
                state);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void LinqEitherFoldBackWhenLeft()
        {
            int state = 10;
            int expected = 20;
            Either<int, string> either = 10;
            int result =
            either.FoldBack(
                (left, _state) => left + _state,
                (right, _state) => _state,
                state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherFoldBackWhenRight()
        {
            int state = 10;
            int expected = 10;
            Either<int, string> either = "10";
            int result =
            either.FoldBack(
                (left, _state) => left + _state,
                (right, _state) => _state,
                state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherSwapWhenLeft()
        {
            int expected = 10;
            Either<int, string> either = 10;
            Either<string, int> swappedEither = either.Swap();

            int result = swappedEither.Match(left => 0, right => right);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherSwapWhenLeft2()
        {
            int expected = 0;
            Either<int, string> either = "Hello";
            Either<string, int> swappedEither = either.Swap();

            int result = swappedEither.Match(left => 0, right => right);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherSwapWhenRight()
        {
            string expected = "Hello";
            Either<int, string> either = expected;
            Either<string, int> swappedEither = either.Swap();

            string result = swappedEither.Match(left => left, right => string.Empty);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherSwapWhenRight2()
        {
            string expected = string.Empty;
            Either<int, string> either = 10;
            Either<string, int> swappedEither = either.Swap();

            string result = swappedEither.Match(left => left, right => string.Empty);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherToOptionLeftWhenLeft()
        {
            int expected = 10;
            Either<int, string> either = 10;
            Option<int> optionResult = either.ToOptionLeft();

            int result = optionResult.Match(value => value, () => 0);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherToOptionLeftWhenRight()
        {
            int expected = 0;
            Either<int, string> either = "Hello World";
            Option<int> optionResult = either.ToOptionLeft();

            int result = optionResult.Match(value => value, () => 0);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherToOptionRightWhenLeft()
        {
            string expected = string.Empty;
            Either<int, string> either = 10;
            Option<string> optionResult = either.ToOptionRight();

            string result = optionResult.Match(value => value, () => string.Empty);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherToOptionRightWhenRight()
        {
            string expected = "Hello World";
            Either<int, string> either = expected;
            Option<string> optionResult = either.ToOptionRight();

            string result = optionResult.Match(value => value, () => string.Empty);
            Assert.AreEqual(expected, result);
        }


    }
}
