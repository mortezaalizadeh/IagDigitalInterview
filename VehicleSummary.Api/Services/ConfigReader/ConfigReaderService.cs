namespace VehicleSummary.Api.Services.ConfigReader
{
    public class ConfigReaderService : IConfigReaderService
    {
        public string GetIagBaseUrl()
        {
            return "https://api.iag.co.nz/vehicles/vehicletypes";
        }

        public string GetSubscriptionKey()
        {
            return "72ec78fb999e43be8dbdac94d7236cae";
        }
    }
}