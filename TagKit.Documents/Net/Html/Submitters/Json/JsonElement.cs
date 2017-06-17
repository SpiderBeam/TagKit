using System;

namespace TagKit.Documents.Net.Html.Submitters.Json
{
    abstract class JsonElement
    {
        public virtual JsonElement this[String key]
        {
            get { throw new InvalidOperationException(); }
            set { throw new InvalidOperationException(); }
        }
    }
}
