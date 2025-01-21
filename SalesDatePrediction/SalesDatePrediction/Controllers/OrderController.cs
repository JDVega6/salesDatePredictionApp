using SalesDatePrediction.Models;
using SalesDatePrediction.Repositories.OrderRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SalesDatePrediction.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrderController : ApiController
    {
        
        private readonly IOrderRepository _orderRepository;

        public OrderController()
        {
            _orderRepository = new OrderRepository();
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetPredictOrder(string CustomerName = "", string orderBy = null, string orderDirection = null)
        {
            try
            {
                List<PredictedOrder> predictOrder = await _orderRepository.GetOrderNextPredictionAsync(CustomerName, orderBy, orderDirection);
                return Ok(predictOrder);
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


        [HttpPost]
        [Route("AddOrder")]
        public IHttpActionResult AddOrder([FromBody] OrderRequest orderRequest)
        {

            try
            {
                var success = _orderRepository.AddOrder(orderRequest);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
