using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnePlat.DiceNotation.Converters;
using OnePlat.DiceNotation.DiceTerms;
using OnePlat.DiceNotation.DieRoller;
using System;
using System.Collections.Generic;

namespace OnePlat.DiceNotation.UnitTests.Converters
{
    /// <summary>
    /// Summary description for TermResultListConverterTests
    /// </summary>
    [TestClass]
    public class TermResultListConverterTests
    {
        private IDieRoller roller = new ConstantDieRoller(3);
        private IDice dice = new Dice();

        public TermResultListConverterTests()
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
        public void TermResultListConverter_ConstructorTest()
        {
            // setup test

            // run test
            TermResultListConverter conv = new TermResultListConverter();

            // validate results
            Assert.IsNotNull(conv);
            Assert.IsInstanceOfType(conv, typeof(TermResultListConverter));
        }

        [TestMethod]
        public void TermResultListConverter_ConvertTextTest()
        {
            // setup test
            TermResultListConverter conv = new TermResultListConverter();
            DiceResult diceResult = this.dice.Roll("d6", this.roller);

            // run test
            string result = conv.Convert(diceResult.Results, typeof(string), null, "en-us") as string;

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("3", result);
        }

        [TestMethod]
        public void TermResultListConverter_ConvertChooseTextTest()
        {
            // setup test
            TermResultListConverter conv = new TermResultListConverter();
            DiceResult diceResult = this.dice.Roll("6d6k3", this.roller);

            // run test
            string result = conv.Convert(diceResult.Results, typeof(string), null, "en-us") as string;

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("3, 3, 3, 3*, 3*, 3*", result);
        }

        [TestMethod]
        public void TermResultListConverter_ConvertComplexTextTest()
        {
            // setup test
            TermResultListConverter conv = new TermResultListConverter();
            DiceResult diceResult = this.dice.Roll("4d6k3 + d8 + 5", this.roller);

            // run test
            string result = conv.Convert(diceResult.Results, typeof(string), null, "en-us") as string;

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("3, 3, 3, 3*, 3", result);
        }

        [TestMethod]
        public void TermResultListConverter_ConvertEmptyResultListTest()
        {
            // setup test
            TermResultListConverter conv = new TermResultListConverter();
            IReadOnlyList<TermResult> list = new List<TermResult>();

            // run test
            string result = conv.Convert(list, typeof(string), null, "en-us") as string;

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void TermResultListConverter_ConvertErrorTargetTypeTest()
        {
            // setup test
            TermResultListConverter conv = new TermResultListConverter();
            DiceResult diceResult = this.dice.Roll("d20", this.roller);

            // run test
            Assert.ThrowsException<ArgumentException>(() => conv.Convert(diceResult.Results, typeof(int), null, "en-us"));

            // validate results
        }

        [TestMethod]
        public void TermResultListConverter_ConvertErrorValueNullTest()
        {
            // setup test
            TermResultListConverter conv = new TermResultListConverter();

            // run test
            Assert.ThrowsException<ArgumentNullException>(() => conv.Convert(null, typeof(string), null, "en-us"));

            // validate results
        }

        [TestMethod]
        public void TermResultListConverter_ConvertErrorValueTypeTest()
        {
            // setup test
            TermResultListConverter conv = new TermResultListConverter();
            string value = "testString";

            // run test
            Assert.ThrowsException<ArgumentException>(() => conv.Convert(value, typeof(string), null, "en-us"));

            // validate results
        }

        [TestMethod]
        public void TermResultListConverter_ConvertBackTest()
        {
            // setup test
            TermResultListConverter conv = new TermResultListConverter();
            string value = "testString";

            // run test
            Assert.ThrowsException<NotSupportedException>(() => conv.ConvertBack(value, typeof(string), null, "en-us"));

            // validate results
        }
    }
}
