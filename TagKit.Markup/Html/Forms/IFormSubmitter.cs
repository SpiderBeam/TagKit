using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagKit.Markup.Html.Forms
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
