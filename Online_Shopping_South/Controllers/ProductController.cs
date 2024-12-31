using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Shopping_South.Entities;
using Online_Shopping_South.Repository.Contracts;
using Online_Shopping_South.Responses;
using Online_Shopping_South.Service.Contracts;

namespace Online_Shopping_South.Controllers
{
    [Route("api/products/south")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService,
            IProductRepo productRepo,
            IMapper mapper)
        {
            _productService = productService;
            _productRepo = productRepo;
            _mapper = mapper;
        }

        [HttpPost("new-product")]
        public async Task<IActionResult> CreateNewProduct([FromBody] Product product)
        {
            await _productRepo.CreateNewProductAsync(product);
            return CreatedAtAction(nameof(CreateNewProduct), new { id = product.Id });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateStatusProduct(Guid id)
        {
            await _productRepo.UpdatestatusProduct(id);
            return Ok(new Response<string>
            {
                Message = "The status is updated successfully"
            });
        }

        [HttpPut]
        public async Task UpdateInforProduct([FromBody] Product product)
        {
            await _productRepo.UpdateInforProduct(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}
