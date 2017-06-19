using System;
using System.Reflection;
using TagKit.Foundation.Attributes;

namespace TagKit.Markup.Common
{
    /// <summary>
    /// Some methods for working with bare objects.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Retrieves a string describing the error of a given error code.
        /// </summary>
        /// <param name="code">A specific error code.</param>
        /// <returns>The description of the error.</returns>
        public static String GetMessage<T>(this T code)
            where T : struct
        {
            var type = typeof(T).GetTypeInfo();
            var field = type.GetDeclaredField(code.ToString());
            var description = field.GetCustomAttribute<DomDescriptionAttribute>()?.Description;
            return description ?? "An unknown error occurred.";
        }

    }
}
