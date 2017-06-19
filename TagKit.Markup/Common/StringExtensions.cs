using System;
using System.Runtime.CompilerServices;

namespace TagKit.Markup.Common
{
    /// <summary>
    /// Useful methods for string objects.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Checks if two strings are exactly equal.
        /// </summary>
        /// <param name="current">The current string.</param>
        /// <param name="other">The other string.</param>
        /// <returns>True if both are equal, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Is(this String current, String other)
        {
            return String.Equals(current, other, StringComparison.Ordinal);
        }
        /// <summary>
        /// Checks if two strings are equal when viewed case-insensitive.
        /// </summary>
        /// <param name="current">The current string.</param>
        /// <param name="other">The other string.</param>
        /// <returns>True if both are equal, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Isi(this String current, String other)
        {
            return String.Equals(current, other, StringComparison.OrdinalIgnoreCase);
        }

    }
}
