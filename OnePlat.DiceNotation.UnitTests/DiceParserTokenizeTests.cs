using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace OnePlat.DiceNotation.UnitTests
{
    /// <summary>
    /// Summary description for DiceParserTokenizeTests
    /// </summary>
    [TestClass]
    public class DiceParserTokenizeTests
    {
        public DiceParserTokenizeTests()
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
        public void DiceParser_TokenizeSimpleTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            List<string> result = parser.Tokenize("d20");

            // validate results
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("d20", result[0]);
        }

        [TestMethod]
        public void DiceParser_TokenizeSimpleConstantTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            List<string> result = parser.Tokenize("42");

            // validate results
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("42", result[0]);
        }

        [TestMethod]
        public void DiceParser_TokenizeSimpleModifierTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            List<string> result = parser.Tokenize("1d20+10");

            // validate results
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("1d20", result[0]);
            Assert.AreEqual("+", result[1]);
            Assert.AreEqual("10", result[2]);
        }

        [TestMethod]
        public void DiceParser_TokenizeModifierFirstTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            List<string> result = parser.Tokenize("2+d6");

            // validate results
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("2", result[0]);
            Assert.AreEqual("+", result[1]);
            Assert.AreEqual("d6", result[2]);
        }

        [TestMethod]
        public void DiceParser_TokenizeNegativeModifierTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            List<string> result = parser.Tokenize("1d20-1");

            // validate results
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("1d20", result[0]);
            Assert.AreEqual("-", result[1]);
            Assert.AreEqual("1", result[2]);
        }

        [TestMethod]
        public void DiceParser_TokenizeMultiplyTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            List<string> result = parser.Tokenize("2d6x10");

            // validate results
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("2d6", result[0]);
            Assert.AreEqual("x", result[1]);
            Assert.AreEqual("10", result[2]);
        }

        [TestMethod]
        public void DiceParser_TokenizeMultiplyFirstTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            List<string> result = parser.Tokenize("10x2d6");

            // validate results
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("10", result[0]);
            Assert.AreEqual("x", result[1]);
            Assert.AreEqual("2d6", result[2]);
        }

        [TestMethod]
        public void DiceParser_TokenizeDivideTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            List<string> result = parser.Tokenize("2d6/10");

            // validate results
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("2d6", result[0]);
            Assert.AreEqual("/", result[1]);
            Assert.AreEqual("10", result[2]);
        }

        [TestMethod]
        public void DiceParser_TokenizeDivideFirstTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            List<string> result = parser.Tokenize("10/2d6");

            // validate results
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("10", result[0]);
            Assert.AreEqual("/", result[1]);
            Assert.AreEqual("2d6", result[2]);
        }

        [TestMethod]
        public void DiceParser_TokenizeChooseTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            List<string> result = parser.Tokenize("4d6k3");

            // validate results
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("4d6k3", result[0]);
        }

        [TestMethod]
        public void DiceParser_TokenizeChainedDiceExpressionTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            List<string> result = parser.Tokenize("4d6k3 + d8 + 3");

            // validate results
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual("4d6k3", result[0]);
            Assert.AreEqual("+", result[1]);
            Assert.AreEqual("d8", result[2]);
            Assert.AreEqual("+", result[3]);
            Assert.AreEqual("3", result[4]);
        }

        [TestMethod]
        public void DiceParser_TokenizeUnaryOperatorTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            List<string> result = parser.Tokenize("-1 + 4d6k3");

            // validate results
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("-1", result[0]);
            Assert.AreEqual("+", result[1]);
            Assert.AreEqual("4d6k3", result[2]);
        }

        [TestMethod]
        public void DiceParser_TokenizeUnaryOperatorPositiveTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            List<string> result = parser.Tokenize("+1 + 4d6k3");

            // validate results
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("+1", result[0]);
            Assert.AreEqual("+", result[1]);
            Assert.AreEqual("4d6k3", result[2]);
        }

        [TestMethod]
        public void DiceParser_TokenizeParenthesesSimpleTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            List<string> result = parser.Tokenize("4d6k3 + (d8 - 2)");

            // validate results
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.AreEqual(7, result.Count);
            Assert.AreEqual("4d6k3", result[0]);
            Assert.AreEqual("+", result[1]);
            Assert.AreEqual("(", result[2]);
            Assert.AreEqual("d8", result[3]);
            Assert.AreEqual("-", result[4]);
            Assert.AreEqual("2", result[5]);
            Assert.AreEqual(")", result[6]);
        }

        [TestMethod]
        public void DiceParser_TokenizeParenthesesMuliplyTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            List<string> result = parser.Tokenize("(2d6 + 1)x2");

            // validate results
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.AreEqual(7, result.Count);
            Assert.AreEqual("(", result[0]);
            Assert.AreEqual("2d6", result[1]);
            Assert.AreEqual("+", result[2]);
            Assert.AreEqual("1", result[3]);
            Assert.AreEqual(")", result[4]);
            Assert.AreEqual("x", result[5]);
            Assert.AreEqual("2", result[6]);
        }

        [TestMethod]
        public void DiceParser_TokenizeDefaultMuliplyTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            List<string> result = parser.Tokenize("(2d6 + 1)2");

            // validate results
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.AreEqual(7, result.Count);
            Assert.AreEqual("(", result[0]);
            Assert.AreEqual("2d6", result[1]);
            Assert.AreEqual("+", result[2]);
            Assert.AreEqual("1", result[3]);
            Assert.AreEqual(")", result[4]);
            Assert.AreEqual("x", result[5]);
            Assert.AreEqual("2", result[6]);
        }

        [TestMethod]
        public void DiceParser_TokenizeDefaultMuliplyFirstTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            List<string> result = parser.Tokenize("2(2d6 + 1)");

            // validate results
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.AreEqual(7, result.Count);
            Assert.AreEqual("2", result[0]);
            Assert.AreEqual("x", result[1]);
            Assert.AreEqual("(", result[2]);
            Assert.AreEqual("2d6", result[3]);
            Assert.AreEqual("+", result[4]);
            Assert.AreEqual("1", result[5]);
            Assert.AreEqual(")", result[6]);
        }

        [TestMethod]
        public void DiceParser_TokenizeParenthesesNestedTest()
        {
            // setup test
            DiceParser parser = new DiceParser();

            // run test
            List<string> result = parser.Tokenize("4d6k3 - (2 + 3) + (2(d8 - 2))");

            // validate results
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.AreEqual(17, result.Count);
            Assert.AreEqual("4d6k3", result[0]);
            Assert.AreEqual("-", result[1]);
            Assert.AreEqual("(", result[2]);
            Assert.AreEqual("2", result[3]);
            Assert.AreEqual("+", result[4]);
            Assert.AreEqual("3", result[5]);
            Assert.AreEqual(")", result[6]);
            Assert.AreEqual("+", result[7]);
            Assert.AreEqual("(", result[8]);
            Assert.AreEqual("2", result[9]);
            Assert.AreEqual("x", result[10]);
            Assert.AreEqual("(", result[11]);
            Assert.AreEqual("d8", result[12]);
            Assert.AreEqual("-", result[13]);
            Assert.AreEqual("2", result[14]);
            Assert.AreEqual(")", result[15]);
            Assert.AreEqual(")", result[16]);
        }
    }
}
