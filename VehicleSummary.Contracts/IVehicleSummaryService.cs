using System.Collections.Generic;
using System.Threading.Tasks;

namespace VehicleSummary.Contracts
{
    /// <summary>
    ///     Declares the interface that defines how to retrieve vehicle summary by a given make
    /// </summary>
    public interface IVehicleSummaryService
    {
        /// <summary>
        ///     Returns the vehicle models for the given vehicle make to the caller.
        /// </summary>
        /// <param name="make">Mandatory. The vehicle make to retrieve the models for</param>
        /// <returns>The vehicle models for the given vehicle make</returns>
        Task<List<string>> GetModelsByMake(string make);

        /// <summary>
        ///     Returns the vehicle years for the given vehicle make and model.
        /// </summary>
        /// <param name="make">Mandatory. The vehicle make to retrieve the years for</param>
        /// <param name="model">Mandatory. The vehicle model to retrieve the years for</param>
        /// <returns>The vehicle years for the given vehicle make and model</returns>
        Task<List<int>> GetYearsByMakeAndModel(string make, string model);
    }
}