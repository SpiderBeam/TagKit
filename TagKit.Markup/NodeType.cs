namespace TagKit.Markup
{
    /// <summary>
    /// Specifies the type of node.
    /// </summary>
    public enum NodeType
    {
        None,
        Element,
        Attribute,
        Text,
        CDATA,
        EntityReference,
        Entity,
        ProcessingInstruction,
        Comment,
        Document,
        DocumentType,
        Notation,
        Whitespace,
        SignificantWhitespace,
        EndElement,
        EndEntity,
        XmlDeclaration
    }
}
