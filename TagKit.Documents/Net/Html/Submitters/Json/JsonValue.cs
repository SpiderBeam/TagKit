﻿using System;
using System.Globalization;
using TagKit.Foundation.Text;

namespace TagKit.Documents.Net.Html.Submitters.Json
{
    sealed class JsonValue : JsonElement
    {
        readonly String _value;

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
