using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagKit.Configuration.Services
{
    /// <summary>
    /// Represents a service to determine the default encoding.
    /// </summary>
    public interface IEncodingProvider
    {
        /// <summary>
        /// Suggests the initial Encoding for the given locale.
        /// </summary>
        /// <param name="locale">
        /// The locale defined by the BCP 47 language tag.
        /// </param>
        /// <returns>The suggested encoding.</returns>
        Encoding Suggest(String locale);
    }
}
