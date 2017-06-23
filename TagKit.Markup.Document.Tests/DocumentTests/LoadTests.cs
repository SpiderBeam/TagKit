using System.IO;
using Xunit;

namespace TagKit.Markup.Tests
{
    public class LoadTests
    {
        [Fact]
        public void LoadDocumentFromFile()
        {
            TextReader textReader = File.OpenText(@"example.xml");
            TagReaderSettings settings = new TagReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.DtdProcessing = DtdProcessing.Ignore;
            Document doc = new Document();

            //using (StringReader sr = new StringReader(textReader.ReadToEnd()))
            //using (TagReader reader = TagReader.Create(sr, settings))
            //{
            //    doc.Load(reader);
            //}
        }
    }
}
