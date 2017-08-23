using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnePlat.DiceNotation.DiceTerms;
using OnePlat.DiceNotation.DieRoller;
using System.Linq;
using OnePlat.DiceNotation.UnitTests.Helpers;

namespace OnePlat.DiceNotation.UnitTests.DiceTerms
{
    /// <summary>
    /// Summary description for FudgeDiceTermTests
    /// </summary>
    [TestClass]
    public class FudgeDiceTermTests
    {
        private IDieRoller roller = new FudgeDieRoller();

        public FudgeDiceTermTests()
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
        public void FudgeDiceTerm_ConstructorTest()
        {
            // setup test

            // run test
            IExpressionTerm term = new FudgeDiceTerm(3);

            // validate results
            Assert.IsNotNull(term);
            Assert.IsInstanceOfType(term, typeof(IExpressionTerm));
            Assert.IsInstanceOfType(term, typeof(FudgeDiceTerm));
        }

        [TestMethod]
        public void FudgeDiceTerm_ConstructorInvalidNumDiceTest()
        {
            // setup test

            // run test
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new FudgeDiceTerm(0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new FudgeDiceTerm(-5));

            // validate results
        }

        [TestMethod]
        public void FudgeDiceTerm_ConstructorInvalidChooseTest()
        {
            // setup test

            // run test
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new FudgeDiceTerm(3, choose: 0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new FudgeDiceTerm(3, choose: -1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new FudgeDiceTerm(3, choose: 4));

            // validate results
        }

        [TestMethod]
        public void DiceTerm_CalculateResultsTest()
        {
            // setup test
            IExpressionTerm term = new FudgeDiceTerm(1);

            // run test
            IReadOnlyList<TermResult> results = term.CalculateResults(roller);

            // validate results
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);
            TermResult r = results.FirstOrDefault();
            Assert.IsNotNull(r);
            Assert.AreEqual(1, r.Scalar);
            AssertHelpers.IsWithinRangeInclusive(-1, 1, r.Value);
            Assert.AreEqual("FudgeDiceTerm.dF", r.Type);
        }

        [TestMethod]
        public void DiceTerm_CalculateResultsMultipleDiceTest()
        {
            // setup test
            IExpressionTerm term = new FudgeDiceTerm(3);

            // run test
            IReadOnlyList<TermResult> results = term.CalculateResults(roller);

            // validate results
            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count);
            foreach (TermResult r in results)
            {
                Assert.IsNotNull(r);
                Assert.AreEqual(1, r.Scalar);
                AssertHelpers.IsWithinRangeInclusive(-1, 1, r.Value);
                Assert.AreEqual("FudgeDiceTerm.dF", r.Type);
            }
        }

        [TestMethod]
        public void DiceTerm_CalculateResultsChooseDiceTest()
        {
            // setup test
            IExpressionTerm term = new FudgeDiceTerm(5, 3);

            // run test
            IReadOnlyList<TermResult> results = term.CalculateResults(roller);

            // validate results
            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count);
            foreach (TermResult r in results)
            {
                Assert.IsNotNull(r);
                Assert.AreEqual(1, r.Scalar);
                AssertHelpers.IsWithinRangeInclusive(-1, 1, r.Value);
                Assert.AreEqual("FudgeDiceTerm.dF", r.Type);
            }
        }

        [TestMethod]
        public void FudgeDiceTerm_CalculateResultsNullDieRollerTest()
        {
            // setup test
            IExpressionTerm term = new FudgeDiceTerm(1);

            // run test
            Assert.ThrowsException<ArgumentNullException>(() => term.CalculateResults(null));

            // validate results
        }

        [TestMethod]
        public void FudgeDiceTerm_ToStringTest()
        {
            // setup test
            IExpressionTerm term = new FudgeDiceTerm(2);

            // run test
            string result = term.ToString();

            // validate results
            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.AreEqual("2f", result);
        }

        [TestMethod]
        public void FudgeDiceTerm_ToStringChooseTest()
        {
            // setup test
            IExpressionTerm term = new FudgeDiceTerm(5, 3);

            // run test
            string result = term.ToString();

            // validate results
            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.AreEqual("5fk3", result);
        }
    }
}
