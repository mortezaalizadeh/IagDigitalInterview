using System;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using VehicleSummary.Api.Controllers;
using VehicleSummary.Contracts;
using Xunit;

namespace VehicleSummary.UnitTests.ControllersTests.VehicleChecksControllerTests
{
    public class Constructor
    {
        [Fact]
        public void Should_not_throw_exception_if_all_required_services_are_provided()
        {
            new VehicleChecksController(A.Fake<IVehicleSummaryService>(), A.Fake<ILogger<VehicleChecksController>>());
        }

        [Fact]
        public void Should_throw_exception_if_null_logger_provided()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new VehicleChecksController(A.Fake<IVehicleSummaryService>(), null));
        }

        [Fact]
        public void Should_throw_exception_if_null_VehicleSummaryService_provided()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new VehicleChecksController(null, A.Fake<ILogger<VehicleChecksController>>()));
        }
    }
}