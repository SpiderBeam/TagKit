namespace TagKit.Markup
{
    public interface ILineInfo
    {
        bool HasLineInfo();
        int LineNumber { get; }
        int LinePosition { get; }
    }
}
