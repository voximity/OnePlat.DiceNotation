// <copyright file="MainViewModelTests.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnePlat.DiceNotation.CommandLine.Core;
using OnePlat.DiceNotation.DieRoller;

namespace OnePlat.DiceNotation.CommandLine.UnitTests
{
    [TestClass]
    public class MainViewModelTests
    {
        [TestMethod]
        public void MainViewModel_Constructor()
        {
            // setup test

            // run test
            MainViewModel vm = new MainViewModel();

            // validate results
            Assert.IsNotNull(vm);
            Assert.IsInstanceOfType(vm, typeof(MainViewModel));
            Assert.IsInstanceOfType(vm, typeof(IViewModel));
            Assert.IsNotNull(vm.ConstantRollerCommand);
            Assert.IsNotNull(vm.HelpCommand);
            Assert.IsNotNull(vm.RollDiceCommand);
            Assert.IsNotNull(vm.SetVerboseCommand);
            Assert.IsNotNull(vm.SetUnboundResultCommand);
            Assert.IsNotNull(vm.SetDefaultNumberDieSidesCommand);
            Assert.AreEqual(vm.RollDiceCommand, vm.DefaultCommand);
            Assert.IsTrue(string.IsNullOrEmpty(vm.DisplayText));
            Assert.IsFalse(vm.UseVerboseOutput);
            Assert.IsNull(vm.ConstantRollerValue);
            Assert.IsTrue(vm.HasBoundedResult);
            Assert.IsNull(vm.DefaultDieSides);
            Assert.IsNotNull(vm.DieRoller);
            Assert.IsInstanceOfType(vm.DieRoller, typeof(RandomDieRoller));
        }

        [TestMethod]
        public void MainViewModel_HelpTest()
        {
            // setup test
            MainViewModel vm = new MainViewModel();

            // run test
            bool result = vm.HelpCommand.Execute(null);

            // validate results
            Assert.IsFalse(result);
            Assert.IsTrue(vm.HelpCommand.CanExecute(null));
            Assert.IsTrue(vm.DisplayText.Contains("=== OnePlat.DiceNotation Command Line Tool (1.0.2) ==="));
            Assert.IsTrue(vm.DisplayText.Contains("Dice expressions:"));
        }

        [TestMethod]
        public void MainViewModel_SetVerboseTest()
        {
            // setup test
            MainViewModel vm = new MainViewModel();

            // run test
            bool result = vm.SetVerboseCommand.Execute(null);

            // validate results
            Assert.IsTrue(result);
            Assert.IsTrue(vm.SetVerboseCommand.CanExecute(null));
            Assert.IsTrue(vm.UseVerboseOutput);
        }

        [TestMethod]
        public void MainViewModel_UnboundResultTest()
        {
            // setup test
            MainViewModel vm = new MainViewModel();

            // run test
            bool result = vm.SetUnboundResultCommand.Execute(null);

            // validate results
            Assert.IsTrue(result);
            Assert.IsTrue(vm.SetUnboundResultCommand.CanExecute(null));
            Assert.IsFalse(vm.HasBoundedResult);
        }

        [TestMethod]
        public void MainViewModel_DefaultDieSidesTest()
        {
            // setup test
            MainViewModel vm = new MainViewModel();

            // run test
            bool result = vm.SetDefaultNumberDieSidesCommand.Execute("10");

            // validate results
            Assert.IsTrue(result);
            Assert.IsTrue(vm.SetDefaultNumberDieSidesCommand.CanExecute("10"));
            Assert.AreEqual(10, vm.DefaultDieSides);
        }

        [TestMethod]
        public void MainViewModel_DefaultDieSidesErrorTest()
        {
            // setup test
            MainViewModel vm = new MainViewModel();

            // run test
            bool result = vm.SetDefaultNumberDieSidesCommand.Execute("f5");

            // validate results
            Assert.IsFalse(result);
            Assert.IsTrue(vm.SetDefaultNumberDieSidesCommand.CanExecute("f5"));
            Assert.IsNull(vm.DefaultDieSides);
            Assert.IsNotNull(vm.DieRoller);
            Assert.IsInstanceOfType(vm.DieRoller, typeof(RandomDieRoller));
            Assert.IsTrue(vm.DisplayText.Contains("FormatException"));
        }

        [TestMethod]
        public void MainViewModel_ConstantRollerTest()
        {
            // setup test
            MainViewModel vm = new MainViewModel();

            // run test
            bool result = vm.ConstantRollerCommand.Execute("5");

            // validate results
            Assert.IsTrue(result);
            Assert.IsTrue(vm.ConstantRollerCommand.CanExecute("5"));
            Assert.AreEqual(5, vm.ConstantRollerValue);
            Assert.IsNotNull(vm.DieRoller);
            Assert.IsInstanceOfType(vm.DieRoller, typeof(ConstantDieRoller));
        }

