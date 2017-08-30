using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnePlat.DiceNotation.DieRoller;
using OnePlat.DiceNotation.UnitTests.Helpers;
using System;
using System.Collections.Generic;

namespace OnePlat.DiceNotation.UnitTests.DieRoller
{
    /// <summary>
    /// Summary description for DieRollTrackerTests
    /// </summary>
    [TestClass]
    public class DieRollTrackerTests
    {
        public DieRollTrackerTests()
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
        public void DieRollTracker_ConstructorTest()
        {
            // setup test

            // run test
            IDieRollTracker t = new DieRollTracker();

            // validate results
            Assert.IsNotNull(t);
            Assert.IsInstanceOfType(t, typeof(IDieRollTracker));
            Assert.IsInstanceOfType(t, typeof(DieRollTracker));
        }

        [TestMethod]
        public void DieRollTracker_AddDieRollTest()
        {
            // setup test
            IDieRollTracker t = new DieRollTracker();

            // run test
            t.AddDieRoll(6, 4, typeof(RandomDieRoller));
            IList<DieTrackingData> d = t.GetTrackingData();

            // validate results
            Assert.IsNotNull(d);
            Assert.AreEqual(1, d.Count);
            Assert.AreEqual("RandomDieRoller", d[0].RollerType);
            Assert.AreEqual("6", d[0].DieSides);
            Assert.AreEqual(4, d[0].Result);
            Assert.IsInstanceOfType(d[0].Id, typeof(Guid));
            Assert.IsInstanceOfType(d[0].Timpstamp, typeof(DateTime));
        }

        [TestMethod]
        public void DieRollTracker_AddMultipleDieRollTest()
        {
            // setup test
            IDieRollTracker t = new DieRollTracker();

            // run test
            t.AddDieRoll(6, 4, typeof(RandomDieRoller));
            t.AddDieRoll(6, 3, typeof(RandomDieRoller));
            t.AddDieRoll(6, 1, typeof(RandomDieRoller));
            t.AddDieRoll(6, 6, typeof(RandomDieRoller));
            t.AddDieRoll(6, 2, typeof(RandomDieRoller));
            t.AddDieRoll(6, 4, typeof(RandomDieRoller));
            IList<DieTrackingData> d = t.GetTrackingData();

            // validate results
            Assert.IsNotNull(d);
            Assert.AreEqual(6, d.Count);
            foreach (DieTrackingData e in d)
            {
                Assert.AreEqual("RandomDieRoller", e.RollerType);
                Assert.AreEqual("6", e.DieSides);
                AssertHelpers.IsWithinRangeInclusive(1, 6, e.Result);
            }
        }

        [TestMethod]
        public void DieRollTracker_AddMultipleDieRollDieSidesTest()
        {
            // setup test
            IDieRollTracker t = new DieRollTracker();

            // run test
            t.AddDieRoll(6, 4, typeof(RandomDieRoller));
            t.AddDieRoll(6, 3, typeof(RandomDieRoller));
            t.AddDieRoll(6, 1, typeof(RandomDieRoller));
            t.AddDieRoll(6, 6, typeof(RandomDieRoller));
            t.AddDieRoll(8, 2, typeof(RandomDieRoller));
            t.AddDieRoll(8, 4, typeof(RandomDieRoller));
            IList<DieTrackingData> d = t.GetTrackingData();

            // validate results
            Assert.IsNotNull(d);
            Assert.AreEqual(6, d.Count);
            int i;
            for (i = 0; i < 4; i++)
            {
                DieTrackingData e = d[i];
                Assert.AreEqual("RandomDieRoller", e.RollerType);
                Assert.AreEqual("6", e.DieSides);
                AssertHelpers.IsWithinRangeInclusive(1, 6, e.Result);
            }

            for (int x = i; x < 5; x++)
            {
                DieTrackingData e = d[x];
                Assert.AreEqual("RandomDieRoller", e.RollerType);
                Assert.AreEqual("8", e.DieSides);
                AssertHelpers.IsWithinRangeInclusive(1, 8, e.Result);
            }
        }

        [TestMethod]
        public void DieRollTracker_AddMultipleDieRollDieTypesTest()
        {
            // setup test
            IDieRollTracker t = new DieRollTracker();

            // run test
            t.AddDieRoll(6, 4, typeof(RandomDieRoller));
            t.AddDieRoll(6, 3, typeof(RandomDieRoller));
            t.AddDieRoll(6, 5, typeof(RandomDieRoller));
            t.AddDieRoll(6, 6, typeof(RandomDieRoller));
            t.AddDieRoll(6, 2, typeof(ConstantDieRoller));
            t.AddDieRoll(6, 2, typeof(ConstantDieRoller));
            IList<DieTrackingData> d = t.GetTrackingData();

            // validate results
            Assert.IsNotNull(d);
            Assert.AreEqual(6, d.Count);
            int i;
            for (i = 0; i < 2; i++)
            {
                DieTrackingData e = d[i];
                Assert.AreEqual("ConstantDieRoller", e.RollerType);
                Assert.AreEqual("6", e.DieSides);
                AssertHelpers.IsWithinRangeInclusive(1, 6, e.Result);
            }

            for (int x = i; x < 6; x++)
            {
                DieTrackingData e = d[x];
                Assert.AreEqual("RandomDieRoller", e.RollerType);
                Assert.AreEqual("6", e.DieSides);
                AssertHelpers.IsWithinRangeInclusive(1, 6, e.Result);
            }
        }

