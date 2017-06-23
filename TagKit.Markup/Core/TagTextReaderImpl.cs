using System.Collections.Generic;

namespace TagKit.Markup
{
    internal partial class TagTextReaderImpl : TagReader, ITagLineInfo, ITagNamespaceResolver
    {
        #region Overrides of TagReader

        public override ReadState ReadState { get; }

        #endregion

        #region Implementation of ITagLineInfo

        public bool HasLineInfo()
        {
            throw new System.NotImplementedException();
        }

        public int LineNumber { get; }
        public int LinePosition { get; }

        #endregion

        #region Implementation of ITagNamespaceResolver

        public IDictionary<string, string> GetNamespacesInScope(TagNamespaceScope scope)
        {
            throw new System.NotImplementedException();
        }

        public string LookupNamespace(string prefix)
        {
            throw new System.NotImplementedException();
        }

        public string LookupPrefix(string namespaceName)
        {
            throw new System.NotImplementedException();
        }

        #region Overrides of TagReader
        /// <summary>
        /// Reads next node from the input data
        /// </summary>
        /// <returns></returns>
        public override bool Read()
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #endregion
    }
}