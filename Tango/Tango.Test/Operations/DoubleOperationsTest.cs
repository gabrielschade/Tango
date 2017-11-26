using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Tango.CommonOperations;
using Tango.Linq;

namespace Tango.Test.Types
{
    [TestClass]
    public class DoubleOperationsTests

    {
        [TestMethod]
        public void OperationAdd()
        {
            double expected = 10.5 ;
            Assert.AreEqual(expected, DoubleOperations.Add(5, 5.5 ));
        }

        [TestMethod]
        public void OperationAddWith()
        {
            double expected = 10.5 ;
            Assert.AreEqual(expected, DoubleOperations.AddWith(5)(5.5 ));
        }

        [TestMethod]
        public void OperationAdd3With1()
        {
            double expected = 10.5 ;
            Assert.AreEqual(expected, DoubleOperations.Add3With(2)(3, 5.5 ));
        }

        [TestMethod]
        public void OperationAdd3With2()
        {
            double expected = 10.5 ;
            Assert.AreEqual(expected, DoubleOperations.Add3With(2, 3)(5.5 ));
        }

        [TestMethod]
        public void OperationAddReduction()
        {
            double expected = 10.5 ;
            IEnumerable<double> generated = Collection.Generate(2, 3, 5.3 , 0.2 );
            Assert.AreEqual(expected, generated.Reduce(DoubleOperations.Add));
        }

        [TestMethod]
        public void OperationAddFold2()
        {
            double expected = 21 ;
            IEnumerable<double> generated =
                Collection.Generate(2, 3, 5.3 , 0.2 );


            Assert.AreEqual(expected, generated.Fold2(generated, 0, DoubleOperations.Add3));
        }


        [TestMethod]
        public void OperationSubtract()
        {
            double expected = -0.5 ;
            Assert.AreEqual(expected, DoubleOperations.Subtract(5, 5.5 ));
        }

        [TestMethod]
        public void OperationSubtractWith()
        {
            double expected = -0.5 ;
            Assert.AreEqual(expected, DoubleOperations.SubtractWith(5)(5.5 ));
        }

        [TestMethod]
        public void OperationSubtract3With1()
        {
            double expected = -6.5 ;
            Assert.AreEqual(expected, DoubleOperations.Subtract3With(2)(3, 5.5 ));
        }

        [TestMethod]
        public void OperationSubtract3With2()
        {
            double expected = -6.5 ;
            Assert.AreEqual(expected, DoubleOperations.Subtract3With(2, 3)(5.5 ));
        }

        [TestMethod]
        public void OperationSubtractReduction()
        {
            double expected = -6.5 ;
            IEnumerable<double> generated = Collection.Generate(2, 3, 5.3 , 0.2 );
            Assert.AreEqual(expected, generated.Reduce(DoubleOperations.Subtract));
        }

        [TestMethod]
        public void OperationSubtractFold2()
        {
            double expected = -21 ;
            IEnumerable<double> generated =
                Collection.Generate(2, 3, 5.3 , 0.2 );


            Assert.AreEqual(expected, generated.Fold2(generated, 0, DoubleOperations.Subtract3));
        }


        [TestMethod]
        public void OperationMultiply()
        {
            double expected = 5 ;
            Assert.AreEqual(expected, DoubleOperations.Multiply(2, 2.5 ));
        }

        [TestMethod]
        public void OperationMultiplyWith()
        {
            double expected = 5 ;
            Assert.AreEqual(expected, DoubleOperations.MultiplyWith(2)(2.5 ));
        }

        [TestMethod]
        public void OperationMultiply3With1()
        {
            double expected = 5 ;
            Assert.AreEqual(expected, DoubleOperations.Multiply3With(2)(1, 2.5 ));
        }

        [TestMethod]
        public void OperationMultiply3With2()
        {
            double expected = 5 ;
            Assert.AreEqual(expected, DoubleOperations.Multiply3With(2, 1)(2.5 ));
        }

        [TestMethod]
        public void OperationMultiplyReduction()
        {
            double expected = 5 ;
            IEnumerable<double> generated = Collection.Generate(2 , 2.5 , 2 , 0.5 );
            Assert.AreEqual(expected, generated.Reduce(DoubleOperations.Multiply));
        }

        [TestMethod]
        public void OperationMultiplyFold2()
        {
            double expected = 25 ;
            IEnumerable<double> generated =
                Collection.Generate(2 , 2.5 , 2 , 0.5 );


            Assert.AreEqual(expected, generated.Fold2(generated, 1, DoubleOperations.Multiply3));
        }

        [TestMethod]
        public void OperationDivide()
        {
            double expected = 2.5 ;
            Assert.AreEqual(expected, DoubleOperations.Divide(5, 2 ));
        }

        [TestMethod]
        public void OperationDivideWith()
        {
            double expected = 2.5 ;
            Assert.AreEqual(expected, DoubleOperations.DivideWith(5)(2 ));
        }

        [TestMethod]
        public void OperationDivide3With1()
        {
            double expected = 2.5 ;
            Assert.AreEqual(expected, DoubleOperations.Divide3With(10)(2, 2 ));
        }

        [TestMethod]
        public void OperationDivide3With2()
        {
            double expected = 2.5 ;
            Assert.AreEqual(expected, DoubleOperations.Divide3With(10, 2)(2 ));
        }

        [TestMethod]
        public void OperationDivideReduction()
        {
            double expected = 5 ;
            IEnumerable<double> generated = Collection.Generate(10, 2, 2, 0.5 );
            Assert.AreEqual(expected, generated.Reduce(DoubleOperations.Divide));
        }

        [TestMethod]
        public void OperationDivideFold2()
        {
            double expected = 0.0025 ;
            IEnumerable<double> generated =
                Collection.Generate(10, 2, 2, 0.5 );


            Assert.AreEqual(expected, generated.Fold2(generated, 1, DoubleOperations.Divide3));
        }


    }
}
