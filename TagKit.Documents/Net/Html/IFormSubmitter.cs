﻿using System.IO;

namespace TagKit.Documents.Net.Html
{
    /// <summary>
    /// Strategy for serializing form data sets.
    /// </summary>
    public interface IFormSubmitter : IFormDataSetVisitor
    {
        /// <summary>
        /// Serializes the visited form data set to the stream.
        /// </summary>
        /// <param name="stream">The stream writer to use.</param>
        void Serialize(StreamWriter stream);
    }
}