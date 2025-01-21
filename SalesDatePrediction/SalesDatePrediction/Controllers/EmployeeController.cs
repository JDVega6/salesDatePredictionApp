using SalesDatePrediction.Models;
using SalesDatePrediction.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SalesDatePrediction.Controllers
{
    [RoutePrefix("api/employees")]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeRepository _employee;
        public EmployeeController()
        {
            _employee = new EmployeeRepository();
        }


        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetEmployees(string orderBy = null, string orderDirection = null)
        {
            try
            {
                List<Employees> employees = await _employee.GetEmployeesAsync(orderBy, orderDirection);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Error en el servidor: " + ex.Message));
            }

        }
    }
}
