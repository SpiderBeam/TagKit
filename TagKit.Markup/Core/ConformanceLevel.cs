namespace TagKit.Markup
{
    public enum ConformanceLevel
    {
        /// <summary>
        /// With conformance level Auto an Reader or Writer automatically determines whether in incoming markup is an markup fragment or document.
        /// </summary>
        Auto = 0,

        /// <summary>
        /// Conformance level for markup fragment. An markup fragment can contain any node type that can be a child of an element,
        /// plus it can have a single markup declaration as its first node
        /// </summary>
        Fragment = 1,

        // Conformance level for markup document as specified in XML 1.0 Specification
        Document = 2,
    }
}
