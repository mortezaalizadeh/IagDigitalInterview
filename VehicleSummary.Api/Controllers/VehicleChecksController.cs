using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VehicleSummary.Contracts;
using VehicleSummary.Contracts.Models;
using VehicleSummary.Contracts.Responses;

namespace VehicleSummary.Api.Controllers
{
    [Route("api/vehicle-checks")]
    [ApiController]
    public class VehicleChecksController : Controller
    {
        private readonly IVehicleSummaryService _vehicleSummaryService;

        public VehicleChecksController(IVehicleSummaryService vehicleSummaryService)
        {
            _vehicleSummaryService =
                vehicleSummaryService ?? throw new ArgumentNullException(nameof(vehicleSummaryService));
        }

        [HttpGet]
        [Route("makes/{make}")]
        public async Task<IActionResult> Makes(string make)
        {
            var models = await _vehicleSummaryService.GetModelsByMake(make);
            var response = new VehicleSummaryResponse
            {
                Make = make,
                Models = models.Select(async model => new
                    {
                        model,
                        years = await _vehicleSummaryService.GetYearsByMakeAndModel(make, model)
                    })
                    .Select(task => task.Result)
                    .Where(result => result != null)
                    .SelectMany(result => result.years.Select(year => new VehicleSummaryModels
                        {Name = result.model, YearsAvailable = year}))
                    .ToList()
            };

            return Ok(response);
        }
    }
}