// <copyright file="TermResultListHelper.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using OnePlat.DiceNotation.Converters;
using OnePlat.DiceNotation.DiceTerms;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DiceRoller.Mvc
{
    /// <summary>
    /// Html helper class to format term results lists.
    /// </summary>
    public static class TermResultListHelper
    {
        private static TermResultListConverter converter = new TermResultListConverter();

        /// <summary>
        /// Formats the specified list of term results into the corresponding text format.
        /// </summary>
        /// <param name="helper">Html helper</param>
        /// <param name="results">Term results to convert</param>
        /// <returns>Text representation for the list.</returns>
        public static string DisplayResultsFor(this HtmlHelper helper, IReadOnlyList<TermResult> results)
        {
            string result = (string)converter.Convert(results, typeof(string), null, "en-us");
            return result;
        }
    }
}