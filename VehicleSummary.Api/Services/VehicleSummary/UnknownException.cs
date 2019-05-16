using System;

namespace VehicleSummary.Api.Services.VehicleSummary
{
    public class UnknownException : Exception
    {
        public UnknownException(string message) : base(message)
        {
        }
    }
}