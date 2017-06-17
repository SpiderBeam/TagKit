using System;
using System.Collections.Generic;
using System.Linq;
using TagKit.Documents;
using TagKit.Foundation.Common;

namespace TagKit.Configuration
{
    /// <summary>
    /// A set of useful extensions for Configuration (or derived) objects.
    /// </summary>
    public static class ConfigurationExtensions
    {
        #region General

        /// <summary>
        /// Returns a new configuration that includes the given service.
        /// </summary>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="service">The service to register.</param>
        /// <returns>The new instance with the service.</returns>
        public static IConfiguration With(this IConfiguration configuration, Object service)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (service == null)
                throw new ArgumentNullException(nameof(service));

            return new Configuration(configuration.Services.Concat(service));
        }

        /// <summary>
        /// Returns a new configuration that includes only the given service,
        /// excluding other instances or instance creators for the same service.
        /// </summary>
        /// <typeparam name="TService">The service to include exclusively.</typeparam>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="service">The service to include.</param>
        /// <returns>The new instance with only the given service.</returns>
        public static IConfiguration WithOnly<TService>(this IConfiguration configuration, TService service)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            return configuration.Without<TService>().With(service);
        }

        /// <summary>
        /// Returns a new configuration that includes only the given service
        /// creator, excluding other instances or instance creators for the same
        /// service.
        /// </summary>
        /// <typeparam name="TService">The service to include exclusively.</typeparam>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="creator">The service creator to include.</param>
        /// <returns>The new instance with only the given service.</returns>
        public static IConfiguration WithOnly<TService>(this IConfiguration configuration, Func<IBrowsingContext, TService> creator)
        {
            if (creator == null)
                throw new ArgumentNullException(nameof(creator));

            return configuration.Without<TService>().With(creator);
        }

        /// <summary>
        /// Returns a new configuration that excludes the given service.
        /// </summary>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="service">The service to unregister.</param>
        /// <returns>The new instance without the service.</returns>
        public static IConfiguration Without(this IConfiguration configuration, Object service)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (service == null)
                throw new ArgumentNullException(nameof(service));

            return new Configuration(configuration.Services.Except(service));
        }

        /// <summary>
        /// Returns a new configuration that includes the given services.
        /// </summary>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="services">The services to register.</param>
        /// <returns>The new instance with the services.</returns>
        public static IConfiguration With(this IConfiguration configuration, IEnumerable<Object> services)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (services == null)
                throw new ArgumentNullException(nameof(services));

            return new Configuration(services.Concat(configuration.Services));
        }

        /// <summary>
        /// Returns a new configuration that excludes the given services.
        /// </summary>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="services">The services to unregister.</param>
        /// <returns>The new instance without the services.</returns>
        public static IConfiguration Without(this IConfiguration configuration, IEnumerable<Object> services)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (services == null)
                throw new ArgumentNullException(nameof(services));

            return new Configuration(configuration.Services.Except(services));
        }

        /// <summary>
        /// Returns a new configuration that includes the given service creator.
        /// </summary>
        /// <typeparam name="TService">The type of service to create.</typeparam>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="creator">The creator to register.</param>
        /// <returns>The new instance with the services.</returns>
        public static IConfiguration With<TService>(this IConfiguration configuration, Func<IBrowsingContext, TService> creator)
        {
            if (creator == null)
                throw new ArgumentNullException(nameof(creator));

            return configuration.With((Object)creator);
        }

        /// <summary>
        /// Returns a new configuration that excludes the given service creator.
        /// </summary>
        /// <typeparam name="TService">The type of service to remove.</typeparam>
        /// <param name="configuration">The configuration to extend.</param>
        /// <returns>The new instance without the services.</returns>
        public static IConfiguration Without<TService>(this IConfiguration configuration)
        {
            var items = configuration.Services.OfType<TService>();
            var creators = configuration.Services.OfType<Func<IBrowsingContext, TService>>();
            return configuration.Without(items).Without(creators);
        }

        /// <summary>
        /// Checks if the configuration holds any references to the given service.
        /// </summary>
        /// <typeparam name="TService">The type of service to check for.</typeparam>
        /// <param name="configuration">The configuration to examine.</param>
        /// <returns>True if any service / creators are found, otherwise false.</returns>
        public static Boolean Has<TService>(this IConfiguration configuration)
        {
            return configuration.Services.OfType<TService>().Any() || configuration.Services.OfType<Func<IBrowsingContext, TService>>().Any();
        }

        #endregion

    }
}