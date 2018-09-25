using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tango.Modules;
using Tango.Types;

namespace Tango.Test.Modules
{
    [TestClass]
    public class EitherModuleTests

    {
        Func<int, bool> _isEven = element => element % 2 == 0;

        [TestMethod]
        public void EitherToTupleWhenSomeLeft()
        {
            string expected = "Test";
            Either<string, int> either = expected;
            (Option<string> Left, Option<int> Right) tuple = EitherModule.ToTuple(either);

            string result = tuple.Left.Match(value => value, () => string.Empty);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherToTupleWhenSomeRight()
        {
            int expected = 10;
            Either<string, int> either = expected;
            (Option<string> Left, Option<int> Right) tuple = EitherModule.ToTuple(either);

            int result = tuple.Right.Match(value => value, () => 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherToTupleWhenNoneRight()
        {
            int expected = 0;
            Either<string, int> either = "Test";
            (Option<string> Left, Option<int> Right) tuple = EitherModule.ToTuple(either);

            int result = tuple.Right.Match(value => value, () => 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherToTupleWhenNoneLeft()
        {
            string expected = string.Empty;
            Either<string, int> either = 10;
            (Option<string> Left, Option<int> Right) tuple = EitherModule.ToTuple(either);

            string result = tuple.Left.Match(value => value, () => string.Empty);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherIterateRightWhenLeft()
        {
            int expected = 10;
            int result = 10;
            Either<string, int> either = "Hello";
            EitherModule.IterateRight(
                right => result += right
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherIterateRightWhenRight()
        {
            int expected = 20;
            int result = 10;
            Either<string, int> either = 10;
            EitherModule.IterateRight(
                right => result += right
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherIterateLeftWhenLeft()
        {
            string expected = "Hello World";
            string result = "World";
            Either<string,int> either = "Hello ";
            EitherModule.IterateLeft(
                left => result = string.Concat(left, result)
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherIterateLeftWhenRight()
        {
            string expected = "World";
            string result = "World";
            Either<string, int> either = 10;
            EitherModule.IterateLeft(
                left => result = string.Concat(left, result)
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherIterateWhenRight()
        {
            int expected = 20;
            int result = 10;
            Either<string, int> either = 10;
            EitherModule.Iterate(
                right => result += right,
                left => string.Concat(left, left)
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherIterateWhenLeft()
        {
            string expected = "Hello World";
            string result = "World";
            Either<string, int> either = "Hello ";
            EitherModule.Iterate(
                right => right += right * 2,
                left => result = string.Concat(left, result)
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherExistsRightFalseWhenRight()
        {
            bool expected = false;
            Either<string, int> either = 20;
            bool result =
                EitherModule.ExistsRight(
                left => left == 10
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherExistsRightTrueWhenRight()
        {
            bool expected = true;
            Either<string, int> either = 10;
            bool result =
                EitherModule.ExistsRight(
                left => left == 10
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherExistsRightFalseWhenLeft()
        {
            bool expected = false;
            Either<string, int> either = "Hello";
            bool result =
                EitherModule.ExistsRight(
                left => left == 10
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherExistsLeftFalseWhenLeft()
        {
            bool expected = false;
            Either<string, int> either = "Hello";
            bool result =
                EitherModule.ExistsLeft(
                right => right == "World"
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherExistsLeftTrueWhenLeft()
        {
            bool expected = true;
            Either<string, int> either = "Hello";
            bool result =
                EitherModule.ExistsLeft(
                right => right == "Hello"
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherExistsLeftFalseWhenRight()
        {
            bool expected = false;
            Either<string, int> either = 10;
            bool result =
                EitherModule.ExistsLeft(
                right => right == "Hello"
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherExistsTrueWhenRight()
        {
            bool expected = true;
            Either<string, int> either = 10;
            bool result =
                EitherModule.Exists(
                right => right == 10,
                left => left == "Test"
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherExistsFalseWhenRight()
        {
            bool expected = false;
            Either<string, int> either = 20;
            bool result =
                EitherModule.Exists(
                right => right == 10,
                left => left == "Test"
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherExistsTrueWhenLeft()
        {
            bool expected = true;
            Either<string, int> either = "Test";
            bool result =
                EitherModule.Exists(
                right => right == 10,
                left => left == "Test"
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherExistsFalseWhenLeft()
        {
            bool expected = false;
            Either<string, int> either = "Hello World";
            bool result =
                EitherModule.Exists(
                right => right == 10,
                left => left == "Test"
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherMapRightWhenRight()
        {
            bool expected = true;
            Either<string, int> either = 10;
            Either<string, bool> eitherResult =
            EitherModule.MapRight(
                _isEven,
                either);

            bool result = eitherResult.Match(right => right, left => false);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherMapRightWhenLeft()
        {
            bool expected = false;
            Either<string, int> either = "10";
            Either<string, bool> eitherResult =
            EitherModule.MapRight(
                _isEven,
                either);

            bool result = eitherResult.Match(right => right, left => false);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherMapLeftWhenRight()
        {
            double expected = 0;
            Either<string, int> either = 10;
            Either<double, int> eitherResult =
            EitherModule.MapLeft(
                left => Convert.ToDouble(left),
                either);

            double result = eitherResult.Match(right => 0, left=> left);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherMapLeftWhenLeft()
        {
            double expected = 10;
            Either<string, int> either = "10";
            Either<double, int> eitherResult =
            EitherModule.MapLeft(
                left => Convert.ToDouble(left),
                either);

            double result = eitherResult.Match(right => 0 , left => left);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherMapWhenRight()
        {
            bool expected = true;
            Either<string, int> either = 10;
            Either<int, bool> eitherResult =
            EitherModule.Map(
                _isEven,
                left => Convert.ToInt32(left),
                either);

            bool result = eitherResult.Match(right => right, left => false);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherMapWhenLeft()
        {
            int expected = 10;
            Either<string, int> either = "10";
            Either<int, bool> eitherResult =
            EitherModule.Map(
                _isEven,
                left => Convert.ToInt32(left),
                either);

            int result = eitherResult.Match(right => 0, left => left);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherFoldRightWhenRight()
        {
            int state = 10;
            int expected = 20;
            Either<string, int> either = 10;
            int result =
            EitherModule.FoldRight(
                (_state, right) => right + _state,
                state,
                either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherFoldRightWhenLeft()
        {
            int state = 10;
            int expected = 10;
            Either<string, int> either = "10";
            int result =
            EitherModule.FoldRight(
                (_state, right) => right + _state,
                state,
                either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherFoldLeftWhenRight()
        {
            string state = "Hello";
            string expected = "Hello";
            Either<string, int> either = 10;
            string result =
            EitherModule.FoldLeft(
                (_state, left) => string.Concat(_state, left),
                state,
                either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherFoldLeftWhenLeft()
        {
            string state = "Hello";
            string expected = "Hello World";
            Either<string, int> either = " World";
            string result =
            EitherModule.FoldLeft(
                (_state, left) => string.Concat(_state, left),
                state,
                either);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void EitherFoldWhenRight()
        {
            int state = 10;
            int expected = 20;
            Either<string, int> either = 10;
            int result =
            EitherModule.Fold(
                (_state, right) => right + _state,
                (_state, left) => _state,
                state,
                either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherFoldWhenLeft()
        {
            int state = 10;
            int expected = 10;
            Either<string, int> either = "10";
            int result =
            EitherModule.Fold(
                (_state, right) => right + _state,
                (_state, left) => _state,
                state,
                either);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void EitherFoldBackRightWhenRight()
        {
            int state = 10;
            int expected = 20;
            Either<string, int> either = 10;
            int result =
            EitherModule.FoldBackRight(
                (right, _state) => right + _state,
                either, state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherFoldBackRightWhenLeft()
        {
            int state = 10;
            int expected = 10;
            Either<string, int> either = "10";
            int result =
            EitherModule.FoldBackRight(
                (right, _state) => right + _state,
                either, state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherFoldBackLeftWhenRight()
        {
            string state = "Hello";
            string expected = "Hello";
            Either<string, int> either = 10;
            string result =
            EitherModule.FoldBackLeft(
                (right, _state) => string.Concat(_state, right),
                either, state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherFoldBackLeftWhenLeft()
        {
            string state = "Hello";
            string expected = "Hello World";
            Either<string, int> either = " World";
            string result =
            EitherModule.FoldBackLeft(
                (left, _state) => string.Concat(_state, left),
                either, state);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void EitherFoldBackWhenRight()
        {
            int state = 10;
            int expected = 20;
            Either<string, int> either = 10;
            int result =
            EitherModule.FoldBack(
                (right, _state) => right + _state,
                (left, _state) => _state,
                either,
                state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherFoldBackWhenLeft()
        {
            int state = 10;
            int expected = 10;
            Either<string, int> either = "10";
            int result =
            EitherModule.FoldBack(
                (right, _state) => right + _state,
                (left, _state) => _state,
                either,
                state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherSwapWhenRight()
        {
            int expected = 10;
            Either<string, int> either = 10;
            Either<int, string> swappedEither = 
                EitherModule.Swap(either);

            int result = swappedEither.Match(right => 0, left => left);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherSwapWhenLeft()
        {
            int expected = 0;
            Either<string, int> either = "Hello";
            Either<int, string> swappedEither =
                EitherModule.Swap(either);

            int result = swappedEither.Match(right => 0, left => left);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherSwapWhenLeft2()
        {
            string expected = "Hello";
            Either<string, int> either = expected;
            Either<int, string> swappedEither =
                EitherModule.Swap(either);

            string result = swappedEither.Match(right => right, left => string.Empty);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherSwapWhenRight2()
        {
            string expected = string.Empty;
            Either<string, int> either = 10;
            Either<int, string> swappedEither =
                EitherModule.Swap(either);

            string result = swappedEither.Match(right => right, left => string.Empty);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherToOptioRightWhenRight()
        {
            int expected = 10;
            Either<string, int> either = 10;
            Option<int> optionResult =
                EitherModule.ToOptionRight(either);

            int result = optionResult.Match(value => value, () => 0);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherToOptionRightWhenLeft()
        {
            int expected = 0;
            Either<string, int> either = "Hello World";
            Option<int> optionResult =
                EitherModule.ToOptionRight(either);

            int result = optionResult.Match(value => value, () => 0);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherToOptionLeftWhenRight()
        {
            string expected = string.Empty;
            Either<string, int> either = 10;
            Option<string> optionResult =
                EitherModule.ToOptionLeft(either);

            string result = optionResult.Match(value => value, () => string.Empty);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherToOptionLeftWhenLeft()
        {
            string expected = "Hello World";
            Either<string, int> either = expected;
            Option<string> optionResult =
                EitherModule.ToOptionLeft(either);

            string result = optionResult.Match(value => value, () => string.Empty);
            Assert.AreEqual(expected, result);
        }


    }
}
