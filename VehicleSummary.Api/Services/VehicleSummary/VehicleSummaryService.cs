using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleSummary.Api.Services.ConfigReader;
using VehicleSummary.Contracts;

namespace VehicleSummary.Api.Services.VehicleSummary
{
    public class VehicleSummaryService : IVehicleSummaryService
    {
        private readonly IConfigReaderService _configReaderService;

        public VehicleSummaryService(IConfigReaderService configReaderService)
        {
            _configReaderService = configReaderService ?? throw new ArgumentNullException(nameof(configReaderService));
        }

        public async Task<List<string>> GetModelsByMake(string make)
        {
            var modelsUrl = $"{_configReaderService.GetIagBaseUrl()}/makes/{make}/models";

            return await modelsUrl.AddSubscriptionAndApiVersionAndGetJsonAsync(
                _configReaderService.GetSubscriptionKey());
        }

        public async Task<List<int>> GetYearsByMakeAndModel(string make, string model)
        {
            var modelsUrl = $"{_configReaderService.GetIagBaseUrl()}/makes/{make}/models/{model}/years";

            return (await modelsUrl.AddSubscriptionAndApiVersionAndGetJsonAsync(
                _configReaderService.GetSubscriptionKey())).Select(year => Convert.ToInt32(year)).ToList();
        }
    }
}