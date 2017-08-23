using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnePlat.DiceNotation.DieRoller;

namespace OnePlat.DiceNotation.UnitTests
{
    /// <summary>
    /// Summary description for DiceConfigurationTests
    /// </summary>
    [TestClass]
    public class DiceConfigurationTests
    {
        IDieRoller roller = new ConstantDieRoller(2);

        public DiceConfigurationTests()
        {
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
        public void DiceConfiguration_SetUnboundedResultTest()
        {
            // setup test
            IDice dice = new Dice();

            // run test
            dice.Configuration.HasBoundedResult = false;
            DiceResult result = dice.Roll("d12-3", this.roller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("d12-3", result.DiceExpression);
            Assert.AreEqual(1, result.Results.Count);
            Assert.AreEqual(-1, result.Value);
        }

        [TestMethod]
        public void DiceConfiguration_SetBoundedResultMinimumTest()
        {
            // setup test
            IDice dice = new Dice();

            // run test
            dice.Configuration.BoundedResultMinimum = 3;
            DiceResult result = dice.Roll("d7-3", this.roller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("d7-3", result.DiceExpression);
            Assert.AreEqual(1, result.Results.Count);
            Assert.AreEqual(3, result.Value);
        }
    }
}
