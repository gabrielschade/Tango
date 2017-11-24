using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tango.Types;
using System.Linq;
using System.Collections.Generic;
using Tango.Linq;

namespace Tango.Test.Types
{
    [TestClass]
    public class ContinuationTest

    {
        [TestMethod]
        public void TestContinuationWithOptionSuccess()
        {
            Continuation<int, object> continuation = 10;

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
        public void TestContinuationWithMultiplesThen()
        {
            Continuation<int, string> continuation = 1;

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
        public void TestContinuationWithMultiplesThenOperator()
        {
            Continuation<int, string> continuation = 1;

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
        public void TestContinuationWithOptionFail()
        {
            Continuation<int, string> continuation = 1;

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
        public void TestContinuationWithCatch()
        {
            Continuation<int, string> continuation = 1;

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
        public void TestContinuationWithMultiplesCatches()
        {
            Continuation<int, string> continuation = 1;

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
        public void TestContinuationWithChangeFailTypesCatches()
        {
            Continuation<int, string> continuation = 1;

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
        public void TestContinuationWithMultiplesThenAndCatchWithOperators()
        {
            Continuation<int, bool> continuation = 1;

            Option<bool> optionResult =
                continuation
                > (value => value + 4)
                > (value => value + 10)
                > (value => value * 2)
                > (value => {
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
        public void Test()
        {
            IEnumerable<int> values1 = Collection.Range(1, 10);
            
        }

    }
}
