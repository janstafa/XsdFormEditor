using System;

namespace SemeionModulesDesigner.Helpers
{
    /// <summary>
    /// Contains extension methods for DateTime.
    /// </summary>
    internal static class DateTimeHelper
    {
        /// <summary>
        /// Extension method to check if DateTime value is real date.
        /// </summary>
        /// <param name="dateTime">Checked value.</param>
        /// <returns>True if value is real date, false if not.</returns>
        internal static bool HasMeaning(this DateTime dateTime)
        {
            return DateTime.MinValue < dateTime & dateTime < DateTime.MaxValue;
        }
    }
}