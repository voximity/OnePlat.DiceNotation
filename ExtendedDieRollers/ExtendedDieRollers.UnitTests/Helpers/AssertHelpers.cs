using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnePlat.DiceNotation.DiceTerms;

namespace OnePlat.DiceNotation.UnitTests.Helpers
{
    public static class AssertHelpers
    {
        public static void IsWithinRangeInclusive(int min, int max, int value)
        {
            Assert.IsTrue(value >= min && value <= max);
        }
    }
}
