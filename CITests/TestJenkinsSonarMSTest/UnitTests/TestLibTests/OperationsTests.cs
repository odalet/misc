using TestLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// Tests
namespace TestLib.Tests
{
    [TestClass()]
    public class OperationsTests
    {
        [TestMethod()]
        public void AddTest()
        {
            Assert.AreEqual(Operations.Add(1.0, 1.0), 2.0);
        }

        [TestMethod()]
        public void SubtractTest()
        {
            Assert.AreEqual(Operations.Subtract(1.0, 1.0), 0.0);
        }

        [TestMethod()]
        public void MultiplyTest()
        {
            Assert.AreEqual(Operations.Multiply(1.0, 1.0), 1.0);
        }

        [TestMethod()]
        public void DivideTest()
        {
            Assert.AreEqual(Operations.Divide(1.0, 1.0), 1.0);
        }

        [TestMethod()]
        public void PowTest()
        {
            Assert.AreEqual(Operations.Pow(1.0, 1.0), 1.0);
        }

        [TestMethod()]
        public void SqrtTest()
        {
            Assert.AreEqual(Operations.Sqrt(1.0), 1.0);
        }
    }
}