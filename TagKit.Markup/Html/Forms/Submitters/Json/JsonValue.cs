using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagKit.Foundation.Text;

namespace TagKit.Markup.Html.Forms.Submitters.Json
{
    sealed class JsonValue : JsonElement
    {
        private readonly String _value;

        public JsonValue(String value)
        {
            _value = value.CssString();
        }

        public JsonValue(Double value)
        {
            _value = value.ToString(CultureInfo.InvariantCulture);
        }

        public JsonValue(Boolean value)
        {
            _value = value ? "true" : "false";
        }

        public override String ToString()
        {
            return _value;
        }
    }
}
