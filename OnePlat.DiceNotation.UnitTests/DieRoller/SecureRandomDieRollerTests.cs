﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnePlat.DiceNotation.DieRoller;
using OnePlat.DiceNotation.UnitTests.Helpers;
using System;

namespace OnePlat.DiceNotation.UnitTests.DieRoller
{
    /// <summary>
    /// Summary description for SecureRandomDieRoller
    /// </summary>
    [TestClass]
    public class SecureRandomDieRollerTests
    {
        public SecureRandomDieRollerTests()
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
        public void SecureRandomDieRoller_DefaultConstructorTest()
        {
            // setup test

            // run test
            IDieRoller die = new SecureRandomDieRoller();

            // validate results
            Assert.IsNotNull(die);
            Assert.IsInstanceOfType(die, typeof(IDieRoller));
            Assert.IsInstanceOfType(die, typeof(SecureRandomDieRoller));
        }

        [TestMethod]
        public void SecureRandomDieRoller_Rolld20Test()
        {
            // setup test
            IDieRoller die = new SecureRandomDieRoller();

            // run test
            int result = die.Roll(20);

            // validate results
            AssertHelpers.IsWithinRangeInclusive(1, 20, result);
        }

        [TestMethod]
        public void SecureRandomDieRoller_Rolld4Test()
        {
            // setup test
            IDieRoller die = new SecureRandomDieRoller();

            // run test
            int result = die.Roll(4);

            // validate results
            AssertHelpers.IsWithinRangeInclusive(1, 4, result);
        }

        [TestMethod]
        public void SecureRandomDieRoller_Rolld6Test()
        {
            // setup test
            IDieRoller die = new SecureRandomDieRoller();

            // run test
            int result = die.Roll(6);

            // validate results
            AssertHelpers.IsWithinRangeInclusive(1, 6, result);
        }

        [TestMethod]
        public void SecureRandomDieRoller_Rolld8Test()
        {
            // setup test
            IDieRoller die = new SecureRandomDieRoller();

            // run test
            int result = die.Roll(8);

            // validate results
            AssertHelpers.IsWithinRangeInclusive(1, 8, result);
        }

        [TestMethod]
        public void SecureRandomDieRoller_Rolld12Test()
        {
            // setup test
            IDieRoller die = new SecureRandomDieRoller();

            // run test
            int result = die.Roll(12);

            // validate results
            AssertHelpers.IsWithinRangeInclusive(1, 12, result);
        }

        [TestMethod]
        public void SecureRandomDieRoller_Rolld100Test()
        {
            // setup test
            IDieRoller die = new SecureRandomDieRoller();

            // run test
            int result = die.Roll(100);

            // validate results
            AssertHelpers.IsWithinRangeInclusive(1, 100, result);
        }

        [TestMethod]
        public void SecureRandomDieRoller_Rolld7Test()
        {
            // setup test
            IDieRoller die = new SecureRandomDieRoller();

            // run test
            int result = die.Roll(7);

            // validate results
            AssertHelpers.IsWithinRangeInclusive(1, 7, result);
        }

        [TestMethod]
        public void SecureRandomDieRoller_RollFudgeTest()
        {
            // setup test
            IDieRoller die = new SecureRandomDieRoller();

            // run test
            int result = die.Roll(3, -2);

            // validate results
            AssertHelpers.IsWithinRangeInclusive(-1, 1, result);
        }

        [TestMethod]
        public void SecureRandomDieRoller_RollMultipleFudgeTest()
        {
            // setup test
            IDieRoller die = new SecureRandomDieRoller();

            // run test
            for (int i = 0; i < 100; i++)
            {
                int result = die.Roll(3, -2);

                // validate results
                AssertHelpers.IsWithinRangeInclusive(-1, 1, result);
            }
        }

        [TestMethod]
        public void SecureRandomDieRoller_RollThousanD6Test()
        {
            // setup test
            IDieRoller die = new SecureRandomDieRoller();

            // run test
            for (int i = 0; i < 1000; i++)
            {
                int result = die.Roll(6);

                // validate results
                AssertHelpers.IsWithinRangeInclusive(1, 6, result);
            }
        }

        [TestMethod]
        public void SecureRandomDieRoller_RollErrorTest()
        {
            // setup test
            IDieRoller die = new SecureRandomDieRoller();

            // run test
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => die.Roll(0));

            // validate results
        }
    }
}
