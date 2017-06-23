using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace TagKit.Markup
{
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
    public class TagTextReader : TagReader, ITagLineInfo, ITagNamespaceResolver
    {
        #region Member fields

        TagTextReaderImpl impl;


        #endregion

        #region Overrides of TagReader

        public override ReadState ReadState { get; }

        #endregion

        #region Implementation of ITagLineInfo

        public bool HasLineInfo()
        {
            throw new NotImplementedException();
        }

        public int LineNumber { get; }
        public int LinePosition { get; }

        #endregion

        #region Implementation of ITagNamespaceResolver

        public IDictionary<string, string> GetNamespacesInScope(TagNamespaceScope scope)
        {
            throw new NotImplementedException();
        }

        public string LookupNamespace(string prefix)
        {
            throw new NotImplementedException();
        }

        public string LookupPrefix(string namespaceName)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Internal helper methods
        internal TagTextReaderImpl Impl
        {
            get { return impl; }
        }

        #region Overrides of TagReader

        public override bool Read()
        {
            return impl.Read();
        }

        #endregion

        #endregion
    }
}
