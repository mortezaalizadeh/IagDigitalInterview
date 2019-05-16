using System;

namespace VehicleSummary.Api.Services.VehicleSummary
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}