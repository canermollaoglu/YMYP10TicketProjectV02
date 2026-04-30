using Microsoft.AspNetCore.Mvc;
using TicketApp.Business.Services;
using TicketApp.Core.Entities;

namespace TicketApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductApiController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductApiController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }
    }
}