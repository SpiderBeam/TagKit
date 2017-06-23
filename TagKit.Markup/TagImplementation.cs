using System;

namespace TagKit.Markup
{
    /// <summary>
    /// Provides methods for performing operations that are independent of any
    /// particular instance of the document object model.
    /// </summary>
    public class TagImplementation
    {
        private TagNameTable _nameTable;

        public TagImplementation() : this(new NameTable())
        {
        }

        public TagImplementation(TagNameTable nt)
        {
            _nameTable = nt;
        }

        internal TagNameTable NameTable
        {
            get { return _nameTable; }
        }
        /// <summary>
        /// Creates a new Document. All documents created from the same 
        /// TagImplementation object share the same name table.
        /// </summary>
        /// <returns></returns>
        public virtual Document CreateDocument()
        {
            return new Document(this);
        }
        /// <summary>
        /// Test if the DOM implementation implements a specific feature.
        /// </summary>
        /// <param name="strFeature"></param>
        /// <param name="strVersion"></param>
        /// <returns></returns>
        public bool HasFeature(string strFeature, string strVersion)
        {
            if (String.Equals("XML", strFeature, StringComparison.OrdinalIgnoreCase))
            {
                if (strVersion == null || strVersion == "1.0" || strVersion == "2.0")
                    return true;
            }
            return false;
        }
    }
}
