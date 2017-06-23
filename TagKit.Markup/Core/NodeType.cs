namespace TagKit.Markup
{
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
        DocumentFragment,
        Notation,
        Whitespace,
        SignificantWhitespace,
        EndElement,
        EndEntity,
        XmlDeclaration
    }
}
