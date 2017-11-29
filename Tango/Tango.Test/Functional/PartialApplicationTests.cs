using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tango.Functional;
using Tango.Types;

namespace Tango.Test.Functional
{
    [TestClass]
    public class PartialApplicationTests

    {
        [TestMethod]
        public void PartialApplyFuncOneParametersWith1()
        {
            int result, partialAppliedResult;
            Func<int, int> add = (value) => value + 5;
            result = add(5);

            Func<int> addParialApplied = add.PartialApply(5);
            partialAppliedResult = addParialApplied();

            Assert.AreEqual(result, partialAppliedResult);
        }

        [TestMethod]
        public void PartialApplyFuncTwoParametersWith2()
        {
            int result, partialAppliedResult;
            Func<int, int, int> add = (value, value2) => value + value2;
            result = add(2, 3);

            Func<int> addParialApplied = add.PartialApply(2,3);
            partialAppliedResult = addParialApplied();

            Assert.AreEqual(result, partialAppliedResult);
        }

        [TestMethod]
        public void PartialApplyFuncTwoParametersWith1()
        {
            int result, partialAppliedResult;
            Func<int, int, int> add = (value, value2) => value + value2;
            result = add(2, 3);

            Func<int, int> addParialApplied = add.PartialApply(2);
            partialAppliedResult = addParialApplied(3);

            Assert.AreEqual(result, partialAppliedResult);
        }

        [TestMethod]
        public void PartialApplyFuncThreeParametersWith3()
        {
            int result, partialAppliedResult;
            Func<int, int, int, int> add =
                (value, value2, value3) => value + value2 + value3;
            result = add(2, 3, 10);

            Func<int> addParialApplied = add.PartialApply(2, 3,10);
            partialAppliedResult = addParialApplied();

            Assert.AreEqual(result, partialAppliedResult);
        }

        [TestMethod]
        public void PartialApplyFuncThreeParametersWith2()
        {
            int result, partialAppliedResult;
            Func<int, int, int, int> add =
                (value, value2, value3) => value + value2 + value3;
            result = add(2, 3, 10);

            Func<int, int> addParialApplied = add.PartialApply(2, 3);
            partialAppliedResult = addParialApplied(10);

            Assert.AreEqual(result, partialAppliedResult);
        }

        [TestMethod]
        public void PartialApplyFuncThreeParametersWith1()
        {
            int result, partialAppliedResult;
            Func<int, int, int, int> add = 
                (value, value2, value3) => value + value2 + value3;
            result = add(2, 3, 10);

            Func<int, int,int> addParialApplied = add.PartialApply(2);
            partialAppliedResult = addParialApplied(3,10);

            Assert.AreEqual(result, partialAppliedResult);
        }

        [TestMethod]
        public void PartialApplyFuncFourParametersWith4()
        {
            int result, partialAppliedResult;
            Func<int, int, int, int, int> add =
                (value, value2, value3, value4) =>
                    value + value2 + value3 + value4;
            result = add(2, 3, 10, 5);

            Func<int> addParialApplied = add.PartialApply(2, 3, 10,5);
            partialAppliedResult = addParialApplied();

            Assert.AreEqual(result, partialAppliedResult);
        }

        [TestMethod]
        public void PartialApplyFuncFourParametersWith3()
        {
            int result, partialAppliedResult;
            Func<int, int, int, int, int> add =
                (value, value2, value3, value4) =>
                    value + value2 + value3 + value4;
            result = add(2, 3, 10, 5);

            Func<int, int> addParialApplied = add.PartialApply(2, 3, 10);
            partialAppliedResult = addParialApplied(5);

            Assert.AreEqual(result, partialAppliedResult);
        }

        [TestMethod]
        public void PartialApplyFuncFourParametersWith2()
        {
            int result, partialAppliedResult;
            Func<int, int, int, int, int> add =
                (value, value2, value3, value4) =>
                    value + value2 + value3 + value4;
            result = add(2, 3, 10, 5);

            Func<int, int, int> addParialApplied = add.PartialApply(2, 3);
            partialAppliedResult = addParialApplied(10, 5);

            Assert.AreEqual(result, partialAppliedResult);
        }

        [TestMethod]
        public void PartialApplyFuncFourParametersWith1()
        {
            int result, partialAppliedResult;
            Func<int, int, int, int, int> add =
                (value, value2, value3, value4) =>
                    value + value2 + value3 + value4;
            result = add(2, 3, 10, 5);

            Func<int, int, int,int> addParialApplied = add.PartialApply(2);
            partialAppliedResult = addParialApplied(3,10,5);

            Assert.AreEqual(result, partialAppliedResult);
        }

        



        [TestMethod]
        public void PartialApplyActionOneParametersWith1()
        {
            int sideEffectResult = 0,result, partialAppliedResult;
            Action<int> add = (value) => sideEffectResult = value + 5;
            add(5);
            result = sideEffectResult;

            Action addParialApplied = add.PartialApply(5);
            addParialApplied();
            partialAppliedResult = sideEffectResult;

            Assert.AreEqual(result, partialAppliedResult);
        }

