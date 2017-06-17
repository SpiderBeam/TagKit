using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagKit.Configuration.Services;
using TagKit.Documents;
using TagKit.Foundation.Documents;

namespace TagKit.Configuration
{
    /// <summary>
    /// Represents a helper to construct objects with externally defined
    /// classes and libraries.
    /// </summary>
    static class ConfigurationExtensions
    {
        #region Services

        public static TFactory GetFactory<TFactory>(this IConfiguration configuration)
        {
            return configuration.GetServices<TFactory>().Single();
        }

        public static TProvider GetProvider<TProvider>(this IConfiguration configuration)
        {
            return configuration.GetServices<TProvider>().SingleOrDefault();
        }

        public static TService GetService<TService>(this IConfiguration configuration)
        {
            return configuration.GetServices<TService>().FirstOrDefault();
        }

        public static IEnumerable<TService> GetServices<TService>(this IConfiguration configuration)
        {
            return configuration.Services.OfType<TService>();
        }

        //public static IResourceService<TResource> GetResourceService<TResource>(this IConfiguration configuration, String type)
        //    where TResource : IResourceInfo
        //{
        //    var services = configuration.GetServices<IResourceService<TResource>>();

        //    foreach (var service in services)
        //    {
        //        if (service.SupportsType(type))
        //        {
        //            return service;
        //        }
        //    }

        //    return default(IResourceService<TResource>);
        //}

        #endregion
        #region Context

        public static IBrowsingContext NewContext(this IConfiguration configuration, Sandboxes security = Sandboxes.None)
        {
            var factory = configuration.GetFactory<IContextFactory>();
            return factory.Create(configuration, security);
        }

        public static IBrowsingContext FindContext(this IConfiguration configuration, String name)
        {
            var factory = configuration.GetFactory<IContextFactory>();
            return factory.Find(name);
        }

        #endregion

    }
}
