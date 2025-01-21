using SalesDatePrediction.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesDatePrediction.Repositories.ShipperRepository
{
    public interface IShipperRepository
    {
        Task<List<Shipper>> GetShipperAsync(string orderBy, string orderDirection);
    }
}