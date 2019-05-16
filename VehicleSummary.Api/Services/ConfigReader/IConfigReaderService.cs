namespace VehicleSummary.Api.Services.ConfigReader
{
    public interface IConfigReaderService
    {
        string GetIagBaseUrl();
        string GetSubscriptionKey();
    }
}