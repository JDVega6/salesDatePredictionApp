using SalesDatePrediction.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesDatePrediction.Repositories.OrderRepository
{
    public interface IOrderRepository
    { 
        Task<List<PredictedOrder>> GetOrderNextPredictionAsync(string CustomerName,string orderBy, string orderDirection);

        bool AddOrder(OrderRequest orderRequest);
    }
}