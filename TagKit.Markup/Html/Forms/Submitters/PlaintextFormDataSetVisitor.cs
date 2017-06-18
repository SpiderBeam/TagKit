using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagKit.Markup.IO;

namespace TagKit.Markup.Html.Forms.Submitters
{
    sealed class PlaintextFormDataSetVisitor : IFormSubmitter
    {
        #region Fields

        private readonly List<String> _lines;

        #endregion

        #region ctor

        public PlaintextFormDataSetVisitor()
        {
            _lines = new List<String>();
        }

        #endregion

        #region Methods

        public void Text(FormDataSetEntry entry, String value)
        {
            if (entry.HasName && value != null)
            {
                Add(entry.Name, value);
            }
        }

        public void File(FormDataSetEntry entry, String fileName, String contentType, IFile content)
        {
            if (entry.HasName && content?.Name != null)
            {
                Add(entry.Name, content.Name);
            }
        }

        public void Serialize(StreamWriter stream)
        {
            var content = String.Join("\r\n", _lines);
            stream.Write(content);
        }

        #endregion

        #region Helpers

        private void Add(String name, String value)
        {
            _lines.Add(String.Concat(name, "=", value));
        }

        #endregion
    }
}
