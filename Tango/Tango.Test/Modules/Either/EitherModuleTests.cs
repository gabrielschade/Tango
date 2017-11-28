using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tango.Modules;
using Tango.Types;

namespace Tango.Test.Types
{
    [TestClass]
    public class EitherModuleTests

    {
        Func<int, bool> _isEven = element => element % 2 == 0;

        [TestMethod]
        public void EitherToTupleWhenSomeLeft()
        {
            int expected = 10;
            Either<int, string> either = expected;
            (Option<int> Left, Option<string> Right) tuple = EitherModule.ToTuple(either);

            int result = tuple.Left.Match(value => value, () => 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherToTupleWhenSomeRight()
        {
            string expected = "Test";
            Either<int, string> either = expected;
            (Option<int> Left, Option<string> Right) tuple = EitherModule.ToTuple(either);

            string result = tuple.Right.Match(value => value, () => string.Empty);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherToTupleWhenNoneRight()
        {
            string expected = string.Empty;
            Either<int, string> either = 10;
            (Option<int> Left, Option<string> Right) tuple = EitherModule.ToTuple(either);

            string result = tuple.Right.Match(value => value, () => string.Empty);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherToTupleWhenNoneLeft()
        {
            int expected = 0;
            Either<int, string> either = "Test";
            (Option<int> Left, Option<string> Right) tuple = EitherModule.ToTuple(either);

            int result = tuple.Left.Match(value => value, () => 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherIterateLeftWhenLeft()
        {
            int expected = 20;
            int result = 10;
            Either<int, string> either = 10;
            EitherModule.IterateLeft(
                left => result += left
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherIterateLeftWhenRight()
        {
            int expected = 10;
            int result = 10;
            Either<int, string> either = "Hello";
            EitherModule.IterateLeft(
                left => result += left
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherIterateRightWhenLeft()
        {
            string expected = "World";
            string result = "World";
            Either<int, string> either = 10;
            EitherModule.IterateRight(
                right => result = string.Concat(right, result)
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherIterateRightWhenRight()
        {
            string expected = "Hello World";
            string result = "World";
            Either<int, string> either = "Hello ";
            EitherModule.IterateRight(
                right => result = string.Concat(right, result)
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherIterateWhenLeft()
        {
            int expected = 20;
            int result = 10;
            Either<int, string> either = 10;
            EitherModule.Iterate(
                left => result += left,
                right => string.Concat(right, right)
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherIterateWhenRight()
        {
            string expected = "Hello World";
            string result = "World";
            Either<int, string> either = "Hello ";
            EitherModule.Iterate(
                left => left += left * 2,
                right => result = string.Concat(right, result)
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherExistsTrueWhenLeft()
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
        public void EitherExistsLeftFalseWhenLeft()
        {
            bool expected = false;
            Either<int, string> either = 20;
            bool result =
                EitherModule.ExistsLeft(
                left => left == 10
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherExistsLeftTrueWhenLeft()
        {
            bool expected = true;
            Either<int, string> either = 10;
            bool result =
                EitherModule.ExistsLeft(
                left => left == 10
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherExistsLeftFalseWhenRight()
        {
            bool expected = false;
            Either<int, string> either = "Hello";
            bool result =
                EitherModule.ExistsLeft(
                left => left == 10
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherExistsRightFalseWhenRight()
        {
            bool expected = false;
            Either<int, string> either = "Hello";
            bool result =
                EitherModule.ExistsRight(
                right => right == "World"
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherExistsRightTrueWhenRight()
        {
            bool expected = true;
            Either<int, string> either = "Hello";
            bool result =
                EitherModule.ExistsRight(
                right => right == "Hello"
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherExistsRightFalseWhenLeft()
        {
            bool expected = false;
            Either<int, string> either = 10;
            bool result =
                EitherModule.ExistsRight(
                right => right == "Hello"
                , either);

            Assert.AreEqual(expected, result);
        }



        [TestMethod]
        public void EitherExistsFalseWhenLeft()
        {
            bool expected = false;
            Either<int, string> either = 20;
            bool result =
                EitherModule.Exists(
                left => left == 10,
                right => right == "Test"
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherExistsTrueWhenRight()
        {
            bool expected = true;
            Either<int, string> either = "Test";
            bool result =
                EitherModule.Exists(
                left => left == 10,
                right => right == "Test"
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherExistsFalseWhenRight()
        {
            bool expected = false;
            Either<int, string> either = "Hello World";
            bool result =
                EitherModule.Exists(
                left => left == 10,
                right => right == "Test"
                , either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherMapLeftWhenLeft()
        {
            bool expected = true;
            Either<int, string> either = 10;
            Either<bool, string> eitherResult =
            EitherModule.MapLeft(
                _isEven,
                either);

            bool result = eitherResult.Match(left => left, right => false);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherMapLeftWhenRight()
        {
            bool expected = false;
            Either<int, string> either = "10";
            Either<bool, string> eitherResult =
            EitherModule.MapLeft(
                _isEven,
                either);

            bool result = eitherResult.Match(left => left, right => false);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherMapRightWhenLeft()
        {
            double expected = 0;
            Either<int, string> either = 10;
            Either<int, double> eitherResult =
            EitherModule.MapRight(
                right => Convert.ToDouble(right),
                either);

            double result = eitherResult.Match(left => 0, right => right);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherMapRightWhenRight()
        {
            double expected = 10;
            Either<int, string> either = "10";
            Either<int, double> eitherResult =
            EitherModule.MapRight(
                right => Convert.ToDouble(right),
                either);

            double result = eitherResult.Match(left => 0 , right => right);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherMapWhenLeft()
        {
            bool expected = true;
            Either<int, string> either = 10;
            Either<bool, int> eitherResult =
            EitherModule.Map(
                _isEven,
                right => Convert.ToInt32(right),
                either);

            bool result = eitherResult.Match(left => left, right => false);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherMapWhenRight()
        {
            int expected = 10;
            Either<int, string> either = "10";
            Either<bool, int> eitherResult =
            EitherModule.Map(
                _isEven,
                right => Convert.ToInt32(right),
                either);

            int result = eitherResult.Match(left => 0, right => right);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherFoldLeftWhenLeft()
        {
            int state = 10;
            int expected = 20;
            Either<int, string> either = 10;
            int result =
            EitherModule.FoldLeft(
                (_state, left) => left + _state,
                state,
                either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherFoldLeftWhenRight()
        {
            int state = 10;
            int expected = 10;
            Either<int, string> either = "10";
            int result =
            EitherModule.FoldLeft(
                (_state, left) => left + _state,
                state,
                either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherFoldRightWhenLeft()
        {
            string state = "Hello";
            string expected = "Hello";
            Either<int, string> either = 10;
            string result =
            EitherModule.FoldRight(
                (_state, right) => string.Concat(_state, right),
                state,
                either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherFoldRightWhenRight()
        {
            string state = "Hello";
            string expected = "Hello World";
            Either<int, string> either = " World";
            string result =
            EitherModule.FoldRight(
                (_state, right) => string.Concat(_state, right),
                state,
                either);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void EitherFoldWhenLeft()
        {
            int state = 10;
            int expected = 20;
            Either<int, string> either = 10;
            int result =
            EitherModule.Fold(
                (_state, left) => left + _state,
                (_state, right) => _state,
                state,
                either);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherFoldWhenRight()
        {
            int state = 10;
            int expected = 10;
            Either<int, string> either = "10";
            int result =
            EitherModule.Fold(
                (_state, left) => left + _state,
                (_state, right) => _state,
                state,
                either);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void EitherFoldBackLeftWhenLeft()
        {
            int state = 10;
            int expected = 20;
            Either<int, string> either = 10;
            int result =
            EitherModule.FoldBackLeft(
                (left, _state) => left + _state,
                either, state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherFoldBackLeftWhenRight()
        {
            int state = 10;
            int expected = 10;
            Either<int, string> either = "10";
            int result =
            EitherModule.FoldBackLeft(
                (left, _state) => left + _state,
                either, state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherFoldBackRightWhenLeft()
        {
            string state = "Hello";
            string expected = "Hello";
            Either<int, string> either = 10;
            string result =
            EitherModule.FoldBackRight(
                (right, _state) => string.Concat(_state, right),
                either, state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherFoldBackRightWhenRight()
        {
            string state = "Hello";
            string expected = "Hello World";
            Either<int, string> either = " World";
            string result =
            EitherModule.FoldBackRight(
                (right, _state) => string.Concat(_state, right),
                either, state);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void EitherFoldBackWhenLeft()
        {
            int state = 10;
            int expected = 20;
            Either<int, string> either = 10;
            int result =
            EitherModule.FoldBack(
                (left, _state) => left + _state,
                (right, _state) => _state,
                either,
                state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherFoldBackWhenRight()
        {
            int state = 10;
            int expected = 10;
            Either<int, string> either = "10";
            int result =
            EitherModule.FoldBack(
                (left, _state) => left + _state,
                (right, _state) => _state,
                either,
                state);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherSwapWhenLeft()
        {
            int expected = 10;
            Either<int, string> either = 10;
            Either<string,int> swappedEither = 
                EitherModule.Swap(either);

            int result = swappedEither.Match(left => 0, right => right);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherSwapWhenLeft2()
        {
            int expected = 0;
            Either<int, string> either = "Hello";
            Either<string, int> swappedEither =
                EitherModule.Swap(either);

            int result = swappedEither.Match(left => 0, right => right);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherSwapWhenRight()
        {
            string expected = "Hello";
            Either<int, string> either = expected;
            Either<string, int> swappedEither =
                EitherModule.Swap(either);

            string result = swappedEither.Match(left => left, right => string.Empty);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherSwapWhenRight2()
        {
            string expected = string.Empty;
            Either<int, string> either = 10;
            Either<string, int> swappedEither =
                EitherModule.Swap(either);

            string result = swappedEither.Match(left => left, right => string.Empty);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherToOptionLeftWhenLeft()
        {
            int expected = 10;
            Either<int, string> either = 10;
            Option<int> optionResult =
                EitherModule.ToOptionLeft(either);

            int result = optionResult.Match(value => value, () => 0);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherToOptionLeftWhenRight()
        {
            int expected = 0;
            Either<int, string> either = "Hello World";
            Option<int> optionResult =
                EitherModule.ToOptionLeft(either);

            int result = optionResult.Match(value => value, () => 0);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherToOptionRightWhenLeft()
        {
            string expected = string.Empty;
            Either<int, string> either = 10;
            Option<string> optionResult =
                EitherModule.ToOptionRight(either);

            string result = optionResult.Match(value => value, () => string.Empty);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherToOptionRightWhenRight()
        {
            string expected = "Hello World";
            Either<int, string> either = expected;
            Option<string> optionResult =
                EitherModule.ToOptionRight(either);

            string result = optionResult.Match(value => value, () => string.Empty);
            Assert.AreEqual(expected, result);
        }


    }
}
