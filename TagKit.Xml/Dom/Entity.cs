using System;
using TagKit.Markup;

namespace TagKit.Xml
{
    /// <summary>
    /// Represents a parsed or unparsed entity in the DOM document.
    /// </summary>
    public class Entity : Node
    {
        private string _publicId;
        private string _systemId;
        private String _notationName;
        private String _name;
        private String _unparsedReplacementStr;
        private String _baseURI;
        private LinkedNode _lastChild;
        private bool _childrenFoliating;
        internal Entity(String name, String strdata, string publicId, string systemId, String notationName, XmlDocument doc) : base(doc)
        {
            //_name = doc.NameTable.Add(name);
            _publicId = publicId;
            _systemId = systemId;
            _notationName = notationName;
            _unparsedReplacementStr = strdata;
            _childrenFoliating = false;
        }

        #region Overrides of Node

        public override NodeType NodeType
        {
            get { return NodeType.Entity; }
        }
        #endregion
    }
}
