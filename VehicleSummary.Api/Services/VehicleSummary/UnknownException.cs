using System;

namespace VehicleSummary.Api.Services.VehicleSummary
{
    /// <summary>
    ///     Will be thrown by the IAG http client class if Rest service returns an unknown error
    /// </summary>
    public class UnknownException : Exception
    {
        /// <summary>
        ///     The constructor
        /// </summary>
        /// <param name="message">The message to include in the exception</param>
        public UnknownException(string message) : base(message)
        {
        }
    }
}