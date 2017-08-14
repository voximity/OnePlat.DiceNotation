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
            Assert.AreEqual(vm.RollDiceCommand, vm.DefaultCommand);
            Assert.IsTrue(string.IsNullOrEmpty(vm.DisplayText));
            Assert.IsFalse(vm.UseVerboseOutput);
            Assert.IsNull(vm.ConstantRollerValue);
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
            Assert.IsTrue(vm.DisplayText.Contains("=== OnePlat.DiceNotation Command Line Tool (1.0.0) ==="));
            Assert.IsTrue(vm.DisplayText.Contains("Dice expressions:"));
        }
    }
}
