using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace TagKit.Markup
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
        /// <summary>
        /// Strips all leading and trailing space characters from the given string.
        /// </summary>
        /// <param name="str">The string to examine.</param>
        /// <returns>A new string, which excludes the leading and tailing spaces.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String StripLeadingTrailingSpaces(this String str)
        {
            return StripLeadingTrailingSpaces(str.ToCharArray());
        }

        /// <summary>
        /// Strips all leading and trailing space characters from the given char array.
        /// </summary>
        /// <param name="array">The array of characters to examine.</param>
        /// <returns>A new string, which excludes the leading and tailing spaces.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String StripLeadingTrailingSpaces(this Char[] array)
        {
            var start = 0;
            var end = array.Length - 1;

            while (start < array.Length && array[start].IsSpaceCharacter())
            {
                start++;
            }

            while (end > start && array[end].IsSpaceCharacter())
            {
                end--;
            }

            return new String(array, start, 1 + end - start);
        }
        /// <summary>
        /// Converts the given string to an integer.
        /// </summary>
        /// <param name="s">The hexadecimal representation.</param>
        /// <returns>The integer number.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 FromHex(this String s)
        {
            return Int32.Parse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Converts the given string to an integer.
        /// </summary>
        /// <param name="s">The decimal representation.</param>
        /// <returns>The integer number.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 FromDec(this String s)
        {
            return Int32.Parse(s, NumberStyles.Integer, CultureInfo.InvariantCulture);
        }

    }
}
