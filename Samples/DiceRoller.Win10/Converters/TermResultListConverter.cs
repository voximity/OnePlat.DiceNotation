// <copyright file="TermResultListConverter.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using OnePlat.DiceNotation.DiceTerms;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Data;

namespace DiceRoller.Win10.Converters
{
    /// <summary>
    /// Value converter to change a list of TermResults into a display string.
    /// </summary>
    public class TermResultListConverter : IValueConverter
    {
        /// <summary>
        /// Converts the TermResults into a string respresentation.
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

            List<TermResult> list = value as List<TermResult>;
            if (list == null)
            {
                throw new ArgumentException("Object not of type List<TermResult>.", nameof(value));
            }

            return this.DiceRollsToString(list);
        }

        /// <summary>
        /// Converts back from string representation to a list of TermResults.
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <param name="targetType">target type of the conversion</param>
        /// <param name="parameter">converter parameter</param>
        /// <param name="language">language</param>
        /// <returns>Converted value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Converts the DiceResult to a string representation of a list of
        /// dice roll results.
        /// </summary>
        /// <param name="results">DiceResult to use</param>
        /// <returns>string represenation of DiceResult dice rolls</returns>
        private string DiceRollsToString(List<TermResult> results)
        {
            var list = from r in results
                       where r.Type.Contains(nameof(DiceTerm))
                       select r;

            string res = string.Empty;
            foreach (TermResult item in list)
            {
                if (item.AppliesToResultCalculation)
                {
                    res += item.Value.ToString() + ", ";
                }
                else
                {
                    res += item.Value.ToString() + "*, ";
                }
            }

            return res.Trim().TrimEnd(',');
        }
    }
}
