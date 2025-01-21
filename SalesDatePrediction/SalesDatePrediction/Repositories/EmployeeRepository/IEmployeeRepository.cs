using SalesDatePrediction.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesDatePrediction.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employees>> GetEmployeesAsync(string orderBy, string orderDirection);
    }
}