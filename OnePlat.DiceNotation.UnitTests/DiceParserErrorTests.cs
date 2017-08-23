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
        private DiceConfiguration config = new DiceConfiguration();
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
            Assert.ThrowsException<FormatException>(() => parser.Parse("1d20g4", this.config, roller));

            // validate results
        }

        [TestMethod]
        public void DiceParser_ParseDiceDropNumberErrorTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            Assert.ThrowsException<FormatException>(() => parser.Parse("4d6l4", this.config, roller));

            // validate results
        }

        [TestMethod]
        public void DiceParser_ParseDiceOperatorNoValueTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            Assert.ThrowsException<FormatException>(() => parser.Parse("2d4x", this.config, roller));
            Assert.ThrowsException<FormatException>(() => parser.Parse("2d4/", this.config, roller));
            Assert.ThrowsException<FormatException>(() => parser.Parse("2d4k", this.config, roller));
            Assert.ThrowsException<FormatException>(() => parser.Parse("2d4l", this.config, roller));

            // validate results
        }

        [TestMethod]
        public void DiceParser_ParseDiceOperatorMultipleTimesTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            Assert.ThrowsException<FormatException>(() => parser.Parse("2d4k1k2", this.config, roller));
            Assert.ThrowsException<FormatException>(() => parser.Parse("2d4l1l2", this.config, roller));

            // validate results
        }

        [TestMethod]
        public void DiceParser_ParseRandomStringsTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            Assert.ThrowsException<FormatException>(() => parser.Parse("eosnddik+9", this.config, roller));
            Assert.ThrowsException<FormatException>(() => parser.Parse("2drk4/9", this.config, roller));
            Assert.ThrowsException<FormatException>(() => parser.Parse("7y+2d4k4", this.config, roller));
            Assert.ThrowsException<FormatException>(() => parser.Parse("7!y+2d4", this.config, roller));
            // validate results
        }

        [TestMethod]
        public void DiceParser_ParseDicePercentilErrorTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            Assert.ThrowsException<FormatException>(() => parser.Parse("2d6%3", this.config, roller));

            // validate results
        }
    }
}
