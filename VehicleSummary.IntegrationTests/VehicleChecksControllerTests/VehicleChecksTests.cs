using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VehicleSummary.Api;
using VehicleSummary.Contracts.Responses;
using Xunit;

namespace VehicleSummary.IntegrationTests.VehicleChecksControllerTests
{
    public class VehicleChecksTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public VehicleChecksTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        private readonly HttpClient _client;

        [Fact]
        public async Task Should_return_models_and_years_for_a_provided_vehicle_make()
        {
            var httpResponse = await _client.GetAsync("/vehicle-checks/makes/Lotus");

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<VehicleSummaryResponse>(stringResponse);

            Assert.Equal("Lotus", response.Make);
            Assert.NotNull(response.Models);
            Assert.True(response.Models.Any());
        }

        [Fact]
        public async Task Should_return_not_found_if_vehicle_model_does_not_exist()
        {
            var httpResponse = await _client.GetAsync($"/vehicle-checks/makes/{Guid.NewGuid().ToString()}");

            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
        }
    }
}