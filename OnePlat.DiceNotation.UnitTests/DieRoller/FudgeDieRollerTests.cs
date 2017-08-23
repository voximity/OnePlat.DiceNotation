using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnePlat.DiceNotation.DieRoller;
using OnePlat.DiceNotation.UnitTests.Helpers;

namespace OnePlat.DiceNotation.UnitTests.DieRoller
{
    /// <summary>
    /// Summary description for FudgeDieRollerTests
    /// </summary>
    [TestClass]
    public class FudgeDieRollerTests
    {
        public FudgeDieRollerTests()
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
        public void FudgeDieRoller_ConstructorTest()
        {
            // setup test

            // run test
            IDieRoller die = new FudgeDieRoller();

            // validate results
            Assert.IsNotNull(die);
            Assert.IsInstanceOfType(die, typeof(IDieRoller));
            Assert.IsInstanceOfType(die, typeof(FudgeDieRoller));
        }

        [TestMethod]
        public void RandomDieRoller_Rolld6Test()
        {
            // setup test
            IDieRoller die = new FudgeDieRoller();

            // run test
            int result = die.Roll(6);

            // validate results
            AssertHelpers.IsWithinRangeInclusive(-1, 1, result);
        }

        [TestMethod]
        public void RandomDieRoller_Rolld100TimesTest()
        {
            // setup test
            IDieRoller die = new FudgeDieRoller();

            for (int i = 0; i < 100; i++)
            {
                // run test
                int result = die.Roll(6);

                // validate results
                AssertHelpers.IsWithinRangeInclusive(-1, 1, result);
            }
        }

        [TestMethod]
        public void RandomDieRoller_RollOtherNumberSidesTest()
        {
            // setup test
            IDieRoller die = new FudgeDieRoller();

            // run test
            int result = die.Roll(20);

            // validate results
            AssertHelpers.IsWithinRangeInclusive(-1, 1, result);
        }
    }
}
