using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Tango.CommonOperations;
using Tango.Linq;
using Tango.Modules;

namespace Tango.Test.Types
{
    [TestClass]
    public class BoolOperationsTests

    {
        [TestMethod]
        public void OperationAndTrue()
        {
            bool expected = true;
            Assert.AreEqual(expected, BoolOperations.And(true, true));
        }

        [TestMethod]
        public void OperationAndFalse()
        {
            bool expected = false;
            Assert.AreEqual(expected, BoolOperations.And(true, false));
        }

        [TestMethod]
        public void OperationAndTruePartialApplied()
        {
            bool expected = true;
            Assert.AreEqual(expected, BoolOperations.AndWith(true)(true));
        }

        [TestMethod]
        public void OperationAndFalsePartialApplied()
        {
            bool expected = false;
            Assert.AreEqual(expected, BoolOperations.AndWith(true)(false));
        }

        [TestMethod]
        public void OperationAndInReductionForTrue()
        {
            bool expected = true;
            IEnumerable<bool> generated = 
                CollectionModule.Generate(true, true, true, true);
            Assert.AreEqual(expected, generated.Reduce(BoolOperations.And));
        }

        [TestMethod]
        public void OperationAndInReductionForFalse()
        {
            bool expected = false;
            IEnumerable<bool> generated =
                CollectionModule.Generate(false, true, false, true);
            Assert.AreEqual(expected, generated.Reduce(BoolOperations.And));
        }


        [TestMethod]
        public void OperationOrTrue()
        {
            bool expected = true;
            Assert.AreEqual(expected, BoolOperations.Or(true, false));
        }

        [TestMethod]
        public void OperationOrFalse()
        {
            bool expected = false;
            Assert.AreEqual(expected, BoolOperations.Or(false, false));
        }

        [TestMethod]
        public void OperationOrTruePartialApplied()
        {
            bool expected = true;
            Assert.AreEqual(expected, BoolOperations.OrWith(true)(false));
        }

        [TestMethod]
        public void OperationOrFalsePartialApplied()
        {
            bool expected = false;
            Assert.AreEqual(expected, BoolOperations.OrWith(false)(false));
        }

        [TestMethod]
        public void OperationOrInReductionForTrue()
        {
            bool expected = true;
            IEnumerable<bool> generated =
                CollectionModule.Generate(false, false, true, false);
            Assert.AreEqual(expected, generated.Reduce(BoolOperations.Or));
        }

        [TestMethod]
        public void OperationOrInReductionForFalse()
        {
            bool expected = false;
            IEnumerable<bool> generated =
                CollectionModule.Generate(false, false, false, false);
            Assert.AreEqual(expected, generated.Reduce(BoolOperations.Or));
        }

        [TestMethod]
        public void OperationNotFalse()
        {
            bool expected = false;
            Assert.AreEqual(expected, BoolOperations.Not(true));
        }


        [TestMethod]
        public void OperationNotTrue()
        {
            bool expected = true;
            Assert.AreEqual(expected, BoolOperations.Not(false));
        }
    }
}
