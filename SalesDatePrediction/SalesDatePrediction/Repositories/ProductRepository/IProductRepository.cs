using SalesDatePrediction.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesDatePrediction.Repositories.ProductRepository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductAsync(string orderBy, string orderDirection);
    }
}