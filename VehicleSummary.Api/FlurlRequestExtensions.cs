using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http;

namespace VehicleSummary.Api
{
    public static class FlurlRequestExtensions
    {
        public static Task<List<string>> AddSubscriptionAndApiVersionAndGetJsonAsync(this string request,
            string subscriptionKey)
        {
            return request.WithHeader("Ocp-Apim-Subscription-Key", subscriptionKey)
                .SetQueryParam("api-version", "v1")
                .GetJsonAsync<List<string>>();
        }
    }
}