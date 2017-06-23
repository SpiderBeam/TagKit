using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagKit.Markup
{
    internal partial interface IDtdParser
    {
        IDtdInfo ParseInternalDtd(IDtdParserAdapter adapter, bool saveInternalSubset);
        IDtdInfo ParseFreeFloatingDtd(string baseUri, string docTypeName, string publicId, string systemId, string internalSubset, IDtdParserAdapter adapter);
    }
}
