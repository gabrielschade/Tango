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

    }
}