        [TestMethod]
        public void MainViewModel_ConstantRollerErrorTest()
        {
            // setup test
            MainViewModel vm = new MainViewModel();

            // run test
            bool result = vm.ConstantRollerCommand.Execute("f5");

            // validate results
            Assert.IsFalse(result);
            Assert.IsTrue(vm.ConstantRollerCommand.CanExecute("f5"));
            Assert.IsNull(vm.ConstantRollerValue);
            Assert.IsNotNull(vm.DieRoller);
            Assert.IsInstanceOfType(vm.DieRoller, typeof(RandomDieRoller));
            Assert.IsTrue(vm.DisplayText.Contains("FormatException"));
        }

        [TestMethod]
        public void MainViewModel_RollDiceTest()
        {
            // setup test
            MainViewModel vm = new MainViewModel();

            // run test
            bool result = vm.RollDiceCommand.Execute("4d6k3+1");

            // validate results
            Assert.IsTrue(result);
            Assert.IsTrue(vm.RollDiceCommand.CanExecute("4d6k3+1"));
            Assert.IsTrue(vm.DisplayText.Contains("DiceRoll(4d6k3+1)"));
            Assert.IsFalse(vm.DisplayText.Contains("Terms list:"));
        }

        [TestMethod]
        public void MainViewModel_RollDiceConstantTest()
        {
            // setup test
            MainViewModel vm = new MainViewModel();
            vm.ConstantRollerCommand.Execute("1");

            // run test
            bool result = vm.RollDiceCommand.Execute("4d6k3+1");

            // validate results
            Assert.IsTrue(result);
            Assert.IsTrue(vm.RollDiceCommand.CanExecute("4d6k3+1"));
            Assert.IsTrue(vm.DisplayText.Contains("DiceRoll(4d6k3+1)"));
            Assert.IsTrue(vm.DisplayText.Contains("=> 4"));
            Assert.IsFalse(vm.DisplayText.Contains("Terms list:"));
        }

        [TestMethod]
        public void MainViewModel_RollDiceUnboundedTest()
        {
            // setup test
            MainViewModel vm = new MainViewModel();
            vm.ConstantRollerCommand.Execute("1");
            vm.SetUnboundResultCommand.Execute(null);

            // run test
            bool result = vm.RollDiceCommand.Execute("2d8-3");

            // validate results
            Assert.IsTrue(result);
            Assert.IsTrue(vm.RollDiceCommand.CanExecute("2d8-3"));
            Assert.IsTrue(vm.DisplayText.Contains("DiceRoll(2d8-3)"));
            Assert.IsTrue(vm.DisplayText.Contains("=> -1"));
            Assert.IsFalse(vm.DisplayText.Contains("Terms list:"));
        }

        [TestMethod]
        public void MainViewModel_RollDiceDefaultSidesTest()
        {
            // setup test
            MainViewModel vm = new MainViewModel();
            vm.ConstantRollerCommand.Execute("1");
            vm.SetVerboseCommand.Execute(null);
            vm.SetDefaultNumberDieSidesCommand.Execute("10");

            // run test
            bool result = vm.RollDiceCommand.Execute("4dk3");

            // validate results
            Assert.IsTrue(result);
            Assert.IsTrue(vm.RollDiceCommand.CanExecute("4dk3"));
            Assert.IsTrue(vm.DisplayText.Contains("DiceRoll(4dk3)"));
            Assert.IsTrue(vm.DisplayText.Contains("=> 3"));
            Assert.IsTrue(vm.DisplayText.Contains("Terms list:"));
            Assert.IsTrue(vm.DisplayText.Contains("DiceTerm.d10"));
        }

        [TestMethod]
        public void MainViewModel_RollDiceFudgeTest()
        {
            // setup test
            MainViewModel vm = new MainViewModel();

            // run test
            bool result = vm.RollDiceCommand.Execute("4f+1");

            // validate results
            Assert.IsTrue(result);
            Assert.IsTrue(vm.RollDiceCommand.CanExecute("4f+1"));
            Assert.IsTrue(vm.DisplayText.Contains("DiceRoll(4f+1)"));
            Assert.IsFalse(vm.DisplayText.Contains("Terms list:"));
        }

        [TestMethod]
        public void MainViewModel_RollDiceVerboseTest()
        {
            // setup test
            MainViewModel vm = new MainViewModel();
            vm.ConstantRollerCommand.Execute("2");
            vm.SetVerboseCommand.Execute(null);

            // run test
            bool result = vm.RollDiceCommand.Execute("4d6k3+1");

            // validate results
            Assert.IsTrue(result);
            Assert.IsTrue(vm.RollDiceCommand.CanExecute("4d6k3+1"));
            Assert.IsTrue(vm.DisplayText.Contains("DiceRoll(4d6k3+1)"));
            Assert.IsTrue(vm.DisplayText.Contains("=> 7"));
            Assert.IsTrue(vm.DisplayText.Contains("Terms list:"));
        }

        [TestMethod]
        public void MainViewModel_RollDiceErrorTest()
        {
            // setup test
            MainViewModel vm = new MainViewModel();

            // run test
            bool result = vm.RollDiceCommand.Execute("2dk+4");

            // validate results
            Assert.IsFalse(result);
            Assert.IsTrue(vm.RollDiceCommand.CanExecute("2dk+4"));
            Assert.IsFalse(vm.DisplayText.Contains("DiceRoll(4d6k3+1)"));
            Assert.IsTrue(vm.DisplayText.Contains("FormatException"));
        }
    }
}
