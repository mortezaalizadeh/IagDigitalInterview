using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VehicleSummary.Api.Services.VehicleSummary;
using VehicleSummary.Contracts;
using VehicleSummary.Contracts.Models;
using VehicleSummary.Contracts.Responses;

namespace VehicleSummary.Api.Controllers
{
    [Route("vehicle-checks")]
    [ApiController]
    public class VehicleChecksController : Controller
    {
        private readonly ILogger<VehicleChecksController> _logger;
        private readonly IVehicleSummaryService _vehicleSummaryService;

        public VehicleChecksController(IVehicleSummaryService vehicleSummaryService,
            ILogger<VehicleChecksController> logger)
        {
            _vehicleSummaryService =
                vehicleSummaryService ?? throw new ArgumentNullException(nameof(vehicleSummaryService));
            _logger =
                logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("makes/{make}")]
        public async Task<IActionResult> GetVehicleSummaryByMake(string make)
        {
            _logger.LogInformation(LoggingEvents.Api, $"Received request to return summary for the vehicle {make}");

            try
            {
                _logger.LogInformation(LoggingEvents.Api, $"Start fetching vehicle {make} models...");

                var models = await _vehicleSummaryService.GetModelsByMake(make);

                _logger.LogInformation(LoggingEvents.Api, $"Successfully finished fetching vehicle {make} models.");

                var response = new VehicleSummaryResponse
                {
                    Make = make,
                    Models = models.Select(async model =>
                        {
                            _logger.LogInformation(LoggingEvents.Api,
                                $"Start fetching vehicle {make} - {model} years...");

                            var years = await _vehicleSummaryService.GetYearsByMakeAndModel(make, model);

                            _logger.LogInformation(LoggingEvents.Api,
                                $"Successfully finished fetching vehicle {make} - {model} years");

                            return new
                            {
                                model,
                                years
                            };
                        })
                        .Select(task => task.Result)
                        .Where(result => result != null)
                        .SelectMany(result => result.years.Select(year => new VehicleSummaryModels
                            {Name = result.model, YearsAvailable = year}))
                        .ToList()
                };

                _logger.LogInformation(LoggingEvents.Api,
                    $"Successfully processed the request to return summary for the vehicle {make}");
                _logger.LogDebug(LoggingEvents.Api, $"Vehicle {make} summary {JsonConvert.SerializeObject(response)}");

                return Ok(response);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}