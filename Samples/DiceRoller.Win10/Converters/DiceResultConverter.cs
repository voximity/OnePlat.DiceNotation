// <copyright file="DiceResultConverter.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using Windows.UI.Xaml.Data;

namespace DiceRoller.Win10.Converters
{
    /// <summary>
    /// Value converter to change a DiceResult into a display string.
    /// Just uses base implementation in OnePlat.DiceNotation library, but adds
    /// IValueConvert interface, so that it is useable by XAML.
    /// </summary>
    public class DiceResultConverter : OnePlat.DiceNotation.Converters.DiceResultConverter, IValueConverter
    {
    }
}
