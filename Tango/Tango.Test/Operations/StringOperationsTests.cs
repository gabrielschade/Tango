using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Tango.CommonOperations;
using Tango.Linq;
using Tango.Modules;

namespace Tango.Test.Types
{
    [TestClass]
    public class StringOperationsTests

    {
        [TestMethod]
        public void OperationConcat()
        {
            string expected = "Hello World";
            Assert.AreEqual(expected, StringOperations.Concat("Hello ", "World"));
        }

        [TestMethod]
        public void OperationConcatWith()
        {
            string expected = "Hello World";
            Assert.AreEqual(expected, StringOperations.ConcatWith("Hello ")("World"));
        }

        [TestMethod]
        public void OperationConcatInReduction()
        {
            string expected = "Hello World";
            IEnumerable<string> generated = CollectionModule.Generate("Hello", " ", "World");
            Assert.AreEqual(expected, generated.Reduce(StringOperations.Concat));
        }

        [TestMethod]
        public void OperationConcat3()
        {
            string expected = "Hello World";
            string generated = StringOperations.Concat3("Hello", " ", "World");
            Assert.AreEqual(expected, generated);
        }

        [TestMethod]
        public void OperationConcat3InFold22()
        {
            string expected = "Hello World";
            IEnumerable<string> generated1 = CollectionModule.Generate(" ");
            IEnumerable<string> generated2 = CollectionModule.Generate("World");
            Assert.AreEqual(expected, generated1.Fold2(generated2, "Hello", StringOperations.Concat3));
        }

        [TestMethod]
        public void OperationConcat3With1()
        {
            string expected = "Hello World";
            string generated = StringOperations.Concat3With("Hello")(" ", "World");
            Assert.AreEqual(expected, generated);
        }

        [TestMethod]
        public void OperationConcat3With2()
        {
            string expected = "Hello World";
            string generated = StringOperations.Concat3With("Hello", " ")("World");
            Assert.AreEqual(expected, generated);
        }

    }
}
