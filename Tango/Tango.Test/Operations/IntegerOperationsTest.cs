using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Tango.CommonOperations;
using Tango.Linq;
using Tango.Modules;

namespace Tango.Test.Types
{
    [TestClass]
    public class IntegerOperationsTests

    {
        [TestMethod]
        public void IntegerOperationAdd()
        {
            int expected = 10;
            Assert.AreEqual(expected, IntegerOperations.Add(5, 5));
        }

        [TestMethod]
        public void IntegerOperationAddWith()
        {
            int expected = 10;
            Assert.AreEqual(expected, IntegerOperations.AddWith(5)(5));
        }

        [TestMethod]
        public void IntegerOperationAdd3With1()
        {
            int expected = 10;
            Assert.AreEqual(expected, IntegerOperations.Add3With(2)(3, 5));
        }

        [TestMethod]
        public void IntegerOperationAdd3With2()
        {
            int expected = 10;
            Assert.AreEqual(expected, IntegerOperations.Add3With(2, 3)(5));
        }

        [TestMethod]
        public void IntegerOperationAddReduction()
        {
            int expected = 10;
            IEnumerable<int> generated = CollectionModule.Generate(2, 3, 5, 0);
            Assert.AreEqual(expected, generated.Reduce(IntegerOperations.Add));
        }

        [TestMethod]
        public void IntegerOperationAddFold2()
        {
            int expected = 20;
            IEnumerable<int> generated =
                CollectionModule.Generate(2, 3, 5, 0);


            Assert.AreEqual(expected, generated.Fold2(generated, 0, IntegerOperations.Add3));
        }


        [TestMethod]
        public void IntegerOperationSubtract()
        {
            int expected = -1;
            Assert.AreEqual(expected, IntegerOperations.Subtract(5, 6));
        }

        [TestMethod]
        public void IntegerOperationSubtractWith()
        {
            int expected = -1;
            Assert.AreEqual(expected, IntegerOperations.SubtractWith(5)(6));
        }

        [TestMethod]
        public void IntegerOperationSubtract3With1()
        {
            int expected = -7;
            Assert.AreEqual(expected, IntegerOperations.Subtract3With(2)(3,6));
        }

        [TestMethod]
        public void IntegerOperationSubtract3With2()
        {
            int expected = -7;
            Assert.AreEqual(expected, IntegerOperations.Subtract3With(2, 3)(6));
        }

        [TestMethod]
        public void IntegerOperationSubtractReduction()
        {
            int expected = -12;
            IEnumerable<int> generated = CollectionModule.Generate(2, 3, 5, 6);
            Assert.AreEqual(expected, generated.Reduce(IntegerOperations.Subtract));
        }

        [TestMethod]
        public void IntegerOperationSubtractFold2()
        {
            int expected = -20;
            IEnumerable<int> generated =
                CollectionModule.Generate(2, 3, 5, 0);


            Assert.AreEqual(expected, generated.Fold2(generated, 0, IntegerOperations.Subtract3));
        }


        [TestMethod]
        public void IntegerOperationMultiply()
        {
            int expected = 4;
            Assert.AreEqual(expected, IntegerOperations.Multiply(2, 2));
        }

        [TestMethod]
        public void IntegerOperationMultiplyWith()
        {
            int expected = 4;
            Assert.AreEqual(expected, IntegerOperations.MultiplyWith(2)(2));
        }

        [TestMethod]
        public void IntegerOperationMultiply3With1()
        {
            int expected = 4;
            Assert.AreEqual(expected, IntegerOperations.Multiply3With(2)(1, 2));
        }

        [TestMethod]
        public void IntegerOperationMultiply3With2()
        {
            int expected = 4;
            Assert.AreEqual(expected, IntegerOperations.Multiply3With(2, 1)(2));
        }

        [TestMethod]
        public void IntegerOperationMultiplyReduction()
        {
            int expected = 16;
            IEnumerable<int> generated = CollectionModule.Generate(2, 2, 2, 2);
            Assert.AreEqual(expected, generated.Reduce(IntegerOperations.Multiply));
        }

        [TestMethod]
        public void IntegerOperationMultiplyFold2()
        {
            int expected = 256;
            IEnumerable<int> generated =
                CollectionModule.Generate(2, 2, 2, 2);


            Assert.AreEqual(expected, generated.Fold2(generated, 1, IntegerOperations.Multiply3));
        }

        [TestMethod]
        public void IntegerOperationDivide()
        {
            int expected = 2;
            Assert.AreEqual(expected, IntegerOperations.Divide(5, 2));
        }

        [TestMethod]
        public void IntegerOperationDivideWith()
        {
            int expected = 2;
            Assert.AreEqual(expected, IntegerOperations.DivideWith(5)(2));
        }

        [TestMethod]
        public void IntegerOperationDivide3With1()
        {
            int expected = 2;
            Assert.AreEqual(expected, IntegerOperations.Divide3With(10)(2, 2));
        }

        [TestMethod]
        public void IntegerOperationDivide3With2()
        {
            int expected = 2;
            Assert.AreEqual(expected, IntegerOperations.Divide3With(10, 2)(2));
        }

        [TestMethod]
        public void IntegerOperationDivideReduction()
        {
            int expected = 1;
            IEnumerable<int> generated = CollectionModule.Generate(10, 2, 2, 2);
            Assert.AreEqual(expected, generated.Reduce(IntegerOperations.Divide));
        }

        [TestMethod]
        public void IntegerOperationDivideFold2()
        {
            int expected = 0;
            IEnumerable<int> generated =
                CollectionModule.Generate(10, 2, 2, 2);


            Assert.AreEqual(expected, generated.Fold2(generated, 1, IntegerOperations.Divide3));
        }


    }
}
