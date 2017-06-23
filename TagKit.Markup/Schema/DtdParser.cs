using System;

namespace TagKit.Markup
{
    internal partial class DtdParser : IDtdParser
    {
        #region Implementation of IDtdParser

        public IDtdInfo ParseInternalDtd(IDtdParserAdapter adapter, bool saveInternalSubset)
        {
            throw new NotImplementedException();
        }

        public IDtdInfo ParseFreeFloatingDtd(string baseUri, string docTypeName, string publicId, string systemId,
            string internalSubset, IDtdParserAdapter adapter)
        {
            throw new NotImplementedException();
        }

        #endregion

        public static IDtdParser Create()
        {
            throw new NotImplementedException();
        }
    }
}
