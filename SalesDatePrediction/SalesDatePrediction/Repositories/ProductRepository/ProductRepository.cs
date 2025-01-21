using SalesDatePrediction.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesDatePrediction.Repositories.ProductRepository
{
    public class ProductRepository : DbRepository,  IProductRepository
    {
        public async Task<List<Product>> GetProductAsync(string orderBy, string orderDirection)
        {
            if (orderBy != "Productid" && orderBy != "Productname")
            {
                throw new ArgumentException("Valor de 'orderBy' no válido. Use 'Productid' o 'Productname'.");
            }

            if (orderDirection != "ASC" && orderDirection != "DESC")
            {
                throw new ArgumentException("Valor de 'orderDirection' no válido. Use 'ASC' o 'DESC'.");
            }


            string query = $@"
                SELECT 
                    Productid,
                    Productname
                FROM [StoreSample].[Production].[Products]
                ORDER BY {orderBy} {orderDirection};";

            var products = await ExecuteQueryAsync(query, reader =>
            {
                return new Product
                {
                    ProductId = reader.GetInt32(0),
                    ProductName = reader.GetString(1)
                };
            });

            return products;
        }


    }


}