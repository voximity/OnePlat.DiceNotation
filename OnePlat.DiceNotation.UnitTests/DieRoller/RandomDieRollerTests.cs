using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnePlat.DiceNotation.DieRoller;
using System;

namespace OnePlat.DiceNotation.UnitTests.DieRoller
{
    /// <summary>
    /// Summary description for RandomDieRollerTests
    /// </summary>
    [TestClass]
    public class RandomDieRollerTests
    {
        public RandomDieRollerTests()
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
        public void RandomDieRoller_DefaultConstructorTest()
        {
            // setup test

            // run test
            IDieRoller die = new RandomDieRoller();

            // validate results
            Assert.IsNotNull(die);
            Assert.IsInstanceOfType(die, typeof(IDieRoller));
            Assert.IsInstanceOfType(die, typeof(RandomDieRoller));
        }

        [TestMethod]
        public void RandomDieRoller_ConstructorRandomGeneratorTest()
        {
            // setup test
            Random rand = new Random(42);

            // run test
            IDieRoller die = new RandomDieRoller();

            // validate results
            Assert.IsNotNull(die);
            Assert.IsInstanceOfType(die, typeof(IDieRoller));
            Assert.IsInstanceOfType(die, typeof(RandomDieRoller));
        }

        [TestMethod]
        public void RandomDieRoller_Rolld20Test()
        {
            // setup test
            IDieRoller die = new RandomDieRoller();

            // run test
            int result = die.Roll(20);

            // validate results
            Assert.IsTrue(result >= 1 && result <= 20);
        }

        [TestMethod]
        public void RandomDieRoller_Rolld4Test()
        {
            // setup test
            IDieRoller die = new RandomDieRoller();

            // run test
            int result = die.Roll(4);

            // validate results
            Assert.IsTrue(result >= 1 && result <= 4);
        }

        [TestMethod]
        public void RandomDieRoller_Rolld6Test()
        {
            // setup test
            IDieRoller die = new RandomDieRoller();

            // run test
            int result = die.Roll(6);

            // validate results
            Assert.IsTrue(result >= 1 && result <= 6);
        }

        [TestMethod]
        public void RandomDieRoller_Rolld8Test()
        {
            // setup test
            IDieRoller die = new RandomDieRoller();

            // run test
            int result = die.Roll(8);

            // validate results
            Assert.IsTrue(result >= 1 && result <= 8);
        }

        [TestMethod]
        public void RandomDieRoller_Rolld12Test()
        {
            // setup test
            IDieRoller die = new RandomDieRoller();

            // run test
            int result = die.Roll(12);

            // validate results
            Assert.IsTrue(result >= 1 && result <= 12);
        }

        [TestMethod]
        public void RandomDieRoller_Rolld100Test()
        {
            // setup test
            IDieRoller die = new RandomDieRoller();

            // run test
            int result = die.Roll(100);

            // validate results
            Assert.IsTrue(result >= 1 && result <= 100);
        }

        [TestMethod]
        public void RandomDieRoller_Rolld7Test()
        {
            // setup test
            IDieRoller die = new RandomDieRoller();

            // run test
            int result = die.Roll(7);

            // validate results
            Assert.IsTrue(result >= 1 && result <= 7);
        }
    }
}
