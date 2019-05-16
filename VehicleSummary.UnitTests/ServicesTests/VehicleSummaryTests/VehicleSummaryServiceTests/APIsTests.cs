using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Flurl.Http.Testing;
using Newtonsoft.Json;
using VehicleSummary.Api.Services.ConfigReader;
using VehicleSummary.Api.Services.VehicleSummary;
using Xunit;
using Xunit.Abstractions;

namespace VehicleSummary.UnitTests.ServicesTests.VehicleSummaryTests.VehicleSummaryServiceTests
{
    public class APIsTests
    {
        public APIsTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _mockedConfigReaderService = A.Fake<IConfigReaderService>();

            A.CallTo(() => _mockedConfigReaderService.GetIagBaseUrl()).Returns(_iagBaseUrl);
            A.CallTo(() => _mockedConfigReaderService.GetSubscriptionKey()).Returns(_subscriptionKey);

            _service = new VehicleSummaryService(_mockedConfigReaderService);
        }

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IConfigReaderService _mockedConfigReaderService;
        private readonly VehicleSummaryService _service;
        private readonly string _iagBaseUrl = "http://localhost/api";
        private readonly string _subscriptionKey = Guid.NewGuid().ToString();
        private readonly string _make = Guid.NewGuid().ToString();
        private readonly string _model = Guid.NewGuid().ToString();

        [Fact]
        public async Task GetModelsByMake_should_call_IAG_Http_Endpoint()
        {
            var random = new Random();
            var models = Enumerable.Range(0, random.Next(1, 10)).Select(idx => Guid.NewGuid().ToString()).ToList();

            List<string> response;

            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWithJson(models);

                response = await _service.GetModelsByMake(_make);

                httpTest.ShouldHaveCalled($"{_iagBaseUrl}/makes/{_make}/models")
                    .WithHeader("Ocp-Apim-Subscription-Key", _subscriptionKey).WithQueryParam("api-version");
            }

            response.Should().BeEquivalentTo(models);
            _testOutputHelper.WriteLine(JsonConvert.SerializeObject(response));
        }

        [Fact]
        public async Task GetYearsByMakeAndModel_should_call_IAG_Http_Endpoint()
        {
            var random = new Random();
            var years = Enumerable.Range(0, random.Next(1, 10)).Select(idx => random.Next(1, 100)).ToList();

            List<int> response;

            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWithJson(years);

                response = await _service.GetYearsByMakeAndModel(_make, _model);

                httpTest.ShouldHaveCalled($"{_iagBaseUrl}/makes/{_make}/models/{_model}/years")
                    .WithHeader("Ocp-Apim-Subscription-Key", _subscriptionKey).WithQueryParam("api-version");
            }

            response.Should().BeEquivalentTo(years);
            _testOutputHelper.WriteLine(JsonConvert.SerializeObject(response));
        }
    }
}