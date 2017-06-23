namespace TagKit.Markup
{
    /// <summary>
    /// An enumeration for the xml:space scope used in TagReader and TagWriter.
    /// </summary>
    public enum TagSpace
    {
        // xml:space scope has not been specified.
        None = 0,

        // The xml:space scope is "default".
        Default = 1,

        // The xml:space scope is "preserve".
        Preserve = 2
    }
}
