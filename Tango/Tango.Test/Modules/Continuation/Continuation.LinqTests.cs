using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tango.Modules;

namespace Tango.Test.Modules
{
    [TestClass]
    public class ContinuationLinqTest

    {
        [TestMethod]
        public void ContinuationAsContinuationWithSuccess()
        {
            int result = 5.AsContinuation<bool, int>()
                          .Match(value => value, _ => 0);

            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void ContinuationAsContinuationWithFail()
        {
            string result = "fail".AsContinuation<string,int>()
                                  .Match(_ => string.Empty, text => text);

            Assert.AreEqual("fail", result);
        }

        
    }
}
