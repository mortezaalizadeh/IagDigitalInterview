using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using VehicleSummary.Api.Services.ConfigReader;
using VehicleSummary.Contracts;

namespace VehicleSummary.Api.Services.VehicleSummary
{
    /// <summary>
    ///     Implementation <seealso cref="IVehicleSummaryService" /> that uses IAG Rest service to retrieve vehicle summary by
    ///     a given make
    /// </summary>
    public class VehicleSummaryService : IVehicleSummaryService
    {
        private readonly IConfigReaderService _configReaderService;
        private readonly ILogger<VehicleSummaryService> _logger;

        /// <summary>
        ///     The constructor
        /// </summary>
        /// <param name="configReaderService">
        ///     Mandatory. Reference to the service where it returns the configuration required by
        ///     the service to operate
        /// </param>
        /// <param name="logger">
        ///     Mandatory. Reference to the logger service that is used to log activities occurring in the
        ///     <seealso cref="VehicleSummaryService" />
        /// </param>
        public VehicleSummaryService(IConfigReaderService configReaderService, ILogger<VehicleSummaryService> logger)
        {
            _configReaderService = configReaderService ?? throw new ArgumentNullException(nameof(configReaderService));
            _logger =
                logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        ///     Returns the vehicle models for the given vehicle make to the caller.
        /// </summary>
        /// <param name="make">Mandatory. The vehicle make to retrieve the models for</param>
        /// <returns>The vehicle models for the given vehicle make</returns>
        public async Task<List<string>> GetModelsByMake(string make)
        {
            var modelsUrl = $"{_configReaderService.GetIagBaseUrl()}/makes/{make}/models";

            _logger.LogInformation(LoggingEvents.IagHttpClient,
                $"Processing request to fetch vehicle {make} models. Url to fetch: {modelsUrl}");

            try
            {
                return await modelsUrl.AddSubscriptionAndApiVersionAndGetJsonAsync(
                    _configReaderService.GetSubscriptionKey());
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.Response.StatusCode == HttpStatusCode.NotFound)
                    throw new NotFoundException($"Vehicle {make} not found to find the models for");

                throw new UnknownException(ex.Message);
            }
        }

        /// <summary>
        ///     Returns the vehicle years for the given vehicle make and model.
        /// </summary>
        /// <param name="make">Mandatory. The vehicle make to retrieve the years for</param>
        /// <param name="model">Mandatory. The vehicle model to retrieve the years for</param>
        /// <returns>The vehicle years for the given vehicle make and model</returns>
        public async Task<List<int>> GetYearsByMakeAndModel(string make, string model)
        {
            var yearsUrl = $"{_configReaderService.GetIagBaseUrl()}/makes/{make}/models/{model}/years";

            _logger.LogInformation(LoggingEvents.IagHttpClient,
                $"Processing request to fetch vehicle {make} - {model} years. Url to fetch: {yearsUrl}");

            try
            {
                return (await yearsUrl.AddSubscriptionAndApiVersionAndGetJsonAsync(
                    _configReaderService.GetSubscriptionKey()))
                    // Returned list of years are in string format, convert them all to list of integers
                    .Select(year => Convert.ToInt32(year)).ToList();
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.Response.StatusCode == HttpStatusCode.NotFound)
                    throw new NotFoundException($"Vehicle {make} - {model} not found to retrieve the years for");

                throw new UnknownException(ex.Message);
            }
        }
    }
}