using Xunit;

namespace TagKit.Markup.Tests.NodeTests
{
    public class NameTests
    {
        [Fact]
        public static void XmlDeclaration()
        {
            var xmlDocument = new Document();
            xmlDocument.LoadXml("<?xml version=\"1.0\" standalone=\"no\"?><root />");

            Assert.Equal("xml", xmlDocument.FirstChild.Name);
        }
    }
}
