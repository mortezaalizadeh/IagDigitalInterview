namespace VehicleSummary.Api.Services.ConfigReader
{
    /// <summary>
    ///     Provides configurations that are required by the different services
    /// </summary>
    public interface IConfigReaderService
    {
        /// <summary>
        ///     Returns the Iag Rest API base endpoint
        /// </summary>
        /// <returns>The Iag Rest API base endpoint</returns>
        string GetIagBaseUrl();

        /// <summary>
        ///     Returns the subscription key required by the IAG http client to include in request header messages
        /// </summary>
        /// <returns>The subscription key required by the IAG http client to include in request header messages</returns>
        string GetSubscriptionKey();
    }
}