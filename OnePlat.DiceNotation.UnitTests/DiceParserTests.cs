﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnePlat.DiceNotation.DieRoller;
using System;

namespace OnePlat.DiceNotation.UnitTests
{
    /// <summary>
    /// Summary description for DiceParserTests
    /// </summary>
    [TestClass]
    public class DiceParserTests
    {
        private IDieRoller testRoller = new ConstantDieRoller(2);

        public DiceParserTests()
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
        public void DiceParser_ConstructorTest()
        {
            // setup test

            // run test
            DiceParser parser = new DiceParser();

            // validate results
            Assert.IsNotNull(parser);
            Assert.IsInstanceOfType(parser, typeof(DiceParser));
        }

        [TestMethod]
        public void DiceParser_ParseSimpleDiceTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            DiceResult result = parser.Parse("3d6", true, this.testRoller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("3d6", result.DiceExpression);
            Assert.AreEqual(3, result.Results.Count);
            Assert.AreEqual(6, result.Value);
        }

        [TestMethod]
        public void DiceParser_ParseSingleDieTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            DiceResult result = parser.Parse("d20", true, this.testRoller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("d20", result.DiceExpression);
            Assert.AreEqual(1, result.Results.Count);
            Assert.AreEqual(2, result.Value);
        }

        [TestMethod]
        public void DiceParser_ParseDiceWithModifierTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            DiceResult result = parser.Parse("2d4+3", true, this.testRoller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("2d4+3", result.DiceExpression);
            Assert.AreEqual(2, result.Results.Count);
            Assert.AreEqual(7, result.Value);
        }

        [TestMethod]
        public void DiceParser_ParseDiceWithNegativeModifierTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            DiceResult result = parser.Parse("d12-2", true, this.testRoller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("d12-2", result.DiceExpression);
            Assert.AreEqual(1, result.Results.Count);
            Assert.AreEqual(1, result.Value);
        }

        [TestMethod]
        public void DiceParser_ParseDiceWithChooseTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            DiceResult result = parser.Parse("4d6k3", true, this.testRoller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("4d6k3", result.DiceExpression);
            Assert.AreEqual(3, result.Results.Count);
            Assert.AreEqual(6, result.Value);
        }

        [TestMethod]
        public void DiceParser_ParseDiceWithWhitepaceTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            DiceResult result = parser.Parse(" 4  d6 k 3+  2    ", true, this.testRoller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("4d6k3+2", result.DiceExpression);
            Assert.AreEqual(3, result.Results.Count);
            Assert.AreEqual(8, result.Value);
        }

        [TestMethod]
        public void DiceParser_ParseDiceWithChainedExpressionTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            DiceResult result = parser.Parse("4d6k3 + d8 + 2", true, this.testRoller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("4d6k3+d8+2", result.DiceExpression);
            Assert.AreEqual(4, result.Results.Count);
            Assert.AreEqual(10, result.Value);
        }

        [TestMethod]
        public void DiceParser_ParseDiceWithMultiplyAfterTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            DiceResult result = parser.Parse("2d8x10", true, this.testRoller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("2d8x10", result.DiceExpression);
            Assert.AreEqual(2, result.Results.Count);
            Assert.AreEqual(40, result.Value);
        }

        [TestMethod]
        public void DiceParser_ParseDiceWithMultiplyBeforeTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            DiceResult result = parser.Parse("10*2d8", true, this.testRoller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("10*2d8", result.DiceExpression);
            Assert.AreEqual(2, result.Results.Count);
            Assert.AreEqual(40, result.Value);
        }

        [TestMethod]
        public void DiceParser_ParseDiceWithDivideTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            DiceResult result = parser.Parse("3d10 / 2", true, this.testRoller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("3d10/2", result.DiceExpression);
            Assert.AreEqual(3, result.Results.Count);
            Assert.AreEqual(3, result.Value);
        }

        [TestMethod]
        public void DiceParser_ParseDiceWithDivideBeforeTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            DiceResult result = parser.Parse("40 / 1d6", true, this.testRoller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("40/1d6", result.DiceExpression);
            Assert.AreEqual(1, result.Results.Count);
            Assert.AreEqual(20, result.Value);
        }

        [TestMethod]
        public void DiceParser_ParseDiceWithChainedOrderTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            DiceResult result = parser.Parse("2 + 4d6k3 + d8", true, this.testRoller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("2+4d6k3+d8", result.DiceExpression);
            Assert.AreEqual(4, result.Results.Count);
            Assert.AreEqual(10, result.Value);
        }

        [TestMethod]
        public void DiceParser_ParseConstantOnlyTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            DiceResult result = parser.Parse("42", true, this.testRoller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("42", result.DiceExpression);
            Assert.AreEqual(0, result.Results.Count);
            Assert.AreEqual(42, result.Value);
        }

        [TestMethod]
        public void DiceParser_ParseConstantOnlyAdditionTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            DiceResult result = parser.Parse("4 + 2", true, this.testRoller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("4+2", result.DiceExpression);
            Assert.AreEqual(0, result.Results.Count);
            Assert.AreEqual(6, result.Value);
        }

        [TestMethod]
        public void DiceParser_ParseConstantOnlyMultiplyTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            DiceResult result = parser.Parse("4x2", true, this.testRoller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("4x2", result.DiceExpression);
            Assert.AreEqual(0, result.Results.Count);
            Assert.AreEqual(8, result.Value);
        }

        [TestMethod]
        public void DiceParser_ParseConstantOnlyDivideTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            DiceResult result = parser.Parse("4/2", true, this.testRoller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("4/2", result.DiceExpression);
            Assert.AreEqual(0, result.Results.Count);
            Assert.AreEqual(2, result.Value);
        }

        [TestMethod]
        public void DiceParser_ParseDiceSubractOrderTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            DiceResult result = parser.Parse("100 - 2d12", true, this.testRoller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("100-2d12", result.DiceExpression);
            Assert.AreEqual(2, result.Results.Count);
            Assert.AreEqual(96, result.Value);
        }

        [TestMethod]
        public void DiceParser_ParseDiceNegativeConstantTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            DiceResult result = parser.Parse("-5 + 4d6", true, this.testRoller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("-5+4d6", result.DiceExpression);
            Assert.AreEqual(4, result.Results.Count);
            Assert.AreEqual(3, result.Value);
        }

        [TestMethod]
        public void DiceParser_ParseDiceMultipleConstantsTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            DiceResult result = parser.Parse("6 + d20 - 3", true, this.testRoller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("6+d20-3", result.DiceExpression);
            Assert.AreEqual(1, result.Results.Count);
            Assert.AreEqual(5, result.Value);
        }

        [TestMethod]
        public void DiceParser_ParseDiceMultipleConstantsOrderTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            DiceResult result = parser.Parse("2+1d20+2+3x3-10", true, this.testRoller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("2+1d20+2+3x3-10", result.DiceExpression);
            Assert.AreEqual(1, result.Results.Count);
            Assert.AreEqual(5, result.Value);
        }

        [TestMethod]
        public void DiceParser_ParseDicePercentileTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            DiceResult result = parser.Parse("d%+5", true, this.testRoller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("d100+5", result.DiceExpression);
            Assert.AreEqual(1, result.Results.Count);
            Assert.AreEqual(7, result.Value);
        }
    }
}
