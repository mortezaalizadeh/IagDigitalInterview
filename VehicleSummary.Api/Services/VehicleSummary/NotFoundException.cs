using System;

namespace VehicleSummary.Api.Services.VehicleSummary
{
    /// <summary>
    /// Will be thrown by the IAG http client class if Rest service returns 404 - Not Found error
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="message">The message to include in the exception</param>
        public NotFoundException(string message) : base(message)
        {
        }
    }
}