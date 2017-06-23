namespace TagKit.Markup
{
    public interface ITagLineInfo
    {
        bool HasLineInfo();
        int LineNumber { get; }
        int LinePosition { get; }
    }
}
