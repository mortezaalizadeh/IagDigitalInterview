namespace VehicleSummary.Api.Services.ConfigReader
{
    /// <summary>
    ///     Wraps the configurations that are required by different services
    /// </summary>
    public class Config
    {
        /// <summary>
        ///     The Iag Rest API base endpoint
        /// </summary>
        public string IagBaseUrl { get; set; }

        /// <summary>
        ///     The subscription key required by the IAG http client to include in request header messages
        /// </summary>
        public string SubscriptionKey { get; set; }
    }
}