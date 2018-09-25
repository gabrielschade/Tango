using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tango.Modules;
using Tango.Types;

namespace Tango.Test.Modules
{
    [TestClass]
    public class ContinuationModuleTests

    {
        [TestMethod]
        public void ContinuationResolve()
        {
            int result = ContinuationModule.Resolve(10)
                                           .Match(value => value, _ => 0);

            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void ContinuationResolveTypedFail()
        {
            int result = ContinuationModule.Resolve<bool, int>(5)
                                           .Match(value => value, _ => 0);

            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void ContinuationReject()
        {
            string result = ContinuationModule.Reject("fail")
                                           .Match(_ => string.Empty, text => text);

            Assert.AreEqual("fail", result);
        }

        [TestMethod]
        public void ContinuationRejectTypedSuccess()
        {
            string result = ContinuationModule.Reject<string, int>("fail")
                                           .Match(_ => string.Empty, text => text);

            Assert.AreEqual("fail", result);
        }

        [TestMethod]
        public void ContinuationAllWithTwoContinuationSuccess()
        {
            var continuation1 = ContinuationModule.Resolve<string, int>(10);
            var continuation2 = ContinuationModule.Resolve<float, bool>(true);

            (int, bool) result =
            ContinuationModule.All(continuation1, continuation2)
                              .Match(values => values, _ => (0, false));

            Assert.AreEqual((10, true), result);
        }

        [TestMethod]
        public void ContinuationAllWithTwoContinuationFirstFailed()
        {
            var continuation1 = ContinuationModule.Reject<string, int>("fail");
            var continuation2 = ContinuationModule.Resolve<float, bool>(true);

            (int, bool) result =
            ContinuationModule.All(continuation1, continuation2)
                              .Match(values => values, _ => (0, false));

            Assert.AreEqual((0, false), result);
        }

        [TestMethod]
        public void ContinuationAllWithTwoContinuationSecondFailed()
        {
            var continuation1 = ContinuationModule.Resolve<string, int>(10);
            var continuation2 = ContinuationModule.Reject<float, bool>(3.5f);

            (int, bool) result =
            ContinuationModule.All(continuation1, continuation2)
                              .Match(values => values, _ => (0, false));

            Assert.AreEqual((0, false), result);
        }

        [TestMethod]
        public void ContinuationAllWithTwoContinuationBothFailed()
        {
            var continuation1 = ContinuationModule.Reject<string, int>("fail");
            var continuation2 = ContinuationModule.Reject<float, bool>(3.5f);

            (int, bool) result =
            ContinuationModule.All(continuation1, continuation2)
                              .Match(values => values, _ => (0, false));

            Assert.AreEqual((0, false), result);
        }

        [TestMethod]
        public void ContinuationAllWithThreeContinuationSuccess()
        {
            var continuation1 = ContinuationModule.Resolve<string, int>(10);
            var continuation2 = ContinuationModule.Resolve<float, bool>(true);
            var continuation3 = ContinuationModule.Resolve<bool, string>("success");

            (int, bool, string) result =
            ContinuationModule.All(continuation1, continuation2, continuation3)
                              .Match(values => values, _ => (0, false, string.Empty));

            Assert.AreEqual((10, true, "success"), result);
        }

        [TestMethod]
        public void ContinuationAllWithThreeContinuationFirstFailed()
        {
            var continuation1 = ContinuationModule.Reject<string, int>("fail");
            var continuation2 = ContinuationModule.Resolve<float, bool>(true);
            var continuation3 = ContinuationModule.Resolve<bool, string>("success");

            (int, bool, string) result =
            ContinuationModule.All(continuation1, continuation2, continuation3)
                              .Match(values => values, _ => (0, false, string.Empty));

            Assert.AreEqual((0, false, string.Empty), result);
        }

        [TestMethod]
        public void ContinuationAllWithThreeContinuationSecondFailed()
        {
            var continuation1 = ContinuationModule.Resolve<string, int>(10);
            var continuation2 = ContinuationModule.Reject<float, bool>(3.5f);
            var continuation3 = ContinuationModule.Resolve<bool, string>("success");

            (int, bool, string) result =
            ContinuationModule.All(continuation1, continuation2, continuation3)
                              .Match(values => values, _ => (0, false, string.Empty));

            Assert.AreEqual((0, false, string.Empty), result);
        }

        [TestMethod]
        public void ContinuationAllWithThreeContinuationThirdFailed()
        {
            var continuation1 = ContinuationModule.Resolve<string, int>(10);
            var continuation2 = ContinuationModule.Resolve<float, bool>(true);
            var continuation3 = ContinuationModule.Reject<bool, string>(false);

            (int, bool, string) result =
            ContinuationModule.All(continuation1, continuation2, continuation3)
                              .Match(values => values, _ => (0, false, string.Empty));

            Assert.AreEqual((0, false, string.Empty), result);
        }

        [TestMethod]
        public void ContinuationAllWithThreeContinuationFirstAndSecondFailed()
        {
            var continuation1 = ContinuationModule.Reject<string, int>("fail");
            var continuation2 = ContinuationModule.Reject<float, bool>(3.5f);
            var continuation3 = ContinuationModule.Resolve<bool, string>("success");

            (int, bool, string) result =
            ContinuationModule.All(continuation1, continuation2, continuation3)
                              .Match(values => values, _ => (0, false, string.Empty));

            Assert.AreEqual((0, false, string.Empty), result);
        }

        [TestMethod]
        public void ContinuationAllWithThreeContinuationSecondAndThirdFailed()
        {
            var continuation1 = ContinuationModule.Resolve<string, int>(10);
            var continuation2 = ContinuationModule.Reject<float, bool>(3.5f);
            var continuation3 = ContinuationModule.Reject<bool, string>(false);

            (int, bool, string) result =
            ContinuationModule.All(continuation1, continuation2, continuation3)
                              .Match(values => values, _ => (0, false, string.Empty));

            Assert.AreEqual((0, false, string.Empty), result);
        }

        [TestMethod]
        public void ContinuationAllWithThreeContinuationFirstAndThirdFailed()
        {
            var continuation1 = ContinuationModule.Reject<string, int>("fail");
            var continuation2 = ContinuationModule.Resolve<float, bool>(true);
            var continuation3 = ContinuationModule.Reject<bool, string>(false);

            (int, bool, string) result =
            ContinuationModule.All(continuation1, continuation2, continuation3)
                              .Match(values => values, _ => (0, false, string.Empty));

            Assert.AreEqual((0, false, string.Empty), result);
        }

        [TestMethod]
        public void ContinuationAllWithThreeContinuationAllFailed()
        {
            var continuation1 = ContinuationModule.Reject<string, int>("fail");
            var continuation2 = ContinuationModule.Reject<float, bool>(3.5f);
            var continuation3 = ContinuationModule.Reject<bool, string>(false);

            (int, bool, string) result =
            ContinuationModule.All(continuation1, continuation2, continuation3)
                              .Match(values => values, _ => (0, false, string.Empty));

            Assert.AreEqual((0, false, string.Empty), result);
        }

        [TestMethod]
        public void ContinuationAllWithFourContinuationSuccess()
        {
            var continuation1 = ContinuationModule.Resolve<string, int>(10);
            var continuation2 = ContinuationModule.Resolve<float, bool>(true);
            var continuation3 = ContinuationModule.Resolve<bool, string>("success");
            var continuation4 = ContinuationModule.Resolve<int, double>(22.5);

            (int, bool, string, double) result =
            ContinuationModule.All(continuation1, continuation2, continuation3, continuation4)
                              .Match(values => values, _ => (0, false, string.Empty, -1.0));

            Assert.AreEqual((10, true, "success", 22.5), result);
        }

        [TestMethod]
        public void ContinuationAllWithFourContinuationFirstFailed()
        {
            var continuation1 = ContinuationModule.Reject<string, int>("fail");
            var continuation2 = ContinuationModule.Resolve<float, bool>(true);
            var continuation3 = ContinuationModule.Resolve<bool, string>("success");
            var continuation4 = ContinuationModule.Resolve<int, double>(22.5);

            (int, bool, string, double) result =
            ContinuationModule.All(continuation1, continuation2, continuation3, continuation4)
                              .Match(values => values, _ => (0, false, string.Empty, -1.0));

            Assert.AreEqual((0, false, string.Empty, -1.0), result);
        }

        [TestMethod]
        public void ContinuationAllWithFourContinuationSecondFailed()
        {
            var continuation1 = ContinuationModule.Resolve<string, int>(10);
            var continuation2 = ContinuationModule.Reject<float, bool>(3.5f);
            var continuation3 = ContinuationModule.Resolve<bool, string>("success");
            var continuation4 = ContinuationModule.Resolve<int, double>(22.5);

            (int, bool, string, double) result =
            ContinuationModule.All(continuation1, continuation2, continuation3, continuation4)
                              .Match(values => values, _ => (0, false, string.Empty, -1.0));

            Assert.AreEqual((0, false, string.Empty, -1.0), result);
        }

        public void ContinuationAllWithFourContinuationThirdFailed()
        {
            var continuation1 = ContinuationModule.Resolve<string, int>(10);
            var continuation2 = ContinuationModule.Resolve<float, bool>(true);
            var continuation3 = ContinuationModule.Reject<bool, string>(false);
            var continuation4 = ContinuationModule.Resolve<int, double>(22.5);

            (int, bool, string, double) result =
            ContinuationModule.All(continuation1, continuation2, continuation3, continuation4)
                              .Match(values => values, _ => (0, false, string.Empty, -1.0));

            Assert.AreEqual((0, false, string.Empty, -1.0), result);
        }

        public void ContinuationAllWithFourContinuationFourthFailed()
        {
            var continuation1 = ContinuationModule.Resolve<string, int>(10);
            var continuation2 = ContinuationModule.Resolve<float, bool>(true);
            var continuation3 = ContinuationModule.Resolve<bool, string>("success");
            var continuation4 = ContinuationModule.Reject<int, double>(0);

            (int, bool, string, double) result =
            ContinuationModule.All(continuation1, continuation2, continuation3, continuation4)
                              .Match(values => values, _ => (0, false, string.Empty, -1.0));

            Assert.AreEqual((0, false, string.Empty, -1.0), result);
        }

        public void ContinuationAllWithFourContinuationAllFailed()
        {
            var continuation1 = ContinuationModule.Reject<string, int>("fail");
            var continuation2 = ContinuationModule.Reject<float, bool>(3.5f);
            var continuation3 = ContinuationModule.Reject<bool, string>(false);
            var continuation4 = ContinuationModule.Reject<int, double>(0);

            (int, bool, string, double) result =
            ContinuationModule.All(continuation1, continuation2, continuation3, continuation4)
                              .Match(values => values, _ => (0, false, string.Empty, -1.0));

            Assert.AreEqual((0, false, string.Empty, -1.0), result);
        }


    }
}
