using System;
using Microsoft.Extensions.Options;

namespace VehicleSummary.Api.Services.ConfigReader
{
    /// <summary>
    ///     Provides configurations that are required by the different services
    /// </summary>
    public class ConfigReaderService : IConfigReaderService
    {
        private readonly IOptions<Config> _config;

        /// <summary>
        ///     The constructor
        /// </summary>
        /// <param name="config">
        ///     Mandatory. The config class will contains the required configurations read from the appsettings
        ///     file.
        /// </param>
        public ConfigReaderService(IOptions<Config> config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        ///     Returns the Iag Rest API base endpoint
        /// </summary>
        /// <returns>The Iag Rest API base endpoint</returns>
        public string GetIagBaseUrl()
        {
            return _config.Value.IagBaseUrl;
        }

        /// <summary>
        ///     Returns the subscription key required by the IAG http client to include in request header messages
        /// </summary>
        /// <returns>The subscription key required by the IAG http client to include in request header messages</returns>
        public string GetSubscriptionKey()
        {
            return _config.Value.SubscriptionKey;
        }
    }
}