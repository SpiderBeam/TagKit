using System;
using System.IO;

namespace TagKit.Markup
{
    public class Document:Node
    {

        private DomNameTable _domNameTable; // hash table of TagName
        private TagImplementation _implementation;

        // special name strings for
        internal string strDocumentName;
        internal string strDocumentFragmentName;
        internal string strCommentName;
        internal string strTextName;
        internal string strCDataSectionName;
        internal string strEntityName;
        internal string strID;
        internal string strXmlns;
        internal string strXml;
        internal string strSpace;
        internal string strLang;
        internal string strEmpty;

        internal string strNonSignificantWhitespaceName;
        internal string strSignificantWhitespaceName;
        internal string strReservedXmlns;
        internal string strReservedXml;

        internal String baseURI;

        internal object objLock;
        internal static EmptyEnumerator EmptyEnumerator = new EmptyEnumerator();

        public Document() : this(new TagImplementation())
        {
        }
        public Document(TagNameTable nt) : this(new TagImplementation(nt))
        {
        }
        protected internal Document(TagImplementation imp) : base()
        {
            _implementation = imp;
            _domNameTable = new DomNameTable(this);

            // force the following string instances to be default in the nametable
            TagNameTable nt = this.NameTable;
            nt.Add(string.Empty);
            strDocumentName = nt.Add("#document");
            strDocumentFragmentName = nt.Add("#document-fragment");
            strCommentName = nt.Add("#comment");
            strTextName = nt.Add("#text");
            strCDataSectionName = nt.Add("#cdata-section");
            strEntityName = nt.Add("#entity");
            strID = nt.Add("id");
            strNonSignificantWhitespaceName = nt.Add("#whitespace");
            strSignificantWhitespaceName = nt.Add("#significant-whitespace");
            strXmlns = nt.Add("xmlns");
            strXml = nt.Add("xml");
            strSpace = nt.Add("space");
            strLang = nt.Add("lang");
            strReservedXmlns = nt.Add(TagConst.ReservedNsXmlNs);
            strReservedXml = nt.Add(TagConst.ReservedNsXml);
            strEmpty = nt.Add(String.Empty);
            baseURI = String.Empty;

            objLock = new object();
        }

        #region Overrides of Node

        public override NodeType NodeType { get; }
        /// <summary>
        /// Gets the root XmlElement for the document.
        /// </summary>
        public Element DocumentElement
        {
            get { return (Element)FindChild(NodeType.Element); }
        }
        //extensions

        /// <summary>
        /// Gets the TagNameTable associated with this
        /// implementation.        
        /// /// </summary>
        public TagNameTable NameTable
        {
            get { return _implementation.NameTable; }
        }

        internal virtual Node FindChild(NodeType type)
        {
            for (Node child = FirstChild; child != null; child = child.NextSibling)
            {
                if (child.NodeType == type)
                {
                    return child;
                }
            }
            return null;
        }
        #endregion

        /// <summary>
        /// Loads the XML document from the specified string.
        /// </summary>
        /// <param name="xml"></param>
        public virtual void LoadXml(string xml)
        {
            //TextReader reader = SetupReader(new TextReader(new StringReader(xml), NameTable));
            //try
            //{
            //    Load(reader);
            //}
            //finally
            //{
            //    reader.Close();
            //}
        }
    }
}
