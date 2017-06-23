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
    }
}
