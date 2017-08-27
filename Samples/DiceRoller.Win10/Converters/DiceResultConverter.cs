// <copyright file="DiceResultConverter.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using OnePlat.DiceNotation;
using System;
using Windows.UI.Xaml.Data;

namespace DiceRoller.Win10.Converters
{
    /// <summary>
    /// Value converter to change a DiceResult into a display string.
    /// </summary>
    public class DiceResultConverter : IValueConverter
    {
        /// <summary>
        /// Converts the DiceResult into a string respresentation.
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <param name="targetType">target type of the conversion</param>
        /// <param name="parameter">converter parameter</param>
        /// <param name="language">language</param>
        /// <returns>Converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType != typeof(string))
            {
                throw new ArgumentException("Unexpected type passed to converter.", nameof(targetType));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            DiceResult dr = value as DiceResult;
            if (dr == null)
            {
                throw new ArgumentException("Object not of type DiceResult.", nameof(value));
            }

            return string.Format("{0} ({1})", dr.Value, dr.DiceExpression);
        }

        /// <summary>
        /// Converts back from string representation to a DiceResult.
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <param name="targetType">target type of the conversion</param>
        /// <param name="parameter">converter parameter</param>
        /// <param name="language">language</param>
        /// <returns>Converted value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // the reverse conversion is not supported.
            throw new NotSupportedException();
        }
    }
}
