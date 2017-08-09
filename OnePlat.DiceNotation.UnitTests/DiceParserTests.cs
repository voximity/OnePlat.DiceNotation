using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OnePlat.DiceNotation.UnitTests
{
    /// <summary>
    /// Summary description for DiceParserTests
    /// </summary>
    [TestClass]
    public class DiceParserTests
    {
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
            IDice dice = parser.Parse("3d6");

            // validate results
            Assert.IsNotNull(dice);
            Assert.AreEqual("3d6", dice.ToString());
        }

        [TestMethod]
        public void DiceParser_ParseSingleDieTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            IDice dice = parser.Parse("d20");

            // validate results
            Assert.IsNotNull(dice);
            Assert.AreEqual("1d20", dice.ToString());
        }

        [TestMethod]
        public void DiceParser_ParseDiceWithModifierTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            IDice dice = parser.Parse("2d4+3");

            // validate results
            Assert.IsNotNull(dice);
            Assert.AreEqual("2d4 + 3", dice.ToString());
        }

        [TestMethod]
        public void DiceParser_ParseDiceWithNegativeModifierTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            IDice dice = parser.Parse("d12-2");

            // validate results
            Assert.IsNotNull(dice);
            Assert.AreEqual("1d12 + -2", dice.ToString());
        }

        [TestMethod]
        public void DiceParser_ParseDiceWithChooseTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            IDice dice = parser.Parse("4d6k3");

            // validate results
            Assert.IsNotNull(dice);
            Assert.AreEqual("4d6k3", dice.ToString());
        }

        [TestMethod]
        public void DiceParser_ParseDiceWithWhitepaceTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            IDice dice = parser.Parse(" 4  d6 k 3+  2    ");

            // validate results
            Assert.IsNotNull(dice);
            Assert.AreEqual("4d6k3 + 2", dice.ToString());
        }

        [TestMethod]
        public void DiceParser_ParseDiceWithChainedExpressionTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            IDice dice = parser.Parse("4d6k3 + d8 + 2");

            // validate results
            Assert.IsNotNull(dice);
            Assert.AreEqual("4d6k3 + 1d8 + 2", dice.ToString());
        }

        [TestMethod]
        public void DiceParser_ParseDiceWithMultiplyTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            IDice dice = parser.Parse("2d8x10");

            // validate results
            Assert.IsNotNull(dice);
            Assert.AreEqual("2d8x10", dice.ToString());
        }

        [TestMethod]
        public void DiceParser_ParseDiceWithDivideTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            IDice dice = parser.Parse("3d10 / 2");

            // validate results
            Assert.IsNotNull(dice);
            Assert.AreEqual("3d10/10", dice.ToString());
        }
    }
}
