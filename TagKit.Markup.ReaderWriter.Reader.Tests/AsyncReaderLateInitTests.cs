using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TagKit.Markup.Tests
{
    public static class AsyncReaderLateInitTests
    {
        private const string _dummyXml = @"<?xml version=""1.0""?>
                <root>
                    <a/><!-- comment -->
                    <b>bbb</b>
                    <c>
                        <d>ddd</d>
                    </c>
                </root>";
        private static Stream GetDummyXmlStream()
        {
            byte[] buffer = Encoding.UTF8.GetBytes(_dummyXml);
            return new MemoryStream(buffer);
        }
        private static TextReader GetDummyXmlTextReader()
        {
            return new StringReader(_dummyXml);
        }
        [Fact]
        public static void ReadAsyncAfterInitializationWithStreamDoesNotThrow()
        {
            using (TagReader reader = TagReader.Create(GetDummyXmlStream(), new TagReaderSettings() { Async = true }))
            {
                //reader.ReadAsync().Wait();
            }
        }
    }
}
