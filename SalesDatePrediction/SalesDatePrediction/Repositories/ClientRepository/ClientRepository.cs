using SalesDatePrediction.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesDatePrediction.Repositories.ClientRepository
{
    public class ClientRepository : DbRepository, IClientRepository
    {
        public ClientRepository()
        {
        }

        public async Task<List<ClientOrder>> GetClientOrdersAsync(int custId, string orderBy, string orderDirection)
        {

            orderBy = string.IsNullOrEmpty(orderBy) ? "Orderid" : orderBy;
            orderDirection = string.IsNullOrEmpty(orderDirection) ? "ASC" : orderDirection;

            string query = $@"
                SELECT 
                            Orderid,
                            Requireddate,
                            Shippeddate,
                            Shipname,
                            Shipaddress,
                            Shipcity
                FROM [StoreSample].[Sales].[Orders]
                WHERE Custid = {custId}
                ORDER BY {orderBy} {orderDirection};";

            var clientOrder = await ExecuteQueryAsync(query, reader =>
            {
                return new ClientOrder
                {
                    OrderId = reader.GetInt32(0),
                    RequiredDate = reader.IsDBNull(1) ? (DateTime?)null : reader.GetDateTime(1),
                    ShippedDate = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2),
                    ShipName = reader.IsDBNull(3) ? null : reader.GetString(3),
                    ShipAddress = reader.IsDBNull(4) ? null : reader.GetString(4),
                    ShipCity = reader.IsDBNull(5) ? null : reader.GetString(5),
                };
            });

            return clientOrder;
        }


    }
}