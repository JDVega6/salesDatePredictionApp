using SalesDatePrediction.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesDatePrediction.Repositories.ShipperRepository
{
    public class ShipperRepository : DbRepository, IShipperRepository
    {
        public async Task<List<Shipper>> GetShipperAsync(string orderBy, string orderDirection)
        {

            orderBy = string.IsNullOrEmpty(orderBy) ? "Shipperid" : orderBy;  
            orderDirection = string.IsNullOrEmpty(orderDirection) ? "ASC" : orderDirection; 

            string query = $@"
                SELECT 
                    Shipperid,
                    CompanyName
                FROM [StoreSample].[Sales].[Shippers]
                ORDER BY {orderBy} {orderDirection};";

            var shippers = await ExecuteQueryAsync(query, reader =>
            {
                return new Shipper
                {
                    ShipperId = reader.GetInt32(0),
                    CompanyName = reader.GetString(1)
                };
            });

            return shippers;
        }

    }
}