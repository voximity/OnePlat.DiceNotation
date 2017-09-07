// <copyright file="GlobalSuppressions.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:Elements must appear in the correct order", Justification = "Exception: keeping the singleton code together for readability", Scope = "member", Target = "~F:DiceRoller.Win10.Services.AppServices.diceService")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:Elements must appear in the correct order", Justification = "Exception: keeping INotifyPropertyChanged code together for readability", Scope = "member", Target = "~E:DiceRoller.Win10.Views.FrequencyStatsPage.PropertyChanged")]