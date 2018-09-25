using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tango.Modules;
using Tango.Types;
using Tango.Linq;

namespace Tango.Test.Modules
{
    [TestClass]
    public class EitherLinqTests

    {
        Func<int, bool> _isEven = element => element % 2 == 0;

        [TestMethod]
        public void LinqEitherToTupleWhenSomeLeft()
        {
            string expected = "Test";
            Either<string, int> either = expected;
            (Option<string> Left, Option<int> Right) tuple = either.ToTuple();

            string result = tuple.Left.Match(value => value, () => string.Empty);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherToTupleWhenSomeRight()
        {
            int expected = 10;
            Either<string, int> either = expected;
            (Option<string> Left, Option<int> Right) tuple = either.ToTuple();

            int result = tuple.Right.Match(value => value, () => 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherToTupleWhenNoneRight()
        {
            int expected = 0;
            Either<string, int> either = "Test";
            (Option<string> Left, Option<int> Right) tuple = either.ToTuple();

            int result = tuple.Right.Match(value => value, () => 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherToTupleWhenNoneLeft()
        {
            string expected = string.Empty;
            Either<string, int> either = 10;
            (Option<string> Left, Option<int> Right) tuple = either.ToTuple();

            string result = tuple.Left.Match(value => value, () => string.Empty);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherIterateRightWhenLeft()
        {
            int expected = 10;
            int result = 10;
            Either<string, int> either = "Hello";
            either.IterateRight(right => result += right);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherIterateRightWhenRight()
        {
            int expected = 20;
            int result = 10;
            Either<string, int> either = 10;
            either.IterateRight(right => result += right );

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherIterateLeftWhenLeft()
        {
            string expected = "Hello World";
            string result = "World";
            Either<string, int> either = "Hello ";
            either.IterateLeft(left => result = string.Concat(left, result) );

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherIterateLeftWhenRight()
        {
            string expected = "World";
            string result = "World";
            Either<string, int> either = 10;
            either.IterateLeft(left => result = string.Concat(left, result));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherIterateWhenRight()
        {
            int expected = 20;
            int result = 10;
            Either<string, int> either = 10;
            either.Iterate(
                right => result += right,
                left => string.Concat(left, left));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherIterateWhenLeft()
        {
            string expected = "Hello World";
            string result = "World";
            Either<string, int> either = "Hello ";
            either.Iterate(
                right => right += right * 2,
                left => result = string.Concat(left, result));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherExistsRightFalseWhenRight()
        {
            bool expected = false;
            Either<string, int> either = 20;
            bool result =
                either.ExistsRight(
                left => left == 10 );

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherExistsRightTrueWhenRight()
        {
            bool expected = true;
            Either<string, int> either = 10;
            bool result =
                either.ExistsRight(left => left == 10);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherExistsRightFalseWhenLeft()
        {
            bool expected = false;
            Either<string, int> either = "Hello";
            bool result =
                either.ExistsRight(left => left == 10);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherExistsLeftFalseWhenLeft()
        {
            bool expected = false;
            Either<string, int> either = "Hello";
            bool result =
                either.ExistsLeft(
                right => right == "World");

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherExistsLeftTrueWhenLeft()
        {
            bool expected = true;
            Either<string, int> either = "Hello";
            bool result =
                either.ExistsLeft(
                right => right == "Hello");

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherExistsLeftFalseWhenRight()
        {
            bool expected = false;
            Either<string, int> either = 10;
            bool result =
                either.ExistsLeft(
                right => right == "Hello"
                );

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherExistsTrueWhenRight()
        {
            bool expected = true;
            Either<string, int> either = 10;
            bool result =
                either.Exists(
                right => right == 10,
                left => left == "Test"
                );

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherExistsFalseWhenRight()
        {
            bool expected = false;
            Either<string, int> either = 20;
            bool result =
                either.Exists(
                right => right == 10,
                left => left == "Test"
                );

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherExistsTrueWhenLeft()
        {
            bool expected = true;
            Either<string, int> either = "Test";
            bool result =
                either.Exists(
                right => right == 10,
                left => left == "Test");

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherExistsFalseWhenLeft()
        {
            bool expected = false;
            Either<string, int> either = "Hello World";
            bool result =
                either.Exists(
                right => right == 10,
                left => left == "Test"
                );

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherMapRightWhenRight()
        {
            bool expected = true;
            Either<string, int> either = 10;
            Either<string, bool> eitherResult = either.MapRight(_isEven);

            bool result = eitherResult.Match(right => right, left => false);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherMapRightWhenLeft()
        {
            bool expected = false;
            Either<string, int> either = "10";
            Either<string, bool> eitherResult = either.MapRight(_isEven);

            bool result = eitherResult.Match(right => right, left => false);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherMapLeftWhenRight()
        {
            double expected = 0;
            Either<string, int> either = 10;
            Either<double, int> eitherResult = either.MapLeft(left => Convert.ToDouble(left));

            double result = eitherResult.Match(right => 0, left => left);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherMapLeftWhenLeft()
        {
            double expected = 10;
            Either<string, int> either = "10";
            Either<double, int> eitherResult = either.MapLeft(left => Convert.ToDouble(left));

            double result = eitherResult.Match(right => 0, left => left);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherMapWhenRight()
        {
            bool expected = true;
            Either<string, int> either = 10;
            Either<int, bool> eitherResult =
            either.Map(
                _isEven,
                left => Convert.ToInt32(left));

            bool result = eitherResult.Match(right => right, left => false);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherMapWhenLeft()
        {
            int expected = 10;
            Either<string, int> either = "10";
            Either<int, bool> eitherResult =
            either.Map(
                _isEven,
                left => Convert.ToInt32(left));

            int result = eitherResult.Match(right => 0, left => left);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherFoldRightWhenRight()
        {
            int state = 10;
            int expected = 20;
            Either<string, int> either = 10;
            int result =
            either.FoldRight(
                state,
                (_state, right) => right + _state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherFoldRightWhenLeft()
        {
            int state = 10;
            int expected = 10;
            Either<string, int> either = "10";
            int result =
            either.FoldRight(
                state,
                (_state, right) => right + _state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherFoldLeftWhenRight()
        {
            string state = "Hello";
            string expected = "Hello";
            Either<string, int> either = 10;
            string result =
            either.FoldLeft(
                state,
                (_state, left) => string.Concat(_state, left));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherFoldLeftWhenLeft()
        {
            string state = "Hello";
            string expected = "Hello World";
            Either<string, int> either = " World";
            string result =
            either.FoldLeft(
                state,
                (_state, left) => string.Concat(_state, left));

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void LinqEitherFoldWhenRight()
        {
            int state = 10;
            int expected = 20;
            Either<string, int> either = 10;
            int result =
            either.Fold(
                state,
                (_state, right) => right + _state,
                (_state, left) => _state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherFoldWhenLeft()
        {
            int state = 10;
            int expected = 10;
            Either<string, int> either = "10";
            int result =
            either.Fold(
                state,
                (_state, right) => right + _state,
                (_state, left) => _state);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void LinqEitherFoldBackRightWhenRight()
        {
            int state = 10;
            int expected = 20;
            Either<string, int> either = 10;
            int result =
            either.FoldBackRight(
                (right, _state) => right + _state,
                state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherFoldBackRightWhenLeft()
        {
            int state = 10;
            int expected = 10;
            Either<string, int> either = "10";
            int result =
            either.FoldBackRight(
                (right, _state) => right + _state,
                 state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherFoldBackLeftWhenRight()
        {
            string state = "Hello";
            string expected = "Hello";
            Either<string, int> either = 10;
            string result =
            either.FoldBackLeft(
                (right, _state) => string.Concat(_state, right), state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherFoldBackLeftWhenLeft()
        {
            string state = "Hello";
            string expected = "Hello World";
            Either<string, int> either = " World";
            string result =
            either.FoldBackLeft(
                (left, _state) => string.Concat(_state, left),
                state);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void LinqEitherFoldBackWhenRight()
        {
            int state = 10;
            int expected = 20;
            Either<string, int> either = 10;
            int result =
            either.FoldBack(
                (right, _state) => right + _state,
                (left, _state) => _state,
                state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherFoldBackWhenLeft()
        {
            int state = 10;
            int expected = 10;
            Either<string, int> either = "10";
            int result =
            either.FoldBack(
                (right, _state) => right + _state,
                (left, _state) => _state,
                state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherSwapWhenRight()
        {
            int expected = 10;
            Either<string, int> either = 10;
            Either<int, string> swappedEither = either.Swap();

            int result = swappedEither.Match(right => 0, left => left);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherSwapWhenLeft()
        {
            int expected = 0;
            Either<string, int> either = "Hello";
            Either<int, string> swappedEither = either.Swap();

            int result = swappedEither.Match(right => 0, left => left);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherSwapWhenLeft2()
        {
            string expected = "Hello";
            Either<string, int> either = expected;
            Either<int, string> swappedEither = either.Swap();

            string result = swappedEither.Match(right => right, left => string.Empty);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherSwapWhenRight2()
        {
            string expected = string.Empty;
            Either<string, int> either = 10;
            Either<int, string> swappedEither = either.Swap();

            string result = swappedEither.Match(right => right, left => string.Empty);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherToOptioRightWhenRight()
        {
            int expected = 10;
            Either<string, int> either = 10;
            Option<int> optionResult = either.ToOptionRight();

            int result = optionResult.Match(value => value, () => 0);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherToOptionRightWhenLeft()
        {
            int expected = 0;
            Either<string, int> either = "Hello World";
            Option<int> optionResult = either.ToOptionRight();

            int result = optionResult.Match(value => value, () => 0);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherToOptionLeftWhenRight()
        {
            string expected = string.Empty;
            Either<string, int> either = 10;
            Option<string> optionResult = either.ToOptionLeft();

            string result = optionResult.Match(value => value, () => string.Empty);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqEitherToOptionLeftWhenLeft()
        {
            string expected = "Hello World";
            Either<string, int> either = expected;
            Option<string> optionResult = either.ToOptionLeft();

            string result = optionResult.Match(value => value, () => string.Empty);
            Assert.AreEqual(expected, result);
        }




    }
}
