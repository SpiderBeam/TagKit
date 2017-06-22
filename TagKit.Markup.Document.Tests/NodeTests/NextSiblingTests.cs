using Xunit;

namespace TagKit.Markup
{
    public static class NextSiblingTests
    {
        [Fact]
        public static void OnTextNode()
        {
            var xmlDocument = new Document();
            xmlDocument.LoadXml("<root>some text<child1/></root>");

            Assert.Equal(NodeType.Text, xmlDocument.DocumentElement.ChildNodes[0].NodeType);
            Assert.Equal(xmlDocument.DocumentElement.ChildNodes[0].NextSibling,
                xmlDocument.DocumentElement.ChildNodes[1]);
        }
    }
}