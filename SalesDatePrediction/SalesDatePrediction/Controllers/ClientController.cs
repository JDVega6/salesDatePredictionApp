using SalesDatePrediction.Models;
using SalesDatePrediction.Repositories.ClientRepository;
using System.Collections.Generic;
using System;
using System.Web.Http;
using System.Threading.Tasks;

namespace SalesDatePrediction.Controllers
{
    [RoutePrefix("api/client")]
    public class ClientController : ApiController
    {
        
        private readonly IClientRepository _client;
        public ClientController()
        {
            _client = new ClientRepository();
        }

        [HttpGet]
        [Route("{custId:int}")]
        public async Task<IHttpActionResult> GetClient(int custId, string orderBy = null, string orderDirection = null)
        {
            try
            {
                List<ClientOrder> client = await _client.GetClientOrdersAsync(custId, orderBy, orderDirection);
                return Ok(client);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Error en el servidor: " + ex.Message));
            }

        }
    }
}
