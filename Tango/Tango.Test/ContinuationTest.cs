using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tango.Core;
using Tango.Core.Extensions;
using System.Linq;
using System.Collections.Generic;

namespace Tango.Test
{
    [TestClass]
    public class ContinuationTest
    {
        [TestMethod]
        public void TestAddAndSub()
        {
            int result = 1.MapToContinuation()
                            .Then(number => number + 5)
                            .Then(number => number + 10)
                            .Then(number => number - 2);

            Assert.AreEqual(14, result);
        }

        [TestMethod]
        public void TestDivWithZero()
        {
            int zero = 0;
            IEnumerable<string> errors = Enumerable.Empty<string>();
            int result = 5.MapToContinuation()
                            .Then(number => number + 5)
                            .Then(number => number / zero)
                            .Fail(fail => {
                                errors = fail.Errors;
                                return fail;
                            });

            Assert.IsTrue(errors.Any());
        }

        [TestMethod]
        public void TestEvenNumberValidationWithSuccess()
        {
            IEnumerable<string> errors = Enumerable.Empty<string>();
            int result = TestEvenNumberThenDoubleIt(4)
                            .Fail(fail =>
                            {
                                errors = fail.Errors;
                                return fail;
                            });

            Assert.IsTrue(!errors.Any() && result == 8);
        }

        [TestMethod]
        public void TestEvenNumberValidationWithFail()
        {
            IEnumerable<string> errors = Enumerable.Empty<string>();
            int result = TestEvenNumberThenDoubleIt(3)
                            .Fail(fail =>
                            {
                                errors = fail.Errors;
                                return fail;
                            });

            Assert.IsTrue(errors.Any() && result == 0);
        }

        private Continuation<int, FailResult> TestEvenNumberThenDoubleIt(int numberForTest)
         => numberForTest.MapToContinuation()
                         .Bypass(IsEven)
                         .Then(number => number * 2);

        private FailResult IsEven(int number)
            => number % 2 == 0 ? ContinuationFail.Ignore()
                                 :new FailResult("This number is odd.");
    }
}
