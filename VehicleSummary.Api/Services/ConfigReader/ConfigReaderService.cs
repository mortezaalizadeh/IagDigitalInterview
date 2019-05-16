using Microsoft.Extensions.Options;

namespace VehicleSummary.Api.Services.ConfigReader
{
    public class ConfigReaderService : IConfigReaderService
    {
        private readonly IOptions<Config> _config;

        public ConfigReaderService(IOptions<Config> config)
        {
            _config = config;
        }

        public string GetIagBaseUrl()
        {
            return _config.Value.IagBaseUrl;
        }

        public string GetSubscriptionKey()
        {
            return _config.Value.SubscriptionKey;
        }
    }
}