        [TestMethod]
        public void PartialApplyActionTwoParametersWith2()
        {
            int sideEffectResult = 0, result, partialAppliedResult;
            Action<int, int> add = (value, value2) => sideEffectResult = value + value2;
            add(2, 3);
            result = sideEffectResult;

            Action addParialApplied = add.PartialApply(2,3);
            addParialApplied();
            partialAppliedResult = sideEffectResult;

            Assert.AreEqual(result, partialAppliedResult);
        }

        [TestMethod]
        public void PartialApplyActionTwoParametersWith1()
        {
            int sideEffectResult = 0, result, partialAppliedResult;
            Action<int,int> add = (value, value2) => sideEffectResult=value + value2;
            add(2, 3);
            result = sideEffectResult;

            Action<int> addParialApplied = add.PartialApply(2);
            addParialApplied(3);
            partialAppliedResult = sideEffectResult;

            Assert.AreEqual(result, partialAppliedResult);
        }

        [TestMethod]
        public void PartialApplyActionThreeParametersWith3()
        {
            int sideEffectResult = 0, result, partialAppliedResult;
            Action<int, int, int> add =
                (value, value2, value3) => sideEffectResult = value + value2 + value3;
            add(2, 3, 10);
            result = sideEffectResult;

            Action addParialApplied = add.PartialApply(2, 3,10);
            addParialApplied();
            partialAppliedResult = sideEffectResult;

            Assert.AreEqual(result, partialAppliedResult);
        }

        [TestMethod]
        public void PartialApplyActionThreeParametersWith2()
        {
            int sideEffectResult = 0, result, partialAppliedResult;
            Action<int, int, int> add =
                (value, value2, value3) => sideEffectResult = value + value2 + value3;
            add(2, 3, 10);
            result = sideEffectResult;

            Action<int> addParialApplied = add.PartialApply(2, 3);
            addParialApplied(10);
            partialAppliedResult = sideEffectResult;

            Assert.AreEqual(result, partialAppliedResult);
        }

        [TestMethod]
        public void PartialApplyActionThreeParametersWith1()
        {
            int sideEffectResult = 0, result, partialAppliedResult;
            Action< int, int, int> add =
                (value, value2, value3) => sideEffectResult = value + value2 + value3;
            add(2, 3, 10);
            result = sideEffectResult;

            Action<int, int> addParialApplied = add.PartialApply(2);
            addParialApplied(3, 10);
            partialAppliedResult = sideEffectResult;
            Assert.AreEqual(result, partialAppliedResult);
        }

        [TestMethod]
        public void PartialApplyActionFourParametersWith4()
        {
            int sideEffectResult = 0, result, partialAppliedResult;
            Action<int, int, int, int> add =
                (value, value2, value3, value4) =>
                    sideEffectResult = value + value2 + value3 + value4;
            add(2, 3, 10, 5);
            result = sideEffectResult;
            Action addParialApplied = add.PartialApply(2, 3, 10,5);
            addParialApplied();
            partialAppliedResult = sideEffectResult;
            Assert.AreEqual(result, partialAppliedResult);
        }

        [TestMethod]
        public void PartialApplyActionFourParametersWith3()
        {
            int sideEffectResult = 0, result, partialAppliedResult;
            Action<int, int, int, int> add =
                (value, value2, value3, value4) =>
                    sideEffectResult = value + value2 + value3 + value4;
            add(2, 3, 10, 5);
            result = sideEffectResult;
            Action<int> addParialApplied = add.PartialApply(2, 3, 10);
            addParialApplied(5);
            partialAppliedResult = sideEffectResult;
            Assert.AreEqual(result, partialAppliedResult);
        }

        [TestMethod]
        public void PartialApplyActionFourParametersWith2()
        {
            int sideEffectResult = 0, result, partialAppliedResult;
            Action<int, int, int, int> add =
                (value, value2, value3, value4) =>
                    sideEffectResult = value + value2 + value3 + value4;
            add(2, 3, 10, 5);
            result = sideEffectResult;

            Action<int, int> addParialApplied = add.PartialApply(2, 3);
            addParialApplied(10, 5);
            partialAppliedResult = sideEffectResult;
            Assert.AreEqual(result, partialAppliedResult);
        }

        [TestMethod]
        public void PartialApplyActionFourParametersWith1()
        {
            int sideEffectResult = 0, result, partialAppliedResult;
            Action<int, int, int, int> add =
                (value, value2, value3, value4) =>
                    sideEffectResult = value + value2 + value3 + value4;
            add(2, 3, 10, 5);
            result = sideEffectResult;

            Action<int, int, int> addParialApplied = add.PartialApply(2);
            addParialApplied(3, 10, 5);
            partialAppliedResult = sideEffectResult;
            Assert.AreEqual(result, partialAppliedResult);
        }
    }
}
