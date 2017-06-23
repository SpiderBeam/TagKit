using Xunit;

namespace TagKit.Markup.Tests.NodeTests
{
    public class NameTests
    {
        [Fact]
        public static void XmlDeclaration()
        {
            var xmlDocument = new Document();
            //Seam:DOM解析
            xmlDocument.LoadXml("<?xml version=\"1.0\" standalone=\"no\"?><root />");

            Assert.Equal("xml", xmlDocument.FirstChild.Name);
        }
    }
}
