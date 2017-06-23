using System;

namespace TagKit.Markup
{
    /// <summary>
    /// Table of atomized string objects. This provides an
    /// efficient means for the markup parser to use the same string object for all
    /// repeated element and attribute names in an markup document.
    /// </summary>
    public abstract class TagNameTable
    {
        /// <summary>
        /// Gets the atomized String object containing the same
        /// chars as the specified range of chars in the given char array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public abstract String Get(char[] array, int offset, int length);
        /// <summary>
        /// Gets the atomized String object containing the same
        /// value as the specified string.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public abstract String Get(String array);
        /// <summary>
        /// Creates a new atom for the characters at the specified range
        /// of characters in the specified string.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public abstract String Add(char[] array, int offset, int length);
        /// <summary>
        /// Creates a new atom for the specified string.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public abstract String Add(String array);


    }
}
