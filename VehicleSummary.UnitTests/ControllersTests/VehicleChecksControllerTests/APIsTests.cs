using System;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using VehicleSummary.Api.Controllers;
using VehicleSummary.Contracts;
using VehicleSummary.Contracts.Models;
using VehicleSummary.Contracts.Responses;
using Xunit;

namespace VehicleSummary.UnitTests.ControllersTests.VehicleChecksControllerTests
{
    public class APIsTests
    {
        public APIsTests()
        {
            _mockedVehicleSummaryService = A.Fake<IVehicleSummaryService>();
            _controller = new VehicleChecksController(_mockedVehicleSummaryService);
        }

        private readonly VehicleChecksController _controller;
        private readonly IVehicleSummaryService _mockedVehicleSummaryService;

        [Fact]
        public async Task Call_GetModelsByMake_with_given_make()
        {
            var random = new Random();
            var make = Guid.NewGuid().ToString();
            var models = Enumerable.Range(0, random.Next(1, 10)).Select(idx => Guid.NewGuid().ToString()).ToList();
            var yearsByModel = models.Select(model => new VehicleSummaryModels
                {Name = model, YearsAvailable = random.Next(1, 100)}).ToList();

            A.CallTo(() => _mockedVehicleSummaryService.GetModelsByMake(A<string>.Ignored))
                .Returns(models);

            models.ForEach(model => A
                .CallTo(() => _mockedVehicleSummaryService.GetYearsByMakeAndModel(A<string>.Ignored, A<string>.Ignored))
                .Returns(yearsByModel.Where(item => item.Name == model).Select(item => item.YearsAvailable).ToList()));

            await _controller.Makes(make);

            A.CallTo(() => _mockedVehicleSummaryService.GetModelsByMake(make))
                .MustHaveHappened();
        }

        [Fact]
        public async Task Call_GetYearsByMakeAndModel_with_given_make_and_model()
        {
            var random = new Random();
            var make = Guid.NewGuid().ToString();
            var models = Enumerable.Range(0, random.Next(1, 10)).Select(idx => Guid.NewGuid().ToString()).ToList();
            var yearsByModel = models.Select(model => new VehicleSummaryModels
                {Name = model, YearsAvailable = random.Next(1, 100)}).ToList();

            A.CallTo(() => _mockedVehicleSummaryService.GetModelsByMake(A<string>.Ignored))
                .Returns(models);

            models.ForEach(model => A
                .CallTo(() => _mockedVehicleSummaryService.GetYearsByMakeAndModel(A<string>.Ignored, A<string>.Ignored))
                .Returns(yearsByModel.Where(item => item.Name == model).Select(item => item.YearsAvailable).ToList()));

            await _controller.Makes(make);
            models.ForEach(model =>
                A.CallTo(() => _mockedVehicleSummaryService.GetYearsByMakeAndModel(make, model)).MustHaveHappened());
        }

        [Fact]
        public async Task Call_VehicleSummaryService_should_return_result_returned_by_vehicle_summary_service()
        {
            var random = new Random();
            var make = Guid.NewGuid().ToString();
            var models = Enumerable.Range(0, random.Next(1, 10)).Select(idx => Guid.NewGuid().ToString()).ToList();
            var yearsByModel = models.Select(model => new VehicleSummaryModels
                {Name = model, YearsAvailable = random.Next(1, 100)}).ToList();

            A.CallTo(() => _mockedVehicleSummaryService.GetModelsByMake(A<string>.Ignored))
                .Returns(models);

            models.ForEach(model => A
                .CallTo(() => _mockedVehicleSummaryService.GetYearsByMakeAndModel(make, model))
                .Returns(yearsByModel.Where(item => item.Name == model).Select(item => item.YearsAvailable).ToList()));

            var response = await _controller.Makes(make);

            Assert.NotNull(response);

            var okResult = response as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var vehicleSummaryResponse = okResult.Value as VehicleSummaryResponse;

            Assert.NotNull(vehicleSummaryResponse);
            Assert.Equal(make, vehicleSummaryResponse.Make);
            Assert.Equal(yearsByModel.Count, yearsByModel.Count(expectedModel =>
                vehicleSummaryResponse.Models.Any(model =>
                    expectedModel.Name == model.Name && expectedModel.YearsAvailable == model.YearsAvailable)));
        }

        [Fact]
        public async Task Call_VehicleSummaryService_should_throw_exception_if_GetModelsByMake_throws_exception()
        {
            A.CallTo(() => _mockedVehicleSummaryService.GetModelsByMake(A<string>.Ignored))
                .ThrowsAsync(new Exception());

            Assert.ThrowsAsync<Exception>(() => _controller.Makes(Guid.NewGuid().ToString()));
        }

        [Fact]
        public async Task Call_VehicleSummaryService_should_throw_exception_if_GetYearsByMakeAndModel_throws_exception()
        {
            var random = new Random();
            var make = Guid.NewGuid().ToString();
            var models = Enumerable.Range(0, random.Next(1, 10)).Select(idx => Guid.NewGuid().ToString()).ToList();

            A.CallTo(() => _mockedVehicleSummaryService.GetModelsByMake(A<string>.Ignored))
                .Returns(models);

            A.CallTo(() => _mockedVehicleSummaryService.GetYearsByMakeAndModel(A<string>.Ignored, A<string>.Ignored))
                .ThrowsAsync(new Exception());

            Assert.ThrowsAsync<Exception>(() => _controller.Makes(Guid.NewGuid().ToString()));
        }
    }
}