        [TestMethod]
        public void DieRollTracker_ClearTest()
        {
            // setup test
            IDieRollTracker t = new DieRollTracker();
            t.AddDieRoll(6, 4, typeof(RandomDieRoller));
            t.AddDieRoll(6, 3, typeof(RandomDieRoller));
            t.AddDieRoll(6, 1, typeof(RandomDieRoller));
            t.AddDieRoll(6, 6, typeof(RandomDieRoller));
            t.AddDieRoll(6, 2, typeof(RandomDieRoller));
            t.AddDieRoll(6, 4, typeof(RandomDieRoller));
            Assert.AreEqual(6, t.GetTrackingData().Count);

            // run test
            t.Clear();

            // validate results
            Assert.AreEqual(0, t.GetTrackingData().Count);
        }

        [TestMethod]
        public void DieRollTracker_GetTrackingDataAllTest()
        {
            // setup test
            IDieRollTracker t = new DieRollTracker();
            this.SetupTrackingSampleData(t);

            // run test
            IList<DieTrackingData> data = t.GetTrackingData();

            // validate results
            Assert.AreEqual(27, data.Count);
        }

        [TestMethod]
        public void DieRollTracker_GetTrackingDataFilterDieTypeTest()
        {
            // setup test
            IDieRollTracker t = new DieRollTracker();
            this.SetupTrackingSampleData(t);

            // run test
            IList<DieTrackingData> data = t.GetTrackingData("RandomDieRoller");

            // validate results
            Assert.AreEqual(19, data.Count);
            foreach(DieTrackingData e in data)
            {
                Assert.AreEqual("RandomDieRoller", e.RollerType);
            }
        }

        [TestMethod]
        public void DieRollTracker_GetTrackingDataFilterDieSidesTest()
        {
            // setup test
            IDieRollTracker t = new DieRollTracker();
            this.SetupTrackingSampleData(t);

            // run test
            IList<DieTrackingData> data = t.GetTrackingData(dieSides: "10");

            // validate results
            Assert.AreEqual(4, data.Count);
            foreach (DieTrackingData e in data)
            {
                Assert.AreEqual("10", e.DieSides);
            }
        }

        [TestMethod]
        public void DieRollTracker_GetTrackingDataFilterDieTypeAndSidesTest()
        {
            // setup test
            IDieRollTracker t = new DieRollTracker();
            this.SetupTrackingSampleData(t);

            // run test
            IList<DieTrackingData> data = t.GetTrackingData("RandomDieRoller", "20");

            // validate results
            Assert.AreEqual(14, data.Count);
            foreach (DieTrackingData e in data)
            {
                Assert.AreEqual("RandomDieRoller", e.RollerType);
                Assert.AreEqual("20", e.DieSides);
            }
        }

        private void SetupTrackingSampleData(IDieRollTracker tracker)
        {
            tracker.AddDieRoll(6, 4, typeof(RandomDieRoller));
            tracker.AddDieRoll(6, 3, typeof(RandomDieRoller));
            tracker.AddDieRoll(6, 1, typeof(RandomDieRoller));
            tracker.AddDieRoll(6, 6, typeof(RandomDieRoller));
            tracker.AddDieRoll(8, 2, typeof(ConstantDieRoller));
            tracker.AddDieRoll(8, 4, typeof(ConstantDieRoller));
            tracker.AddDieRoll(10, 2, typeof(ConstantDieRoller));
            tracker.AddDieRoll(10, 8, typeof(ConstantDieRoller));
            tracker.AddDieRoll(10, 9, typeof(ConstantDieRoller));
            tracker.AddDieRoll(10, 4, typeof(RandomDieRoller));
            tracker.AddDieRoll(20, 5, typeof(ConstantDieRoller));
            tracker.AddDieRoll(20, 18, typeof(ConstantDieRoller));
            tracker.AddDieRoll(20, 11, typeof(ConstantDieRoller));
            tracker.AddDieRoll(20, 5, typeof(RandomDieRoller));
            tracker.AddDieRoll(20, 5, typeof(RandomDieRoller));
            tracker.AddDieRoll(20, 17, typeof(RandomDieRoller));
            tracker.AddDieRoll(20, 9, typeof(RandomDieRoller));
            tracker.AddDieRoll(20, 12, typeof(RandomDieRoller));
            tracker.AddDieRoll(20, 20, typeof(RandomDieRoller));
            tracker.AddDieRoll(20, 8, typeof(RandomDieRoller));
            tracker.AddDieRoll(20, 11, typeof(RandomDieRoller));
            tracker.AddDieRoll(20, 9, typeof(RandomDieRoller));
            tracker.AddDieRoll(20, 12, typeof(RandomDieRoller));
            tracker.AddDieRoll(20, 20, typeof(RandomDieRoller));
            tracker.AddDieRoll(20, 14, typeof(RandomDieRoller));
            tracker.AddDieRoll(20, 13, typeof(RandomDieRoller));
            tracker.AddDieRoll(20, 9, typeof(RandomDieRoller));
        }
    }
}
