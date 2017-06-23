using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagKit.Markup.Properties;

namespace TagKit.Markup
{
    internal class TagLoader
    {
        Document doc;
        TagReader reader;
        bool preserveWhitespace;

        internal void Load(Document doc, TagReader reader, bool preserveWhitespace)
        {
            this.doc = doc;
            // perf: unwrap XmlTextReader if no one derived from it
            if (reader.GetType() == typeof(TagKit.Markup.TagTextReader))
            {
                this.reader = ((TagTextReader)reader).Impl;
            }
            else
            {
                this.reader = reader;
            }
            this.preserveWhitespace = preserveWhitespace;
            if (doc == null)
                throw new ArgumentException(Res.GetString(Resources.Xdom_Load_NoDocument));
            if (reader == null)
                throw new ArgumentException(Res.GetString(Resources.Xdom_Load_NoReader));
            //doc.SetBaseURI(reader.BaseURI);
            //if (reader.Settings != null
            //    && reader.Settings.ValidationType == ValidationType.Schema)
            //{
            //    doc.Schemas = reader.Settings.Schemas;
            //}
            if (this.reader.ReadState != ReadState.Interactive)
            {
                //Seam:Read
                if (!this.reader.Read())
                    return;
            }
            //LoadDocSequence(doc);
        }

    }
}
