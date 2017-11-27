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
        public void OperationAdd()
        {
            int expected = 10;
            Assert.AreEqual(expected, IntegerOperations.Add(5, 5));
        }

        [TestMethod]
        public void OperationAddWith()
        {
            int expected = 10;
            Assert.AreEqual(expected, IntegerOperations.AddWith(5)(5));
        }

        [TestMethod]
        public void OperationAdd3With1()
        {
            int expected = 10;
            Assert.AreEqual(expected, IntegerOperations.Add3With(2)(3, 5));
        }

        [TestMethod]
        public void OperationAdd3With2()
        {
            int expected = 10;
            Assert.AreEqual(expected, IntegerOperations.Add3With(2, 3)(5));
        }

        [TestMethod]
        public void OperationAddReduction()
        {
            int expected = 10;
            IEnumerable<int> generated = CollectionModule.Generate(2, 3, 5, 0);
            Assert.AreEqual(expected, generated.Reduce(IntegerOperations.Add));
        }

        [TestMethod]
        public void OperationAddFold2()
        {
            int expected = 20;
            IEnumerable<int> generated =
                CollectionModule.Generate(2, 3, 5, 0);


            Assert.AreEqual(expected, generated.Fold2(generated, 0, IntegerOperations.Add3));
        }


        [TestMethod]
        public void OperationSubtract()
        {
            int expected = -1;
            Assert.AreEqual(expected, IntegerOperations.Subtract(5, 6));
        }

        [TestMethod]
        public void OperationSubtractWith()
        {
            int expected = -1;
            Assert.AreEqual(expected, IntegerOperations.SubtractWith(5)(6));
        }

        [TestMethod]
        public void OperationSubtract3With1()
        {
            int expected = -7;
            Assert.AreEqual(expected, IntegerOperations.Subtract3With(2)(3,6));
        }

        [TestMethod]
        public void OperationSubtract3With2()
        {
            int expected = -7;
            Assert.AreEqual(expected, IntegerOperations.Subtract3With(2, 3)(6));
        }

        [TestMethod]
        public void OperationSubtractReduction()
        {
            int expected = -12;
            IEnumerable<int> generated = CollectionModule.Generate(2, 3, 5, 6);
            Assert.AreEqual(expected, generated.Reduce(IntegerOperations.Subtract));
        }

        [TestMethod]
        public void OperationSubtractFold2()
        {
            int expected = -20;
            IEnumerable<int> generated =
                CollectionModule.Generate(2, 3, 5, 0);


            Assert.AreEqual(expected, generated.Fold2(generated, 0, IntegerOperations.Subtract3));
        }


        [TestMethod]
        public void OperationMultiply()
        {
            int expected = 4;
            Assert.AreEqual(expected, IntegerOperations.Multiply(2, 2));
        }

        [TestMethod]
        public void OperationMultiplyWith()
        {
            int expected = 4;
            Assert.AreEqual(expected, IntegerOperations.MultiplyWith(2)(2));
        }

        [TestMethod]
        public void OperationMultiply3With1()
        {
            int expected = 4;
            Assert.AreEqual(expected, IntegerOperations.Multiply3With(2)(1, 2));
        }

        [TestMethod]
        public void OperationMultiply3With2()
        {
            int expected = 4;
            Assert.AreEqual(expected, IntegerOperations.Multiply3With(2, 1)(2));
        }

        [TestMethod]
        public void OperationMultiplyReduction()
        {
            int expected = 16;
            IEnumerable<int> generated = CollectionModule.Generate(2, 2, 2, 2);
            Assert.AreEqual(expected, generated.Reduce(IntegerOperations.Multiply));
        }

        [TestMethod]
        public void OperationMultiplyFold2()
        {
            int expected = 256;
            IEnumerable<int> generated =
                CollectionModule.Generate(2, 2, 2, 2);


            Assert.AreEqual(expected, generated.Fold2(generated, 1, IntegerOperations.Multiply3));
        }

        [TestMethod]
        public void OperationDivide()
        {
            int expected = 2;
            Assert.AreEqual(expected, IntegerOperations.Divide(5, 2));
        }

        [TestMethod]
        public void OperationDivideWith()
        {
            int expected = 2;
            Assert.AreEqual(expected, IntegerOperations.DivideWith(5)(2));
        }

        [TestMethod]
        public void OperationDivide3With1()
        {
            int expected = 2;
            Assert.AreEqual(expected, IntegerOperations.Divide3With(10)(2, 2));
        }

        [TestMethod]
        public void OperationDivide3With2()
        {
            int expected = 2;
            Assert.AreEqual(expected, IntegerOperations.Divide3With(10, 2)(2));
        }

        [TestMethod]
        public void OperationDivideReduction()
        {
            int expected = 1;
            IEnumerable<int> generated = CollectionModule.Generate(10, 2, 2, 2);
            Assert.AreEqual(expected, generated.Reduce(IntegerOperations.Divide));
        }

        [TestMethod]
        public void OperationDivideFold2()
        {
            int expected = 0;
            IEnumerable<int> generated =
                CollectionModule.Generate(10, 2, 2, 2);


            Assert.AreEqual(expected, generated.Fold2(generated, 1, IntegerOperations.Divide3));
        }


    }
}
