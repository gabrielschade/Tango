using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tango.Functional;
using Tango.Types;

namespace Tango.Test.Functional
{
    [TestClass]
    public class FunctionExtensionTests

    {
        [TestMethod]
        public void ToFunctionWithoutParameters()
        {
            int expected = 20;
            int result = 0;
            Action action = () => result += 10;
            Func<Unit> function = action.ToFunction();

            action();
            function();

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void ToFunctionWith4Parameters()
        {
            int expected = 80;
            int result = 0;
            Action<int,int,int,int> action = (p1, p2, p3, p4) => result += p1 + p2 + p3 + p4;
            Func<int, int, int, int,Unit> function = action.ToFunction();

            action(10,10,5,15);
            function(10,10,5,15);

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void ToActionWith4Parameters()
        {
            int expected = 80;
            int result = 0;
            Func<int, int, int, int, Unit> function = (p1, p2, p3, p4) =>
             {
                 result += p1 + p2 + p3 + p4;
                 return new Unit();
             };
            Action<int, int, int, int> action = function.ToAction();

            action(10, 10, 5, 15);
            function(10, 10, 5, 15);

            Assert.AreEqual(result, expected);
        }


    }
}
