using SalesDatePrediction.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesDatePrediction.Repositories
{
    public class EmployeeRepository : DbRepository, IEmployeeRepository
    {
        public EmployeeRepository()
        {
        }

        public async Task<List<Employees>> GetEmployeesAsync(string orderBy, string orderDirection)
        {
            orderBy = string.IsNullOrEmpty(orderBy) ? "Empid" : orderBy;
            orderDirection = string.IsNullOrEmpty(orderDirection) ? "ASC" : orderDirection;

            string query = $@"
                SELECT 
                     Empid, 
                     CONCAT(FirstName, ' ', LastName) AS FullName
                FROM [StoreSample].[HR].[Employees] 
                ORDER BY {orderBy} {orderDirection};";

            List<Employees> employees = await ExecuteQueryAsync(query, reader =>
            {
                return new Employees
                {
                    Empid = reader.GetInt32(0),
                    FullName = reader.GetString(1)
                };
            });

            return employees;
        }
    }
}