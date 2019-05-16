using System.Collections.Generic;
using VehicleSummary.Contracts.Models;

namespace VehicleSummary.Contracts.Responses
{
    public class VehicleSummaryResponse
    {
        public string Make { get; set; }
        public IList<VehicleSummaryModels> Models { get; set; }
    }
}