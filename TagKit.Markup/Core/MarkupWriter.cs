using System;

namespace TagKit.Markup 
{
    /// <summary>
    /// Represents a writer that provides fast non-cached forward-only way of generating XML streams containing XML documents 
    /// that conform to the W3C Extensible Markup Language (XML) 1.0 specification and the Namespaces in XML specification.
    /// </summary>
    public abstract partial class MarkupWriter : IDisposable
    {
        #region Implementation of IDisposable

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
