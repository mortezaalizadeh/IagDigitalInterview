using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using VehicleSummary.Api.Services.ConfigReader;
using VehicleSummary.Contracts;

namespace VehicleSummary.Api.Services.VehicleSummary
{
    public class VehicleSummaryService : IVehicleSummaryService
    {
        private readonly IConfigReaderService _configReaderService;
        private readonly ILogger<VehicleSummaryService> _logger;


        public VehicleSummaryService(IConfigReaderService configReaderService, ILogger<VehicleSummaryService> logger)
        {
            _configReaderService = configReaderService ?? throw new ArgumentNullException(nameof(configReaderService));
            _logger =
                logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<string>> GetModelsByMake(string make)
        {
            var modelsUrl = $"{_configReaderService.GetIagBaseUrl()}/makes/{make}/models";

            _logger.LogInformation(LoggingEvents.IagHttpClient,
                $"Processing request to fetch vehicle {make} models. Url to fetch: {modelsUrl}");

            return await modelsUrl.AddSubscriptionAndApiVersionAndGetJsonAsync(
                _configReaderService.GetSubscriptionKey());
        }

        public async Task<List<int>> GetYearsByMakeAndModel(string make, string model)
        {
            var yearsUrl = $"{_configReaderService.GetIagBaseUrl()}/makes/{make}/models/{model}/years";

            _logger.LogInformation(LoggingEvents.IagHttpClient,
                $"Processing request to fetch vehicle {make} - {model} years. Url to fetch: {yearsUrl}");

            return (await yearsUrl.AddSubscriptionAndApiVersionAndGetJsonAsync(
                _configReaderService.GetSubscriptionKey())).Select(year => Convert.ToInt32(year)).ToList();
        }
    }
}