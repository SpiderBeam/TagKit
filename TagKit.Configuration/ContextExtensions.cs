using TagKit.Configuration.Services;
using TagKit.Documents;

namespace TagKit.Configuration
{
    /// <summary>
    /// Useful methods for browsing contexts.
    /// </summary>
    static class ContextExtensions
    {
        public static TService CreateService<TService>(this IBrowsingContext context)
        {
            var factory = context.Configuration.GetFactory<IServiceFactory>();
            return factory.Create<TService>(context);
        }
    }
}
