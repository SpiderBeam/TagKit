using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagKit.Markup.Html.Forms.Submitters.Json
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
