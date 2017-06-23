using System;

namespace TagKit.Markup
{
    /// <summary>
    /// Resolves external Tag resources named by a Uniform
    ///    Resource Identifier (URI).
    /// </summary>
    public abstract partial class TagResolver
    {
        /// <summary>
        /// URI to an Object containing the actual resource.
        /// </summary>
        /// <param name="absoluteUri"></param>
        /// <param name="role"></param>
        /// <param name="ofObjectToReturn"></param>
        /// <returns></returns>
        public abstract Object GetEntity(Uri absoluteUri,
                                 string role,
                                 Type ofObjectToReturn);

        public virtual Uri ResolveUri(Uri baseUri, string relativeUri)
        {
            if (baseUri == null || (!baseUri.IsAbsoluteUri && baseUri.OriginalString.Length == 0))
            {
                return new Uri(relativeUri, UriKind.RelativeOrAbsolute);
            }
            else
            {
                if (relativeUri == null || relativeUri.Length == 0)
                {
                    return baseUri;
                }
                // relative base Uri
                if (!baseUri.IsAbsoluteUri)
                {
                    // create temporary base for the relative URIs
                    Uri tmpBaseUri = new Uri("tmp:///");

                    // create absolute base URI with the temporary base
                    Uri absBaseUri = new Uri(tmpBaseUri, baseUri.OriginalString);

                    // resolve the relative Uri into a new absolute URI
                    Uri resolvedAbsUri = new Uri(absBaseUri, relativeUri);

                    // make it relative by removing temporary base
                    Uri resolvedRelUri = tmpBaseUri.MakeRelativeUri(resolvedAbsUri);

                    return resolvedRelUri;
                }
                return new Uri(baseUri, relativeUri);
            }
        }
    }
}
