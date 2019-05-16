using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;
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

            return await modelsUrl.WithHeader("Ocp-Apim-Subscription-Key", _configReaderService.GetSubscriptionKey())
                .SetQueryParam("api-version", "v1")
                .GetJsonAsync<List<string>>();
        }

        public async Task<List<int>> GetYearsByMakeAndModel(string make, string model)
        {
            var modelsUrl = $"{_configReaderService.GetIagBaseUrl()}/makes/{make}/models/{model}/years";

            return (await modelsUrl
                .WithHeader("Ocp-Apim-Subscription-Key", _configReaderService.GetSubscriptionKey())
                .SetQueryParam("api-version", "v1")
                .GetJsonAsync<List<string>>()).Select(year => Convert.ToInt32(year)).ToList();
        }
    }
}