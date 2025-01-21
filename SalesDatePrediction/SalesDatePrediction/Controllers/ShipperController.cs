using SalesDatePrediction.Models;
using SalesDatePrediction.Repositories.ShipperRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SalesDatePrediction.Controllers
{
    [RoutePrefix("api/shipper")]
    public class ShipperController : ApiController
    {
        private readonly IShipperRepository _shipperRepository;
        public ShipperController()
        {
            _shipperRepository = new ShipperRepository();
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetShipper(string orderBy = null, string orderDirection = null)
        {
            try
            {
                List<Shipper> shippers = await _shipperRepository.GetShipperAsync(orderBy, orderDirection);
                return Ok(shippers);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Error en el servidor: " + ex.Message));
            }

        }
    }

}
