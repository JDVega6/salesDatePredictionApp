using SalesDatePrediction.Models;
using SalesDatePrediction.Repositories.ProductRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SalesDatePrediction.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductController : ApiController
    {
        
        private readonly IProductRepository _productRepository;
        public ProductController()
        {
            _productRepository = new ProductRepository();
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetProduct(string orderBy = null, string orderDirection = null)
        {
            try
            {
                List<Product> products = await _productRepository.GetProductAsync(orderBy, orderDirection);
                return Ok(products);
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
