using System;
using FakeItEasy;
using VehicleSummary.Api.Services.ConfigReader;
using VehicleSummary.Api.Services.VehicleSummary;
using Xunit;

namespace VehicleSummary.UnitTests.ServicesTests.VehicleSummaryTests.VehicleSummaryServiceTests
{
    public class Constructor
    {
        [Fact]
        public void Should_not_throw_exception_if_all_required_services_are_provided()
        {
            new VehicleSummaryService(A.Fake<IConfigReaderService>());
        }

        [Fact]
        public void Should_throw_exception_if_null_ConfigReaderService_provided()
        {
            Assert.Throws<ArgumentNullException>(() => new VehicleSummaryService(null));
        }
    }
}