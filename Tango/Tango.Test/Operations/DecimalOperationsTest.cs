using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Tango.CommonOperations;
using Tango.Linq;
using Tango.Modules;

namespace Tango.Test.Types
{
    [TestClass]
    public class DecimalOperationsTests

    {
        [TestMethod]
        public void OperationAdd()
        {
            decimal expected = 10.5m;
            Assert.AreEqual(expected, DecimalOperations.Add(5, 5.5m));
        }

        [TestMethod]
        public void OperationAddWith()
        {
            decimal expected = 10.5m;
            Assert.AreEqual(expected, DecimalOperations.AddWith(5)(5.5m));
        }

        [TestMethod]
        public void OperationAdd3With1()
        {
            decimal expected = 10.5m;
            Assert.AreEqual(expected, DecimalOperations.Add3With(2)(3, 5.5m));
        }

        [TestMethod]
        public void OperationAdd3With2()
        {
            decimal expected = 10.5m;
            Assert.AreEqual(expected, DecimalOperations.Add3With(2, 3)(5.5m));
        }

        [TestMethod]
        public void OperationAddReduction()
        {
            decimal expected = 10.5m;
            IEnumerable<decimal> generated = CollectionModule.Generate(2, 3, 5.3m, 0.2m);
            Assert.AreEqual(expected, generated.Reduce(DecimalOperations.Add));
        }

        [TestMethod]
        public void OperationAddFold2()
        {
            decimal expected = 21m;
            IEnumerable<decimal> generated =
                CollectionModule.Generate(2, 3, 5.3m, 0.2m);


            Assert.AreEqual(expected, generated.Fold2(generated, 0, DecimalOperations.Add3));
        }


        [TestMethod]
        public void OperationSubtract()
        {
            decimal expected = -0.5m;
            Assert.AreEqual(expected, DecimalOperations.Subtract(5, 5.5m));
        }

        [TestMethod]
        public void OperationSubtractWith()
        {
            decimal expected = -0.5m;
            Assert.AreEqual(expected, DecimalOperations.SubtractWith(5)(5.5m));
        }

        [TestMethod]
        public void OperationSubtract3With1()
        {
            decimal expected = -6.5m;
            Assert.AreEqual(expected, DecimalOperations.Subtract3With(2)(3, 5.5m));
        }

        [TestMethod]
        public void OperationSubtract3With2()
        {
            decimal expected = -6.5m;
            Assert.AreEqual(expected, DecimalOperations.Subtract3With(2, 3)(5.5m));
        }

        [TestMethod]
        public void OperationSubtractReduction()
        {
            decimal expected = -6.5m;
            IEnumerable<decimal> generated = CollectionModule.Generate(2, 3, 5.3m, 0.2m);
            Assert.AreEqual(expected, generated.Reduce(DecimalOperations.Subtract));
        }

        [TestMethod]
        public void OperationSubtractFold2()
        {
            decimal expected = -21m;
            IEnumerable<decimal> generated =
                CollectionModule.Generate(2, 3, 5.3m, 0.2m);


            Assert.AreEqual(expected, generated.Fold2(generated, 0, DecimalOperations.Subtract3));
        }


        [TestMethod]
        public void OperationMultiply()
        {
            decimal expected = 5m;
            Assert.AreEqual(expected, DecimalOperations.Multiply(2, 2.5m));
        }

        [TestMethod]
        public void OperationMultiplyWith()
        {
            decimal expected = 5m;
            Assert.AreEqual(expected, DecimalOperations.MultiplyWith(2)(2.5m));
        }

        [TestMethod]
        public void OperationMultiply3With1()
        {
            decimal expected = 5m;
            Assert.AreEqual(expected, DecimalOperations.Multiply3With(2)(1, 2.5m));
        }

        [TestMethod]
        public void OperationMultiply3With2()
        {
            decimal expected = 5m;
            Assert.AreEqual(expected, DecimalOperations.Multiply3With(2, 1)(2.5m));
        }

        [TestMethod]
        public void OperationMultiplyReduction()
        {
            decimal expected = 5m;
            IEnumerable<decimal> generated = CollectionModule.Generate(2m, 2.5m, 2m, 0.5m);
            Assert.AreEqual(expected, generated.Reduce(DecimalOperations.Multiply));
        }

        [TestMethod]
        public void OperationMultiplyFold2()
        {
            decimal expected = 25m;
            IEnumerable<decimal> generated =
                CollectionModule.Generate(2m, 2.5m, 2m, 0.5m);


            Assert.AreEqual(expected, generated.Fold2(generated, 1, DecimalOperations.Multiply3));
        }

        [TestMethod]
        public void OperationDivide()
        {
            decimal expected = 2.5m;
            Assert.AreEqual(expected, DecimalOperations.Divide(5, 2m));
        }

        [TestMethod]
        public void OperationDivideWith()
        {
            decimal expected = 2.5m;
            Assert.AreEqual(expected, DecimalOperations.DivideWith(5)(2m));
        }

        [TestMethod]
        public void OperationDivide3With1()
        {
            decimal expected = 2.5m;
            Assert.AreEqual(expected, DecimalOperations.Divide3With(10)(2, 2m));
        }

        [TestMethod]
        public void OperationDivide3With2()
        {
            decimal expected = 2.5m;
            Assert.AreEqual(expected, DecimalOperations.Divide3With(10, 2)(2m));
        }

        [TestMethod]
        public void OperationDivideReduction()
        {
            decimal expected = 5m;
            IEnumerable<decimal> generated = CollectionModule.Generate(10, 2, 2, 0.5m);
            Assert.AreEqual(expected, generated.Reduce(DecimalOperations.Divide));
        }

        [TestMethod]
        public void OperationDivideFold2()
        {
            decimal expected = 0.0025m;
            IEnumerable<decimal> generated =
                CollectionModule.Generate(10, 2, 2, 0.5m);


            Assert.AreEqual(expected, generated.Fold2(generated, 1, DecimalOperations.Divide3));
        }


    }
}
