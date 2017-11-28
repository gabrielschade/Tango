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
        public void DecimalOperationAdd()
        {
            decimal expected = 10.5m;
            Assert.AreEqual(expected, DecimalOperations.Add(5, 5.5m));
        }

        [TestMethod]
        public void DecimalOperationAddWith()
        {
            decimal expected = 10.5m;
            Assert.AreEqual(expected, DecimalOperations.AddWith(5)(5.5m));
        }

        [TestMethod]
        public void DecimalOperationAdd3With1()
        {
            decimal expected = 10.5m;
            Assert.AreEqual(expected, DecimalOperations.Add3With(2)(3, 5.5m));
        }

        [TestMethod]
        public void DecimalOperationAdd3With2()
        {
            decimal expected = 10.5m;
            Assert.AreEqual(expected, DecimalOperations.Add3With(2, 3)(5.5m));
        }

        [TestMethod]
        public void DecimalOperationAddReduction()
        {
            decimal expected = 10.5m;
            IEnumerable<decimal> generated = CollectionModule.Generate(2, 3, 5.3m, 0.2m);
            Assert.AreEqual(expected, generated.Reduce(DecimalOperations.Add));
        }

        [TestMethod]
        public void DecimalOperationAddFold2()
        {
            decimal expected = 21m;
            IEnumerable<decimal> generated =
                CollectionModule.Generate(2, 3, 5.3m, 0.2m);


            Assert.AreEqual(expected, generated.Fold2(generated, 0, DecimalOperations.Add3));
        }


        [TestMethod]
        public void DecimalOperationSubtract()
        {
            decimal expected = -0.5m;
            Assert.AreEqual(expected, DecimalOperations.Subtract(5, 5.5m));
        }

        [TestMethod]
        public void DecimalOperationSubtractWith()
        {
            decimal expected = -0.5m;
            Assert.AreEqual(expected, DecimalOperations.SubtractWith(5)(5.5m));
        }

        [TestMethod]
        public void DecimalOperationSubtract3With1()
        {
            decimal expected = -6.5m;
            Assert.AreEqual(expected, DecimalOperations.Subtract3With(2)(3, 5.5m));
        }

        [TestMethod]
        public void DecimalOperationSubtract3With2()
        {
            decimal expected = -6.5m;
            Assert.AreEqual(expected, DecimalOperations.Subtract3With(2, 3)(5.5m));
        }

        [TestMethod]
        public void DecimalOperationSubtractReduction()
        {
            decimal expected = -6.5m;
            IEnumerable<decimal> generated = CollectionModule.Generate(2, 3, 5.3m, 0.2m);
            Assert.AreEqual(expected, generated.Reduce(DecimalOperations.Subtract));
        }

        [TestMethod]
        public void DecimalOperationSubtractFold2()
        {
            decimal expected = -21m;
            IEnumerable<decimal> generated =
                CollectionModule.Generate(2, 3, 5.3m, 0.2m);


            Assert.AreEqual(expected, generated.Fold2(generated, 0, DecimalOperations.Subtract3));
        }


        [TestMethod]
        public void DecimalOperationMultiply()
        {
            decimal expected = 5m;
            Assert.AreEqual(expected, DecimalOperations.Multiply(2, 2.5m));
        }

        [TestMethod]
        public void DecimalOperationMultiplyWith()
        {
            decimal expected = 5m;
            Assert.AreEqual(expected, DecimalOperations.MultiplyWith(2)(2.5m));
        }

        [TestMethod]
        public void DecimalOperationMultiply3With1()
        {
            decimal expected = 5m;
            Assert.AreEqual(expected, DecimalOperations.Multiply3With(2)(1, 2.5m));
        }

        [TestMethod]
        public void DecimalOperationMultiply3With2()
        {
            decimal expected = 5m;
            Assert.AreEqual(expected, DecimalOperations.Multiply3With(2, 1)(2.5m));
        }

        [TestMethod]
        public void DecimalOperationMultiplyReduction()
        {
            decimal expected = 5m;
            IEnumerable<decimal> generated = CollectionModule.Generate(2m, 2.5m, 2m, 0.5m);
            Assert.AreEqual(expected, generated.Reduce(DecimalOperations.Multiply));
        }

        [TestMethod]
        public void DecimalOperationMultiplyFold2()
        {
            decimal expected = 25m;
            IEnumerable<decimal> generated =
                CollectionModule.Generate(2m, 2.5m, 2m, 0.5m);


            Assert.AreEqual(expected, generated.Fold2(generated, 1, DecimalOperations.Multiply3));
        }

        [TestMethod]
        public void DecimalOperationDivide()
        {
            decimal expected = 2.5m;
            Assert.AreEqual(expected, DecimalOperations.Divide(5, 2m));
        }

        [TestMethod]
        public void DecimalOperationDivideWith()
        {
            decimal expected = 2.5m;
            Assert.AreEqual(expected, DecimalOperations.DivideWith(5)(2m));
        }

        [TestMethod]
        public void DecimalOperationDivide3With1()
        {
            decimal expected = 2.5m;
            Assert.AreEqual(expected, DecimalOperations.Divide3With(10)(2, 2m));
        }

        [TestMethod]
        public void DecimalOperationDivide3With2()
        {
            decimal expected = 2.5m;
            Assert.AreEqual(expected, DecimalOperations.Divide3With(10, 2)(2m));
        }

        [TestMethod]
        public void DecimalOperationDivideReduction()
        {
            decimal expected = 5m;
            IEnumerable<decimal> generated = CollectionModule.Generate(10, 2, 2, 0.5m);
            Assert.AreEqual(expected, generated.Reduce(DecimalOperations.Divide));
        }

        [TestMethod]
        public void DecimalOperationDivideFold2()
        {
            decimal expected = 0.0025m;
            IEnumerable<decimal> generated =
                CollectionModule.Generate(10, 2, 2, 0.5m);


            Assert.AreEqual(expected, generated.Fold2(generated, 1, DecimalOperations.Divide3));
        }


    }
}
