using System.Collections.Generic;
using System.Threading.Tasks;

namespace VehicleSummary.Contracts
{
    public interface IVehicleSummaryService
    {
        Task<List<string>> GetModelsByMake(string make);
        Task<List<int>> GetYearsByMakeAndModel(string make, string model);
    }
}