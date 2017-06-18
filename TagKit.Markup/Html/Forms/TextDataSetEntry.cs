using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagKit.Markup.Html.Forms
{
    /// <summary>
    /// A text entry in a form.
    /// </summary>
    sealed class TextDataSetEntry : FormDataSetEntry
    {
        private readonly String _value;

        public TextDataSetEntry(String name, String value, String type)
            : base(name, type)
        {
            _value = value;
        }

        public override Boolean Contains(String boundary, Encoding encoding)
        {
            return _value != null && _value.Contains(boundary);
        }

        public override void Accept(IFormDataSetVisitor visitor)
        {
            visitor.Text(this, _value);
        }
    }
}
