using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tango.Types;

namespace Tango.Test.Types
{
    [TestClass]
    public class ContinuationTests
    {
        [TestMethod]
        public void ContinuationReturnToSuccessWhenSuccess()
        {
            int expected = 10;
            Continuation<bool, int> continuation = Continuation<bool, int>.Return(10);
            Option<int> option = continuation;
            int result = option.Match(value => value, () => 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ContinuationReturnToSuccessWhenFail()
        {
            int expected = 0;
            Continuation<bool, int> continuation = Continuation<bool, int>.Return(true);
            Option<int> option = continuation;
            int result = option.Match(value => value, () => 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ContinuationReturnToFailWhenSuccess()
        {
            bool expected = false;
            Continuation<bool, int> continuation = Continuation<bool, int>.Return(10);
            Option<bool> option = continuation;
            bool result = option.Match(value => value, () => false);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ContinuationReturnToFailWhenFail()
        {
            bool expected = true;
            Continuation<bool, int> continuation = Continuation<bool, int>.Return(true);
            Option<bool> option = continuation;
            bool result = option.Match(value => value, () => false);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ContinuationMatchWhenSuccess()
        {
            int expected = 10;
            Continuation<bool, int> continuation = 10;
            int result = continuation.Match(success => success, fail => 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ContinuationMatchWhenFail()
        {
            int expected = 0;
            Continuation<bool, int> continuation = true;
            int result = continuation.Match(success => success, fail => 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ContinuationWithOptionSuccess()
        {
            Continuation<object, int> continuation = 10;

            Option<string> optionResult =
                continuation
                .Then<bool>(value => value % 2 == 0)
                .Then<string>(value => value ? "Even" : "Odd");

            string result = optionResult.Match(
                value => value,
                () => string.Empty);

            Assert.AreEqual(result, "Even");
        }

        [TestMethod]
        public void ContinuationWithMultiplesThen()
        {
            Continuation<string, int> continuation = 1;

            Option<int> optionResult =
                continuation
                    .Then(value => value + 4)
                    .Then(value => value + 10);


            int result = optionResult.Match(
                value => value,
                () => 0);

            Assert.AreEqual(result, 15);
        }

        [TestMethod]
        public void ContinuationWithMultiplesParametersThen()
        {
            int extraInput = 5;
            Continuation<string, int> continuation = 1;

            Option<int> optionResult =
                continuation
                    .Then(value => value + 4)
                    .Then<int, int>((input, value) => input + value + 10, extraInput);


            int result = optionResult.Match(
                value => value,
                () => 0);

            Assert.AreEqual(result, 20);
        }

        [TestMethod]
        public void ContinuationWithMultiplesThenOperator()
        {
            Continuation<string, int> continuation = 1;

            Option<int> optionResult =
                continuation
                > (value => value + 4)
                > (value => value + 10)
                > (value => value * 2);


            int result = optionResult.Match(
                value => value,
                () => 0);

            Assert.AreEqual(result, 30);
        }

        [TestMethod]
        public void ContinuationWithOptionFail()
        {
            Continuation<string, int> continuation = 1;

            Option<bool> optionResult =
                continuation
                .Then<bool>(value =>
                {
                    if (value % 2 == 0)
                        return true;
                    else
                        return "ERROR";
                });


            string result = optionResult.Match(
                value => value.ToString(),
                () => string.Empty);

            Assert.AreEqual(result, string.Empty);
        }

        [TestMethod]
        public void ContinuationWithCatch()
        {
            Continuation<string, int> continuation = 1;

            Option<string> optionResult =
                continuation
                .Then<bool>(value =>
                {
                    if (value % 2 == 0)
                        return true;
                    else
                        return "ERROR";
                })
                .Catch(message => $"{message} catched");


            string result = optionResult.Match(
                value => value.ToString(),
                () => string.Empty);

            Assert.AreEqual(result, "ERROR catched");
        }

        [TestMethod]
        public void ContinuationWithMultiplesCatches()
        {
            Continuation<string, int> continuation = 1;

            Option<string> optionResult =
                continuation
                .Then<bool>(value =>
                {
                    if (value % 2 == 0)
                        return true;
                    else
                        return "ERROR";
                })
                .Catch(message => $"{message} catched")
                .Catch(message => $"{message} again")
                .Catch(message => $"{message} and again");


            string result = optionResult.Match(
                value => value.ToString(),
                () => string.Empty);

            Assert.AreEqual(result, "ERROR catched again and again");
        }



        [TestMethod]
        public void ContinuationWithChangeFailTypesCatches()
        {
            Continuation<string, int> continuation = 1;

            Option<int> optionResult =
                continuation
                .Then<bool>(value =>
                {
                    if (value % 2 == 0)
                        return true;
                    else
                        return "ERROR";
                })
                .Catch(message => $"{message} catched")
                .Catch<double>(message => 35.5)
                .Catch<int>(value => 42);


            string result = optionResult.Match(
                value => value.ToString(),
                () => string.Empty);

            Assert.AreEqual(result, "42");
        }

        [TestMethod]
        public void ContinuationWithChangeFailTypesCatchesWhenThen()
        {
            Continuation<string, int> continuation = 2;

            Option<double> optionResult =
                continuation
                .Then<bool>(value =>
                {
                    if (value % 2 == 0)
                        return true;
                    else
                        return "ERROR";
                })
                .Catch<double>(message => 35.5);


            double result = optionResult.Match(
                value => value,
                () => 0);

            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void ContinuationWithMultiplesThenAndCatchWithOperators()
        {
            Continuation<bool, int> continuation = 1;

            Option<bool> optionResult =
                continuation
                > (value => value + 4)
                > (value => value + 10)
                > (value => value * 2)
                > (value =>
                {
                    if (value < 50)
                        return false;
                    else
                        return value;
                })
                > (value => value + 10)
                >= (value => true);


            bool result = optionResult.Match(
                value => value,
                () => false);

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ContinuationWithInvalidperatorThen()
        {
            Continuation<bool, int> continuation = 1;
            Option<int> result =
                continuation < (value => value + 4);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ContinuationWithInvalidperatorCatch()
        {
            Continuation<int, bool> continuation = 1;
            Option<int> result =
                continuation <= ((value) => 0);
        }

    }
}
