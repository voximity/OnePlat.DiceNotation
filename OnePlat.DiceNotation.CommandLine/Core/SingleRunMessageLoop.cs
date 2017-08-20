// <copyright file="SingleRunMessageLoop.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.DiceNotation.CommandLine
// Author           : DarthPedro
// Created          : 8/13/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/20/2017
//-----------------------------------------------------------------------
// <summary>
//       This project is licensed under the MIT license.
//
//       OnePlat.DiceNotation.CommandLine is an open source project that parses,
//  evalutes, and rolls dice that conform to the defined Dice notiation. This
//  command line tool is for testing purposes.
//  This notation is usable in any form of random dice games and role-playing
//  games like D&D and d20.
// </summary>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace OnePlat.DiceNotation.CommandLine.Core
{
    /// <summary>
    /// Message loop runner for running the command-line interface once based on
    /// the command line parameters and exits.
    /// </summary>
    public class SingleRunMessageLoop : IMessageLoop
    {
        #region Members
        private const char CommandParamSeparator = ':';
        private static Regex whitespaceRegex = new Regex(@"\s+");
        private List<ICommand> operationCommmands = new List<ICommand>();
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleRunMessageLoop"/> class.
        /// </summary>
        /// <param name="mainView">Starting view for the application message loop</param>
        public SingleRunMessageLoop(IView mainView)
        {
            this.CurrentView = mainView;

            this.BindOperationCommand(mainView.DataContext);
        }

        #region IMessageLoop methods

        /// <inheritdoc/>
        public IView CurrentView { get; private set; }

        /// <inheritdoc/>
        public void Run(string[] args)
        {
            List<string> parameters = this.OrderArguments(args);

            foreach (string p in parameters)
            {
                Debug.WriteLine("==> Processing parameter: " + p);
                ICommand command = this.FindAssociatedCommand(p);
                if (command != null)
                {
                    if (this.ExecuteCommand(command, p) == false)
                    {
                        break;
                    }
                }
                else
                {
                    if (this.ProcessDefaultCommand(p) == false)
                    {
                        break;
                    }
                }
            }

            this.CurrentView.Update();
        }
        #endregion

        #region Helper methods

        private List<string> OrderArguments(string[] args)
        {
            List<Tuple<int, string>> results = new List<Tuple<int, string>>();

            foreach (string s in args)
            {
                ICommand command = this.FindAssociatedCommand(s);
                Tuple<int, string> item = new Tuple<int, string>(command == null ? int.MaxValue : command.Order, s);
                results.Add(item);
            }

            return results.OrderBy(i => i.Item1).Select(s => s.Item2).ToList();
        }

        private ICommand FindAssociatedCommand(string parameter)
        {
            foreach (ICommand command in this.operationCommmands)
            {
                foreach (string operation in command.Operations)
                {
                    if (parameter.StartsWith(operation))
                    {
                        return command;
                    }
                }
            }

            return null;
        }

        private bool ExecuteCommand(ICommand command, string commandParam)
        {
            Debug.WriteLine("   ==> Command running: " + command.GetType().Name);

            object parameter = this.GetExecuteParameter(commandParam);
            bool continueCommandProcessing = true;

            if (command.CanExecute(parameter))
            {
                continueCommandProcessing = command.Execute(parameter);
            }

            return continueCommandProcessing;
        }

        private object GetExecuteParameter(string commandParam)
        {
            object result = null;

            commandParam = whitespaceRegex.Replace(commandParam, string.Empty);
            string[] list = commandParam.Split(new char[] { CommandParamSeparator });
            if (list.Length == 2)
            {
                result = list.Last();
            }

            return result;
        }

        private bool ProcessDefaultCommand(string parameter)
        {
            Debug.WriteLine("   ==> Command handler not found for: " + parameter);
            if (parameter.StartsWith("-") == false)
            {
                IViewModel vm = this.CurrentView.DataContext;
                if (vm != null && vm.DefaultCommand != null)
                {
                    Debug.WriteLine("   ==> Executing default command:" + vm.DefaultCommand.GetType().Name);
                    if (this.ExecuteCommand(vm.DefaultCommand, "default:" + parameter) == false)
                    {
                        return false;
                    }
                }
            }
            else
            {
                Debug.WriteLine("   ==> Error processing command!");
            }

            return true;
        }

        private void BindOperationCommand(IViewModel viewModel)
        {
            Type vm = viewModel.GetType();
            foreach (PropertyInfo prop in vm.GetProperties())
            {
                if (prop.PropertyType == typeof(ICommand))
                {
                    if (prop.GetValue(viewModel) is ICommand command)
                    {
                        this.operationCommmands.Add(command);
                    }
                }
            }
        }
        #endregion
    }
}
