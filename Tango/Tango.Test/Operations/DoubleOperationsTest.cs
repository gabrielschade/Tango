using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Tango.CommonOperations;
using Tango.Linq;
using Tango.Modules;

namespace Tango.Test.Operations
{
    [TestClass]
    public class DoubleOperationsTests

    {
        [TestMethod]
        public void DoubleOperationAdd()
        {
            double expected = 10.5 ;
            Assert.AreEqual(expected, DoubleOperations.Add(5, 5.5 ));
        }

        [TestMethod]
        public void DoubleOperationAddWith()
        {
            double expected = 10.5 ;
            Assert.AreEqual(expected, DoubleOperations.AddWith(5)(5.5 ));
        }

        [TestMethod]
        public void DoubleOperationAdd3With1()
        {
            double expected = 10.5 ;
            Assert.AreEqual(expected, DoubleOperations.Add3With(2)(3, 5.5 ));
        }

        [TestMethod]
        public void DoubleOperationAdd3With2()
        {
            double expected = 10.5 ;
            Assert.AreEqual(expected, DoubleOperations.Add3With(2, 3)(5.5 ));
        }

        [TestMethod]
        public void DoubleOperationAddReduction()
        {
            double expected = 10.5 ;
            IEnumerable<double> generated = CollectionModule.Generate(2, 3, 5.3 , 0.2 );
            Assert.AreEqual(expected, generated.Reduce(DoubleOperations.Add));
        }

        [TestMethod]
        public void DoubleOperationAddFold2()
        {
            double expected = 21 ;
            IEnumerable<double> generated =
                CollectionModule.Generate(2, 3, 5.3 , 0.2 );


            Assert.AreEqual(expected, generated.Fold2(generated, 0, DoubleOperations.Add3));
        }


        [TestMethod]
        public void DoubleOperationSubtract()
        {
            double expected = -0.5 ;
            Assert.AreEqual(expected, DoubleOperations.Subtract(5, 5.5 ));
        }

        [TestMethod]
        public void DoubleOperationSubtractWith()
        {
            double expected = -0.5 ;
            Assert.AreEqual(expected, DoubleOperations.SubtractWith(5)(5.5 ));
        }

        [TestMethod]
        public void DoubleOperationSubtract3With1()
        {
            double expected = -6.5 ;
            Assert.AreEqual(expected, DoubleOperations.Subtract3With(2)(3, 5.5 ));
        }

        [TestMethod]
        public void DoubleOperationSubtract3With2()
        {
            double expected = -6.5 ;
            Assert.AreEqual(expected, DoubleOperations.Subtract3With(2, 3)(5.5 ));
        }

        [TestMethod]
        public void DoubleOperationSubtractReduction()
        {
            double expected = -6.5 ;
            IEnumerable<double> generated = CollectionModule.Generate(2, 3, 5.3 , 0.2 );
            Assert.AreEqual(expected, generated.Reduce(DoubleOperations.Subtract));
        }

        [TestMethod]
        public void DoubleOperationSubtractFold2()
        {
            double expected = -21 ;
            IEnumerable<double> generated =
                CollectionModule.Generate(2, 3, 5.3 , 0.2 );


            Assert.AreEqual(expected, generated.Fold2(generated, 0, DoubleOperations.Subtract3));
        }


        [TestMethod]
        public void DoubleOperationMultiply()
        {
            double expected = 5 ;
            Assert.AreEqual(expected, DoubleOperations.Multiply(2, 2.5 ));
        }

        [TestMethod]
        public void DoubleOperationMultiplyWith()
        {
            double expected = 5 ;
            Assert.AreEqual(expected, DoubleOperations.MultiplyWith(2)(2.5 ));
        }

        [TestMethod]
        public void DoubleOperationMultiply3With1()
        {
            double expected = 5 ;
            Assert.AreEqual(expected, DoubleOperations.Multiply3With(2)(1, 2.5 ));
        }

        [TestMethod]
        public void DoubleOperationMultiply3With2()
        {
            double expected = 5 ;
            Assert.AreEqual(expected, DoubleOperations.Multiply3With(2, 1)(2.5 ));
        }

        [TestMethod]
        public void DoubleOperationMultiplyReduction()
        {
            double expected = 5 ;
            IEnumerable<double> generated = CollectionModule.Generate(2 , 2.5 , 2 , 0.5 );
            Assert.AreEqual(expected, generated.Reduce(DoubleOperations.Multiply));
        }

        [TestMethod]
        public void DoubleOperationMultiplyFold2()
        {
            double expected = 25 ;
            IEnumerable<double> generated =
                CollectionModule.Generate(2 , 2.5 , 2 , 0.5 );


            Assert.AreEqual(expected, generated.Fold2(generated, 1, DoubleOperations.Multiply3));
        }

        [TestMethod]
        public void DoubleOperationDivide()
        {
            double expected = 2.5 ;
            Assert.AreEqual(expected, DoubleOperations.Divide(5, 2 ));
        }

        [TestMethod]
        public void DoubleOperationDivideWith()
        {
            double expected = 2.5 ;
            Assert.AreEqual(expected, DoubleOperations.DivideWith(5)(2 ));
        }

        [TestMethod]
        public void DoubleOperationDivide3With1()
        {
            double expected = 2.5 ;
            Assert.AreEqual(expected, DoubleOperations.Divide3With(10)(2, 2 ));
        }

        [TestMethod]
        public void DoubleOperationDivide3With2()
        {
            double expected = 2.5 ;
            Assert.AreEqual(expected, DoubleOperations.Divide3With(10, 2)(2 ));
        }

        [TestMethod]
        public void DoubleOperationDivideReduction()
        {
            double expected = 5 ;
            IEnumerable<double> generated = CollectionModule.Generate(10, 2, 2, 0.5 );
            Assert.AreEqual(expected, generated.Reduce(DoubleOperations.Divide));
        }

        [TestMethod]
        public void DoubleOperationDivideFold2()
        {
            double expected = 0.0025 ;
            IEnumerable<double> generated =
                CollectionModule.Generate(10, 2, 2, 0.5 );


            Assert.AreEqual(expected, generated.Fold2(generated, 1, DoubleOperations.Divide3));
        }


    }
}
