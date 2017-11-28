using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tango.Functional;

namespace Tango.Test.Functional
{
    [TestClass]
    public class CurryTests

    {
        [TestMethod]
        public void CurryFuncTwoParameters()
        {
            int result, curriedResult;
            Func<int, int, int> add = (value, value2) => value + value2;
            result = add(2, 3);

            Func<int, Func<int, int>> addCurried = add.Curry();
            curriedResult = addCurried(2)(3);

            Assert.AreEqual(result, curriedResult);
        }

        [TestMethod]
        public void CurryFuncThreeParameters()
        {
            int result, curriedResult;
            Func<int, int, int, int> add = (value, value2, value3) => value + value2 + value3;
            result = add(2, 3, 10);

            Func<int, Func<int, Func<int, int>>> addCurried = add.Curry();
            curriedResult = addCurried(2)(3)(10);

            Assert.AreEqual(result, curriedResult);
        }

        [TestMethod]
        public void CurryFuncFourParameters()
        {
            int result, curriedResult;
            Func<int, int, int, int, int> add =
                (value, value2, value3, value4) =>
                    value + value2 + value3 + value4;
            result = add(2, 3, 10, 5);

            Func<int, Func<int, Func<int, Func<int, int>>>> addCurried =
                add.Curry();

            curriedResult = addCurried(2)(3)(10)(5);

            Assert.AreEqual(result, curriedResult);
        }


        [TestMethod]
        public void CurryActionTwoParameters()
        {
            int result = 0, sideEffectResult = 0, curriedResult = 0;
            Action<int, int> add = (value, value2) => sideEffectResult = value + value2;
            add(2, 3);
            sideEffectResult = result;

            Func<int, Action<int>> addCurried = add.Curry();
            addCurried(2)(3);
            curriedResult = result;

            Assert.AreEqual(result, curriedResult);
        }

        [TestMethod]
        public void CurryActionThreeParameters()
        {
            int result = 0, sideEffectResult = 0, curriedResult = 0;
            Action<int, int, int> add =
                (value, value2, value3) => sideEffectResult = value + value2 + value3;
            add(2, 3, 10);
            sideEffectResult = result;

            Func<int, Func<int, Action<int>>> addCurried = add.Curry();
            addCurried(2)(3)(10);
            curriedResult = result;

            Assert.AreEqual(result, curriedResult);
        }

        [TestMethod]
        public void CurryActionFourParameters()
        {
            int result = 0, sideEffectResult = 0, curriedResult = 0;
            Action<int, int, int, int> add =
                (value, value2, value3, value4) => 
                    sideEffectResult = value + value2 + value3 + value4;
            add(2, 3, 10, 5);
            sideEffectResult = result;

            Func < int,Func<int, Func<int, Action<int>>>> addCurried = add.Curry();
            addCurried(2)(3)(10)(5);
            curriedResult = result;
            
            Assert.AreEqual(result, curriedResult);
        }


    }
}
