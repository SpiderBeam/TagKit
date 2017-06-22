using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagKit.Markup
{
    /// <summary>
    /// A collection of official DOM error codes.
    /// </summary>
    public enum DomError : byte
    {
        /// <summary>
        /// The index is not in the allowed range.
        /// </summary>
        IndexSizeError = 0x1,
        /// <summary>
        /// The size of the string is invalid.
        /// </summary>
        DomStringSize = 0x2,
        /// <summary>
        /// The operation would yield an incorrect node tree.
        /// </summary>
        HierarchyRequest = 0x3,
        /// <summary>
        /// The object is in the wrong document.
        /// </summary>
        WrongDocument = 0x4,
        /// <summary>
        /// Invalid character detected.
        /// </summary>
        InvalidCharacter = 0x5,
        /// <summary>
        /// The data is allowed for this object.
        /// </summary>
        NoDataAllowed = 0x6,
        /// <summary>
        /// The object can not be modified.
        /// </summary>
        NoModificationAllowed = 0x7,
        /// <summary>
        /// The object can not be found here.
        /// </summary>
        NotFound = 0x8,
        /// <summary>
        /// The operation is not supported.
        /// </summary>
        NotSupported = 0x9,
        /// <summary>
        /// The element is already in-use.
        /// </summary>
        InUse = 0xA,
        /// <summary>
        /// The object is in an invalid state.
        /// </summary>
        InvalidState = 0xB,
        /// <summary>
        /// The string did not match the expected pattern.
        /// </summary>
        Syntax = 0xC,
        /// <summary>
        /// The object can not be modified in this way.
        /// </summary>
        InvalidModification = 0xD,
        /// <summary>
        /// The operation is not allowed by namespaces in XML.
        /// </summary>
        Namespace = 0xE,
        /// <summary>
        /// The object does not support the operation or argument.
        /// </summary>
        InvalidAccess = 0xF,
        /// <summary>
        /// The validation failed.
        /// </summary>
        Validation = 0xF,
        /// <summary>
        /// The provided argument type is invalid.
        /// </summary>
        TypeMismatch = 0x11,
        /// <summary>
        /// The operation is insecure.
        /// </summary>
        Security = 0x12,
        /// <summary>
        /// A network error occurred.
        /// </summary>
        Network = 0x13,
        /// <summary>
        /// The operation was aborted.
        /// </summary>
        Abort = 0x14,
        /// <summary>
        /// The given URL does not match another URL.
        /// </summary>
        UrlMismatch = 0x15,
        /// <summary>
        /// The quota has been exceeded.
        /// </summary>
        QuotaExceeded = 0x16,
        /// <summary>
        /// The operation timed out.
        /// </summary>
        Timeout = 0x17,
        /// <summary>
        /// The supplied node is incorrect or has an incorrect ancestor for this operation.
        /// </summary>
        InvalidNodeType = 0x18,
        /// <summary>
        /// The object can not be cloned.
        /// </summary>
        DataClone = 0x19,
    }
}
