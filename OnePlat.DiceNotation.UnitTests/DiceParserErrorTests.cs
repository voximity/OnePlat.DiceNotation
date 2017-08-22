using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnePlat.DiceNotation.DieRoller;

namespace OnePlat.DiceNotation.UnitTests
{
    /// <summary>
    /// Summary description for DiceParserErrorTests
    /// </summary>
    [TestClass]
    public class DiceParserErrorTests
    {
        IDieRoller roller = new ConstantDieRoller(2);

        public DiceParserErrorTests()
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
        public void DiceParser_UnrecognizedOperatorTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            Assert.ThrowsException<FormatException>(() => parser.Parse("1d20g4", true, roller));

            // validate results
        }

        [TestMethod]
        public void DiceParser_ParseSingleDieNoSidesTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            Assert.ThrowsException<FormatException>(() => parser.Parse("d", true, roller));

            // validate results
        }

        [TestMethod]
        public void DiceParser_ParseSingleDieNegativeSidesTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            Assert.ThrowsException<FormatException>(() => parser.Parse("3d-8", true, roller));

            // validate results
        }

        [TestMethod]
        public void DiceParser_ParseDiceNoSidesOperatorTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            Assert.ThrowsException<FormatException>(() => parser.Parse("2d+3", true, roller));

            // validate results
        }

        [TestMethod]
        public void DiceParser_ParseDiceDropNumberErrorTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            Assert.ThrowsException<FormatException>(() => parser.Parse("4d6l4", true, roller));

            // validate results
        }

        [TestMethod]
        public void DiceParser_ParseDiceOperatorNoValueTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            Assert.ThrowsException<FormatException>(() => parser.Parse("2d4x", true, roller));
            Assert.ThrowsException<FormatException>(() => parser.Parse("2d4/", true, roller));
            Assert.ThrowsException<FormatException>(() => parser.Parse("2d4k", true, roller));
            Assert.ThrowsException<FormatException>(() => parser.Parse("2d4l", true, roller));

            // validate results
        }

        [TestMethod]
        public void DiceParser_ParseDiceOperatorMultipleTimesTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            Assert.ThrowsException<FormatException>(() => parser.Parse("2d4k1k2", true, roller));
            Assert.ThrowsException<FormatException>(() => parser.Parse("2d4l1l2", true, roller));

            // validate results
        }

        [TestMethod]
        public void DiceParser_ParseRandomStringsTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            Assert.ThrowsException<FormatException>(() => parser.Parse("eosnddik+9", true, roller));
            Assert.ThrowsException<FormatException>(() => parser.Parse("2drk4/9", true, roller));
            Assert.ThrowsException<FormatException>(() => parser.Parse("7y+2d4k4", true, roller));
            Assert.ThrowsException<FormatException>(() => parser.Parse("7!y+2d4", true, roller));
            // validate results
        }

        [TestMethod]
        public void DiceParser_ParseDicePercentilErrorTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            Assert.ThrowsException<FormatException>(() => parser.Parse("2d6%3", true, roller));

            // validate results
        }
    }
}
