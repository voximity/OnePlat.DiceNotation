using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OnePlat.DiceNotation.UnitTests
{
    /// <summary>
    /// Summary description for DiceParserErrorTests
    /// </summary>
    [TestClass]
    public class DiceParserErrorTests
    {
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
            Assert.ThrowsException<ArgumentException>(() => parser.Parse("1d20g4"));

            // validate results
        }

        [TestMethod]
        public void DiceParser_ParseSingleDieNoSidesTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            Assert.ThrowsException<FormatException>(() => parser.Parse("d"));

            // validate results
        }

        [TestMethod]
        public void DiceParser_ParseSingleDieNegativeSidesTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            Assert.ThrowsException<FormatException>(() => parser.Parse("3d-8"));

            // validate results
        }

        [TestMethod]
        public void DiceParser_ParseDiceNoSidesOperatorTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            Assert.ThrowsException<FormatException>(() => parser.Parse("2d+3"));

            // validate results
        }

        [TestMethod]
        public void DiceParser_ParseDiceOperatorNoValueTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            Assert.ThrowsException<FormatException>(() => parser.Parse("2d4x"));
            Assert.ThrowsException<FormatException>(() => parser.Parse("2d4/"));
            Assert.ThrowsException<FormatException>(() => parser.Parse("2d4k"));

            // validate results
        }

        [TestMethod]
        public void DiceParser_ParseRandomStringsTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            Assert.ThrowsException<ArgumentException>(() => parser.Parse("eosnddik+9"));
            Assert.ThrowsException<FormatException>(() => parser.Parse("2drk4/9"));
            Assert.ThrowsException<ArgumentException>(() => parser.Parse("7y+2d4k4"));

            // validate results
        }
    }
}
