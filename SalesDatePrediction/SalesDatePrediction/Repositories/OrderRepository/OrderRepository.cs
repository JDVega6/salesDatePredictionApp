using SalesDatePrediction.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDatePrediction.Repositories.OrderRepository
{
    public class OrderRepository : DbRepository, IOrderRepository
    {


        public async Task<List<PredictedOrder>> GetOrderNextPredictionAsync(string CustomerName, string orderBy, string orderDirection)
        {
            orderBy = string.IsNullOrEmpty(orderBy) ? "CustomerName" : orderBy;  
            orderDirection = string.IsNullOrEmpty(orderDirection) ? "ASC" : orderDirection;  

            string query = $@"
                        WITH OrderDifferences AS (
                            SELECT 
                                o.custid,
                                c.companyname AS CustomerName,
                                o.orderdate,
                                LAG(o.orderdate) OVER (PARTITION BY o.custid ORDER BY o.orderdate) AS PreviousOrderDate
                            FROM 
                                [StoreSample].[Sales].[Orders] o
                            JOIN 
                                [StoreSample].[Sales].[Customers] c
                            ON 
                                o.custid = c.custid
                        ),
                        Averages AS (
                            SELECT 
                                od.custid,
                                od.CustomerName,
                                MAX(od.orderdate) AS LastOrderDate,
                                SUM(DATEDIFF(DAY, od.PreviousOrderDate, od.orderdate)) AS TotalDaysBetweenOrders,
                                COUNT(od.orderdate) AS TotalOrders 
                            FROM 
                                OrderDifferences od
                            WHERE 
                                od.PreviousOrderDate IS NOT NULL 
                            GROUP BY 
                                od.custid, od.CustomerName
                        )
                        SELECT 
                            a.custid,
                            a.CustomerName,
                            a.LastOrderDate,
                            DATEADD(DAY, 
                                    CAST(ISNULL(a.TotalDaysBetweenOrders, 0) AS INT) / NULLIF(a.TotalOrders + 1, 0), 
                                    a.LastOrderDate) AS NextPredictedOrder
                        FROM 
                            Averages a
                        WHERE CustomerName LIKE '%{CustomerName}%'
                        ORDER BY {orderBy} {orderDirection}";

            var predictedOrder = await ExecuteQueryAsync(query, reader =>
            {
                return new PredictedOrder
                {
                    Custid = reader.GetInt32(0),
                    CustomerName = reader.IsDBNull(1) ? null : reader.GetString(1),
                    LastOrderDate = (DateTime)(reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2)),
                    NextPredictedOrder = (DateTime)(reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3)),
                };
            });

            return predictedOrder;
        }

        public bool AddOrder(OrderRequest orderRequest)
        {
            try
            {
                var parameters = new[]
                    {
                        new SqlParameter("@Empid", SqlDbType.Int) { Value = orderRequest.Empid },
                        new SqlParameter("@Custid", SqlDbType.Int) { Value = orderRequest.Custid },
                        new SqlParameter("@Shipperid", SqlDbType.Int) { Value = orderRequest.Shipperid },
                        new SqlParameter("@Shipname", SqlDbType.NVarChar, 255) { Value = orderRequest.Shipname },
                        new SqlParameter("@Shipaddress", SqlDbType.NVarChar, 255) { Value = orderRequest.Shipaddress },
                        new SqlParameter("@Shipcity", SqlDbType.NVarChar, 255) { Value = orderRequest.Shipcity },
                        new SqlParameter("@Orderdate", SqlDbType.DateTime) { Value = orderRequest.Orderdate },
                        new SqlParameter("@Requireddate", SqlDbType.DateTime) { Value = orderRequest.Requireddate },
                        new SqlParameter("@Shippeddate", SqlDbType.DateTime) { Value = (object)orderRequest.Shippeddate ?? DBNull.Value },
                        new SqlParameter("@Freight", SqlDbType.Decimal) { Value = orderRequest.Freight },
                        new SqlParameter("@Shipcountry", SqlDbType.NVarChar, 100) { Value = orderRequest.Shipcountry },
                        new SqlParameter("@Productid", SqlDbType.Int) { Value = orderRequest.Productid },
                        new SqlParameter("@Unitprice", SqlDbType.Decimal) { Value = orderRequest.Unitprice },
                        new SqlParameter("@Qty", SqlDbType.Int) { Value = orderRequest.Qty },
                        new SqlParameter("@Discount", SqlDbType.Decimal) { Value = orderRequest.Discount/100 }
                    };

                ExecuteStoredProcedure<object>("AddNewOrder", reader => null, parameters.ToArray());

                return true; 
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la orden: " + ex.Message);
            }
        }
    }

}