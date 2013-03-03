using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Math;

namespace Math.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class CalculatorTest
    {
        public CalculatorTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            Calculator calc = new Calculator();

            double pi = 3.14159265358979;

            // Test if negative numbers works
            Assert.AreEqual(-1, calc.Solve("-1"));

            // Decimals
            Assert.AreEqual(1.5, calc.Solve("1.5"));
            Assert.AreEqual(-1.5, calc.Solve("-1.5"));

            // All basic operations
            Assert.AreEqual(2, calc.Solve("1 + 1"));
            Assert.AreEqual(1, calc.Solve("2 + -1"));
            Assert.AreEqual(1, calc.Solve("2 - 1"));
            Assert.AreEqual(-8, calc.Solve("8 * -1"));
            Assert.AreEqual(System.Math.Pow(2, 8), calc.Solve("2^8"));
            Assert.AreEqual(System.Math.Pow(2, -8), calc.Solve("2^(-8)"));
            Assert.AreEqual(System.Math.Pow(2, 2/5), calc.Solve("2^(2/5)"));

            // Constants
            Assert.AreEqual(3.14159265358979, calc.Solve("pi"));

            // Functions
            Assert.AreEqual(System.Math.Max(2, 4), calc.Solve("max(2, 4)"));
            //Assert.AreEqual(System.Math.Sin(pi), calc.Solve("sin pi"));

            // Combination of the above
            //Assert.AreEqual(
        }
    }
}
