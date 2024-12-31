using AutoMapper;
using DTOs.DTOs;
using DTOs.Request;
using DTOs.Responses;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;
using Services.Services;
using System.Net.Http;

namespace Online_Shopping.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly string apiNorth = "http://localhost:5285/api/products/north";
        private readonly string apiSouth = "http://localhost:5125/api/products/south";
        private readonly string apiCentral = "http://localhost:5257/api/products/central";

        public ProductController(IProductService productService, IMapper mapper,
            IProductRepo productRepo,
            HttpClient httpClient) 
        {
            _productService = productService;
            _productRepo = productRepo;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        [HttpPost("new-product")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> CreateNewProduct([FromForm] RequestProduct request)
        {
            Product newProduct = await _productService.CreateNewProduct(request);

            var north = await _httpClient.PostAsJsonAsync($"{apiNorth}/new-product", newProduct);
            var south = await _httpClient.PostAsJsonAsync($"{apiSouth}/new-product", newProduct);
            var central = await _httpClient.PostAsJsonAsync($"{apiCentral}/new-product", newProduct);

            return CreatedAtAction(nameof(GetDetailProduct), new { id = newProduct.Id },
                new Response<Product>
                {
                    Message = "New product created successfully",
                    Data = newProduct
                });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            if (!products.Any())
            {
                return NotFound(new Response<IEnumerable<ProductDTO>>
                {
                    Message = "Does not have any products!",
                    Data = products
                });
            }
            return Ok(products);
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetProductsNotHidden()
        {
            var products = await _productService.GetProductsNotHidden();
            if (!products.Any())
                return NotFound(new Response<IEnumerable<ProductDTO>>
                {
                    Message = "There are no products available!",
                    Data = products
                });

            return Ok(products);
        }

        [HttpGet("hidden")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> GetProductsHidden()
        {
            var products = await _productService.GetProductsHidden();
            if (!products.Any())
                return NotFound(new Response<IEnumerable<ProductDTO>>
                {
                    Message = "There are no hidden products!",
                    Data = products
                });

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailProduct(Guid id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
                return NotFound(new Response<string>
                {
                    Message = "This product does not exist!",
                });
            return Ok(product);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> UpdateStatusProductById(Guid id)
        {
            if (!await _productRepo.CheckExistingProduct(id))
                return NotFound(new Response<string>
                {
                    Message = "This product does not exist!",
                });

            await _productRepo.UpdatestatusProduct(id);

            await _httpClient.PatchAsync($"{apiNorth}/{id}", null);
            await _httpClient.PatchAsync($"{apiSouth}/{id}", null);
            await _httpClient.PatchAsync($"{apiCentral}/{id}", null);

            return Ok(new Response<string>
            {
                Message = "The status is updated successfully"
            });
            
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> UpdateInforProduct(Guid id, [FromForm] RequestProduct requestProduct)
        {
            if (!await _productRepo.CheckExistingProduct(id))
                return NotFound(new Response<string>
                {
                    Message = "This product does not exist!",
                });

            Product product = await _productService.UpdateInforProduct(id, requestProduct);

            await _httpClient.PutAsJsonAsync($"{apiNorth}", product);
            await _httpClient.PutAsJsonAsync($"{apiSouth}", product);
            await _httpClient.PutAsJsonAsync($"{apiCentral}", product);

            return Ok(new Response<string>
            {
                Message = "The information is updated successfully"
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productService.DeleteProduct(id);

            return NoContent();
        }
    }
}
