using System;

namespace TagKit.Markup
{
    /// <summary>
    /// Resolves external Tag resources named by a Uniform Resource Identifier (URI).
    /// </summary>
    public partial class TagUrlResolver : TagResolver
    {
        #region Overrides of TagResolver

        public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
