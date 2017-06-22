namespace TagKit.Markup
{
    /// <summary>
    /// This is a thread-safe hash table which maps string keys to values of type TValue.  It is assumed that the string key is embedded in the hashed value
    /// and can be extracted via a call to ExtractKeyDelegate (in order to save space and allow cleanup of key if value is released due to a WeakReference
    /// TValue releasing its target).
    /// </summary>
    internal sealed class Hashtable<TValue>
    {
    }
}
