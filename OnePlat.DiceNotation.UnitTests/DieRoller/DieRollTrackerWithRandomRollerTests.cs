using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnePlat.DiceNotation.DieRoller;
using OnePlat.DiceNotation.UnitTests.Helpers;

namespace OnePlat.DiceNotation.UnitTests.DieRoller
{
    /// <summary>
    /// Summary description for DieRollTrackerWithRandomRollerTests
    /// </summary>
    [TestClass]
    public class DieRollTrackerWithRandomRollerTests
    {
        private IDieRollTracker tracker = new DieRollTracker();
        private IDieRoller roller;

        public DieRollTrackerWithRandomRollerTests()
        {
            this.roller = new RandomDieRoller(this.tracker);
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
        public void DieRollTrackerWithRandomRoller_SingleDieSidesTest()
        {
            // setup test
            this.roller.Roll(12);
            this.roller.Roll(12);
            this.roller.Roll(12);
            this.roller.Roll(12);
            this.roller.Roll(12);

            // run test
            IList<DieTrackingData> data = this.tracker.GetTrackingData();

            // validate results
            Assert.AreEqual(5, data.Count);
            foreach (DieTrackingData e in data)
            {
                Assert.AreEqual("RandomDieRoller", e.RollerType);
                Assert.AreEqual("12", e.DieSides);
                AssertHelpers.IsWithinRangeInclusive(1, 12, e.Result);
            }
        }

        [TestMethod]
        public void DieRollTrackerWithRandomRoller_MultipleDieSidesTest()
        {
            // setup test
            this.roller.Roll(12);
            this.roller.Roll(12);
            this.roller.Roll(12);
            this.roller.Roll(12);
            this.roller.Roll(8);
            this.roller.Roll(8);
            this.roller.Roll(8);
            this.roller.Roll(20);
            this.roller.Roll(20);
            this.roller.Roll(20);
            this.roller.Roll(20);
            this.roller.Roll(20);
            this.roller.Roll(20);
            this.roller.Roll(20);
            this.roller.Roll(20);
            this.roller.Roll(20);
            this.roller.Roll(20);

            // run test
            IList<DieTrackingData> data1 = this.tracker.GetTrackingData(dieSides: "12");
            IList<DieTrackingData> data2 = this.tracker.GetTrackingData(dieSides: "8");
            IList<DieTrackingData> data3 = this.tracker.GetTrackingData(dieSides: "20");

            // validate results
            Assert.AreEqual(17, data1.Count + data2.Count + data3.Count);
            Assert.AreEqual(4, data1.Count);
            foreach (DieTrackingData e in data1)
            {
                Assert.AreEqual("12", e.DieSides);
                AssertHelpers.IsWithinRangeInclusive(1, 12, e.Result);
            }
            Assert.AreEqual(3, data2.Count);
            foreach (DieTrackingData e in data2)
            {
                Assert.AreEqual("8", e.DieSides);
                AssertHelpers.IsWithinRangeInclusive(1, 8, e.Result);
            }
            Assert.AreEqual(10, data3.Count);
            foreach (DieTrackingData e in data3)
            {
                Assert.AreEqual("20", e.DieSides);
                AssertHelpers.IsWithinRangeInclusive(1, 20, e.Result);
            }
        }
    }
}
