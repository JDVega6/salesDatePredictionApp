using SalesDatePrediction.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesDatePrediction.Repositories.ClientRepository
{
    public interface IClientRepository
    {
        Task<List<ClientOrder>> GetClientOrdersAsync(int custId, string orderBy, string orderDirection);
    }
}