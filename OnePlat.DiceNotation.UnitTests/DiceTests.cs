using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnePlat.DiceNotation.DiceTerms;
using OnePlat.DiceNotation.DieRoller;
using OnePlat.DiceNotation.UnitTests.Helpers;
using System;

namespace OnePlat.DiceNotation.UnitTests
{
    /// <summary>
    /// Summary description for DiceTests
    /// </summary>
    [TestClass]
    public class DiceTests
    {
        private IDieRoller roller = new RandomDieRoller();

        public DiceTests()
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
        public void Dice_ConstructorTest()
        {
            // setup test

            // run test
            IDice dice = new Dice();

            // validate results
            Assert.IsNotNull(dice);
            Assert.IsInstanceOfType(dice, typeof(IDice));
            Assert.IsInstanceOfType(dice, typeof(Dice));
            Assert.IsTrue(string.IsNullOrEmpty(dice.ToString()));
            Assert.IsTrue(dice.HasBoundedResult);
        }

        [TestMethod]
        public void Dice_ConstantTest()
        {
            // setup test
            IDice dice = new Dice();

            // run test
            IDice result = dice.Constant(5);

            // validate results
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IDice));
            Assert.IsInstanceOfType(result, typeof(Dice));
            Assert.AreEqual("5", dice.ToString());
        }

        [TestMethod]
        public void Dice_DiceSidesTest()
        {
            // setup test
            IDice dice = new Dice();

            // run test
            IDice result = dice.Dice(8);

            // validate results
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IDice));
            Assert.IsInstanceOfType(result, typeof(Dice));
            Assert.AreEqual("1d8", dice.ToString());
        }

        [TestMethod]
        public void Dice_DiceChainingTest()
        {
            // setup test
            IDice dice = new Dice();

            // run test
            IDice result = dice.Dice(6, 4, choose: 3).Dice(8).Constant(5);

            // validate results
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IDice));
            Assert.IsInstanceOfType(result, typeof(Dice));
            Assert.AreEqual("4d6k3+1d8+5", dice.ToString());
        }

        [TestMethod]
        public void Dice_RollConstantTest()
        {
            // setup test
            IDice dice = new Dice().Constant(3);

            // run test
            DiceResult result = dice.Roll(roller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("OnePlat.DiceNotation.DieRoller.RandomDieRoller", result.DieRollerUsed);
            Assert.AreEqual(3, result.Value);
            Assert.AreEqual(1, result.Results.Count);
            foreach(TermResult r in result.Results)
            {
                Assert.AreEqual(3, r.Value);
            }
            Assert.AreEqual("3", dice.ToString());
        }

        [TestMethod]
        public void Dice_RollSingleDieTest()
        {
            // setup test
            IDice dice = new Dice();
            dice.Dice(20);

            // run test
            DiceResult result = dice.Roll(roller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("OnePlat.DiceNotation.DieRoller.RandomDieRoller", result.DieRollerUsed);
            AssertHelpers.IsWithinRangeInclusive(1, 20, result.Value);
            Assert.AreEqual(1, result.Results.Count);
            int sum = 0;
            foreach (TermResult r in result.Results)
            {
                AssertHelpers.IsWithinRangeInclusive(1, 20, r.Value);
                sum += r.Value;
            }
            Assert.AreEqual(sum, result.Value);
            Assert.AreEqual("1d20", dice.ToString());
        }

        [TestMethod]
        public void Dice_RollMultipleDiceTest()
        {
            // setup test
            IDice dice = new Dice();
            dice.Dice(6, 3);

            // run test
            DiceResult result = dice.Roll(roller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("OnePlat.DiceNotation.DieRoller.RandomDieRoller", result.DieRollerUsed);
            AssertHelpers.IsWithinRangeInclusive(3, 18, result.Value);
            Assert.AreEqual(3, result.Results.Count);
            int sum = 0;
            foreach (TermResult r in result.Results)
            {
                AssertHelpers.IsWithinRangeInclusive(1, 6, r.Value);
                sum += r.Value;
            }
            Assert.AreEqual(sum, result.Value);
            Assert.AreEqual("3d6", dice.ToString());
        }

        [TestMethod]
        public void Dice_RollScalarMultiplierDiceTest()
        {
            // setup test
            IDice dice = new Dice();
            dice.Dice(8, 2, 10);

            // run test
            DiceResult result = dice.Roll(roller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("OnePlat.DiceNotation.DieRoller.RandomDieRoller", result.DieRollerUsed);
            AssertHelpers.IsWithinRangeInclusive(20, 160, result.Value);
            Assert.AreEqual(2, result.Results.Count);
            int sum = 0;
            foreach (TermResult r in result.Results)
            {
                AssertHelpers.IsWithinRangeInclusive(1, 8, r.Value);
                sum += (int)(r.Value * r.Scalar);
            }
            Assert.AreEqual(sum, result.Value);
            Assert.AreEqual("2d8x10", dice.ToString());
        }

        [TestMethod]
        public void Dice_RollChooseDiceTest()
        {
            // setup test
            IDice dice = new Dice();
            dice.Dice(6, 4, choose: 3);

            // run test
            DiceResult result = dice.Roll(roller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("OnePlat.DiceNotation.DieRoller.RandomDieRoller", result.DieRollerUsed);
            AssertHelpers.IsWithinRangeInclusive(3, 18, result.Value);
            Assert.AreEqual(3, result.Results.Count);
            int sum = 0;
            foreach (TermResult r in result.Results)
            {
                AssertHelpers.IsWithinRangeInclusive(1, 6, r.Value);
                sum += r.Value;
            }
            Assert.AreEqual(sum, result.Value);
            Assert.AreEqual("4d6k3", dice.ToString());
        }

        [TestMethod]
        public void Dice_RollChainedDiceTest()
        {
            // setup test
            IDice dice = new Dice();
            dice.Dice(6, 4, choose: 3).Dice(8).Constant(5);

            // run test
            DiceResult result = dice.Roll(new ConstantDieRoller(1));

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("OnePlat.DiceNotation.DieRoller.ConstantDieRoller", result.DieRollerUsed);
            Assert.AreEqual(9, result.Value);
            Assert.AreEqual(5, result.Results.Count);
            int sum = 0;
            foreach (TermResult r in result.Results)
            {
                sum += r.Value;
            }
            Assert.AreEqual(sum, result.Value);
            Assert.AreEqual("4d6k3+1d8+5", dice.ToString());
        }

        [TestMethod]
        public void Dice_RollNullRollerTest()
        {
            // setup test
            IDice dice = new Dice();
            dice.Dice(6, 4, 3).Dice(8).Constant(5);

            // run test
            Assert.ThrowsException<ArgumentNullException>(() => dice.Roll(null));

            // validate results
        }

        [TestMethod]
        public void Dice_ParseMultipleDiceTest()
        {
            // setup test
            IDice dice = new Dice();

            // run test
            dice = dice.Parse("3d6+2");
            DiceResult result = dice.Roll(roller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("OnePlat.DiceNotation.DieRoller.RandomDieRoller", result.DieRollerUsed);
            AssertHelpers.IsWithinRangeInclusive(5, 20, result.Value);
            Assert.AreEqual(4, result.Results.Count);
            int sum = 0;
            foreach (TermResult r in result.Results)
            {
                AssertHelpers.IsWithinRangeInclusive(1, 6, r.Value);
                sum += r.Value;
            }
            Assert.AreEqual(sum, result.Value);
            Assert.AreEqual("3d6+2", dice.ToString());
        }

        [TestMethod]
        public void Dice_ParseChainedDiceTest()
        {
            // setup test
            IDice dice = new Dice();
            
            // run test
            dice.Parse("4d6k3 + 1d8 + 5");
            DiceResult result = dice.Roll(new ConstantDieRoller(1));

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("OnePlat.DiceNotation.DieRoller.ConstantDieRoller", result.DieRollerUsed);
            Assert.AreEqual(9, result.Value);
            Assert.AreEqual(5, result.Results.Count);
            int sum = 0;
            foreach (TermResult r in result.Results)
            {
                sum += r.Value;
            }
            Assert.AreEqual(sum, result.Value);
            Assert.AreEqual("4d6k3+1d8+5", dice.ToString());
        }

        [TestMethod]
        public void Dice_ParseMultipleDiceWithPreexistingTermsTest()
        {
            // setup test
            IDice dice = new Dice();
            dice.Constant(2);

            // run test
            dice = dice.Parse("3d6");
            DiceResult result = dice.Roll(roller);

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("OnePlat.DiceNotation.DieRoller.RandomDieRoller", result.DieRollerUsed);
            AssertHelpers.IsWithinRangeInclusive(5, 20, result.Value);
            Assert.AreEqual(4, result.Results.Count);
            int sum = 0;
            foreach (TermResult r in result.Results)
            {
                AssertHelpers.IsWithinRangeInclusive(1, 6, r.Value);
                sum += r.Value;
            }
            Assert.AreEqual(sum, result.Value);
            Assert.AreEqual("2+3d6", dice.ToString());
        }

        [TestMethod]
        public void Dice_ParseMultipleDiceWithPostTermsTest()
        {
            // setup test
            IDice dice = new Dice();

            // run test
            dice = dice.Parse("4d6k3").Dice(8).Constant(5);
            DiceResult result = dice.Roll(new ConstantDieRoller(1));

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("OnePlat.DiceNotation.DieRoller.ConstantDieRoller", result.DieRollerUsed);
            Assert.AreEqual(9, result.Value);
            Assert.AreEqual(5, result.Results.Count);
            int sum = 0;
            foreach (TermResult r in result.Results)
            {
                sum += r.Value;
            }
            Assert.AreEqual(sum, result.Value);
            Assert.AreEqual("4d6k3+1d8+5", dice.ToString());
        }

        [TestMethod]
        public void Dice_ConcatOtherDiceTest()
        {
            // setup test
            IDice dice = new Dice().Parse("4d6k3");
            IDice other = new Dice().Constant(5).Dice(8);

            // run test
            dice = dice.Concat(other);
            DiceResult result = dice.Roll(new ConstantDieRoller(1));

            // validate results
            Assert.IsNotNull(result);
            Assert.AreEqual("OnePlat.DiceNotation.DieRoller.ConstantDieRoller", result.DieRollerUsed);
            Assert.AreEqual(9, result.Value);
            Assert.AreEqual(5, result.Results.Count);
            int sum = 0;
            foreach (TermResult r in result.Results)
            {
                sum += r.Value;
            }
            Assert.AreEqual(sum, result.Value);
            Assert.AreEqual("4d6k3+5+1d8", dice.ToString());
        }

        [TestMethod]
        public void Dice_RollWithNegativeResultUnboundedTest()
        {
            // setup test
            DiceParser parser = new DiceParser();
            IDice dice = parser.Parse("d12-3");

            // run test
            dice.HasBoundedResult = false;
            DiceResult result = dice.Roll(new ConstantDieRoller(1));

            // validate results
            Assert.IsNotNull(dice);
            Assert.AreEqual("1d12-3", dice.ToString());
            Assert.AreEqual(2, result.Results.Count);
            Assert.AreEqual(-2, result.Value);
        }

        [TestMethod]
        public void Dice_ConcatNullOtherDiceTest()
        {
            // setup test
            IDice dice = new Dice().Parse("4d6k3");

            // run test
            Assert.ThrowsException<ArgumentNullException>(() => dice = dice.Concat(null));

            // validate results
        }
    }
}
