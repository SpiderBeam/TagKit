using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TagKit.Markup;
using TagKit.Markup.Dom;
using TagKit.Markup.Events;
using TagKit.Markup.Parser;

namespace TagKit.Xml.Parser
{
    /// <summary>
    /// Creates an instance of the XML parser front-end.
    /// </summary>
    public class XmlParser : EventTarget, IXmlParser
    {
        private readonly List<System.Object> _services;

        #region Implementation of IParser

        public event DomEventHandler Parsing;
        public event DomEventHandler Parsed;
        public event DomEventHandler Error;

        #endregion

        #region Implementation of IXmlParser
        /// <summary>
        /// Parses the string and returns the result.
        /// </summary>
        public XmlDocument ParseDocument(string source)
        {
            var document = CreateDocument(source);
            return Parse(document);
        }

        public XmlDocument ParseDocument(Stream source)
        {
            throw new System.NotImplementedException();
        }

        public Task<XmlDocument> ParseDocumentAsync(string source, CancellationToken cancel)
        {
            throw new System.NotImplementedException();
        }

        public Task<XmlDocument> ParseDocumentAsync(Stream source, CancellationToken cancel)
        {
            throw new System.NotImplementedException();
        }

        public Task<Document> ParseDocumentAsync(Document document, CancellationToken cancel)
        {
            throw new System.NotImplementedException();
        }

        #endregion
        #region Helpers
        private XmlDocument CreateDocument(string source)
        {
            var textSource = new TextSource(source);
            return CreateDocument(textSource);
        }
        private XmlDocument CreateDocument(Stream source)
        {
            _services.Add(new LocaleEncodingProvider());
            var textSource = new TextSource(source, this.GetDefaultEncoding());
            return CreateDocument(textSource);
        }

        private Encoding GetDefaultEncoding()
        {
            var provider = this.GetProvider<IEncodingProvider>();
            var locale = this.GetLanguage();
            return provider?.Suggest(locale) ?? Encoding.UTF8;
        }

        private string GetLanguage()
        {
            return GetCulture().Name;
        }

        private CultureInfo GetCulture()
        {
            return GetService<CultureInfo>() ?? CultureInfo.CurrentUICulture;
        }
        public T GetService<T>() where T : class
        {
            var count = _services.Count;

            for (var i = 0; i < count; i++)
            {
                var service = _services[i];
                var instance = service as T;

                if (instance == null)
                {
                    var creator = service as Func< T>;

                    if (creator == null)
                        continue;

                    instance = creator.Invoke();
                    _services[i] = instance;
                }

                return instance;
            }
            return null;
        }

        private TProvider GetProvider<TProvider>()
            where TProvider : class
        {
            return GetServices<TProvider>().SingleOrDefault();
        }
       
        public IEnumerable<T> GetServices<T>() where T : class
        {
            var count = _services.Count;

            for (var i = 0; i < count; i++)
            {
                var service = _services[i];
                var instance = service as T;

                if (instance == null)
                {
                    var creator = service as Func<T>;

                    if (creator == null)
                        continue;

                    instance = creator.Invoke();
                    _services[i] = instance;
                }

                yield return instance;
            }
        }
        private XmlDocument CreateDocument(TextSource textSource)
        {
            var document = new XmlDocument(textSource);
            return document;
        }
        private XmlDocument Parse(XmlDocument document)
        {
            var parser = CreateBuilder(document);
            InvokeEventListener(new XmlParseEvent(document, completed: false));
            //parser.Parse(_options);
            InvokeEventListener(new XmlParseEvent(document, completed: true));
            return document;
        }
        private XmlDomBuilder CreateBuilder(Document document)
        {
            var parser = new XmlDomBuilder(document);
            return parser;
        }

        #endregion

    }
}
