using System.IO;

namespace TagKit.Markup
{
    public class Document:Node
    {
        internal static EmptyEnumerator EmptyEnumerator = new EmptyEnumerator();

        #region Overrides of Node

        public override NodeType NodeType { get; }
        /// <summary>
        /// Gets the root XmlElement for the document.
        /// </summary>
        public Element DocumentElement
        {
            get { return (Element)FindChild(NodeType.Element); }
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
