// <copyright file="MainViewTests.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnePlat.DiceNotation.CommandLine.Core;

namespace OnePlat.DiceNotation.CommandLine.UnitTests
{
    [TestClass]
    public class MainViewTests
    {
        [TestMethod]
        public void MainView_ConstructorTest()
        {
            // setup test

            // run test
            MainView v = new MainView();

            // validate results
            Assert.IsNotNull(v);
            Assert.IsInstanceOfType(v, typeof(MainView));
            Assert.IsInstanceOfType(v, typeof(IView));
            Assert.IsNotNull(v.DataContext);
            Assert.IsInstanceOfType(v.DataContext, typeof(MainViewModel));
        }

        [TestMethod]
        public void MainView_UpdateTest()
        {
            // setup test
            MainView v = new MainView();
            ((MainViewModel)v.DataContext).DisplayText = "Testing...123";

            // run test
            v.Update();

            // validate results
        }
    }
}
