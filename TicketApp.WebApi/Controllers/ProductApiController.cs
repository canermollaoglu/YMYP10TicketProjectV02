using Microsoft.AspNetCore.Mvc;
using TicketApp.Business.Services;
using TicketApp.Core.DTOs;
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

        //HTTP METODLARI NELERDİR?
        //GET: Verileri almak için kullanılır.
        //POST: Yeni veri eklemek için kullanılır.
        //PUT: Mevcut veriyi güncellemek için kullanılır.
        //DELETE: Veriyi silmek için kullanılır.
        //PATCH: Mevcut verinin bir kısmını güncellemek için kullanılır.

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetAll()
        {
            var products = await _productService.GetProductsWithCategoryAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDTO>> GetById(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponseDTO>> Create(ProductCreateDTO product)
        {
            var created = await _productService.AddAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
    }
}