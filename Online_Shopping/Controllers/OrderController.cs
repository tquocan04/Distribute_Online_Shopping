using AutoMapper;
using DTOs.DTOs;
using DTOs.Request;
using DTOs.Responses;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Repositories;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;
using Services.Services;
using System.Net.Http;

namespace Online_Shopping.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IAddressService<Customer> _addressService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        private readonly string apiOrderNorth = "http://localhost:5285/api/orders/north";
        private readonly string apiOrderSouth = "http://localhost:5125/api/orders/south";
        private readonly string apiOrderCentral = "http://localhost:5257/api/orders/central";

        public OrderController(IOrderService orderService,
            IAddressService<Customer> addressService,
            IMapper mapper,
            HttpClient httpClient, ITokenService tokenService) 
        {
            _orderService = orderService;
            _addressService = addressService;
            _tokenService = tokenService;
            _mapper = mapper;
            _httpClient = httpClient;
        }
        
        [HttpGet("cart")]
        public async Task<IActionResult> GetCart() 
        {
            Guid id = await _tokenService.GetIdCustomerByToken();
            if (id == Guid.Empty)
                return BadRequest(new Response<string>
                {
                    Message = "Customer is invalid!"
                });

            string currentRegion = await _addressService.GetRegionIdOfObject(id);
            if (currentRegion == "Bac")
            {
                var response = await _httpClient.GetAsync($"{apiOrderNorth}/cart/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return Ok(new Response<OrderCartDTO>
                    {
                        Message = "Cart from customer in North",
                        Data = await response.Content.ReadFromJsonAsync<OrderCartDTO>()
                    });
                }
            }

            else if (currentRegion == "Nam")
            {
                var response = await _httpClient.GetAsync($"{apiOrderSouth}/cart/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return Ok(new Response<OrderCartDTO>
                    {
                        Message = "Cart from customer in South",
                        Data = await response.Content.ReadFromJsonAsync<OrderCartDTO>()
                    });
                }
            }

            else if (currentRegion == "Trung")
            {
                var response = await _httpClient.GetAsync($"{apiOrderCentral}/cart/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return Ok(new Response<OrderCartDTO>
                    {
                        Message = "Cart from customer in Central",
                        Data = await response.Content.ReadFromJsonAsync<OrderCartDTO>()
                    });
                }
            }
            var cart = await _orderService.GetOrderCart(id);
            return Ok(new Response<OrderCartDTO>
            {
                Message = "Cart from customer in South",
                Data = cart
            });
        }

        [HttpPost("new-item")]
        public async Task<IActionResult> AddToCart([FromQuery] Guid prodId)
        {
            Guid id = await _tokenService.GetIdCustomerByToken();

            if (id == Guid.Empty)
                return BadRequest(new Response<string>
                {
                    Message = "Customer is invalid!"
                });

            string currentRegion = await _addressService.GetRegionIdOfObject(id);
            if (currentRegion == "Bac")
                await _httpClient.PostAsync($"{apiOrderNorth}/new-item/{id}/{prodId}", null);
            
            else if (currentRegion == "Nam")
                await _httpClient.PostAsync($"{apiOrderSouth}/new-item/{id}/{prodId}", null);

            else if (currentRegion == "Trung")
                await _httpClient.PostAsync($"{apiOrderCentral}/new-item/{id}/{prodId}", null);

            await _orderService.AddToCart(id, prodId);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteItemInCart([FromQuery] Guid prodId)
        {
            Guid id = await _tokenService.GetIdCustomerByToken();
            string currentRegion = await _addressService.GetRegionIdOfObject(id);
            if (currentRegion == "Bac")
            {
                await _httpClient.DeleteAsync($"{apiOrderNorth}/{id}/{prodId}");
            }
            else if (currentRegion == "Nam")
            {
                await _httpClient.DeleteAsync($"{apiOrderSouth}/{id}/{prodId}");
            }
            else if (currentRegion == "Trung")
            {
                await _httpClient.DeleteAsync($"{apiOrderCentral}/{id}/{prodId}");
            }

            await _orderService.DeleteItemInCart(id, prodId);
            
            return NoContent();
        }

        [HttpPost("all-items")]
        public async Task<IActionResult> MergeItems([FromBody] List<RequestItems> products)
        {
            Guid id = await _tokenService.GetIdCustomerByToken();

            if (id == Guid.Empty)
                return BadRequest(new Response<string>
                {
                    Message = "Customer is invalid!"
                });

            if (products == null || products.Count == 0)
                return BadRequest(new Response<string>
                {
                    Message = "Product list is empty."
                });
            
            string currentRegion = await _addressService.GetRegionIdOfObject(id);
            if (currentRegion == "Bac")
                await _httpClient.PostAsJsonAsync($"{apiOrderNorth}/all-items/{id}", products);
            
            else if (currentRegion == "Nam")
                await _httpClient.PostAsJsonAsync($"{apiOrderSouth}/all-items/{id}", products);

            else if (currentRegion == "Trung")
                await _httpClient.PostAsJsonAsync($"{apiOrderCentral}/all-items/{id}", products);

            OrderCartDTO cart = await _orderService.MergeCartFromClient(id, products);
            if (cart == null)
                return BadRequest(new Response<string>
                {
                    Message = "Exist one product do not have in database."
                });

            return Ok(new Response<OrderCartDTO>
            {
                Message = "Products is merged successfully.",
                Data = cart
            });
        }

        [HttpPatch("{productId}")]
        public async Task<IActionResult> UpdateQuantityItem(Guid productId, [FromQuery] int Quantity)
        {
            Guid id = await _tokenService.GetIdCustomerByToken();

            if (id == Guid.Empty)
                return BadRequest(new Response<string>
                {
                    Message = "Customer is invalid!"
                });

            string currentRegion = await _addressService.GetRegionIdOfObject(id);
            if (currentRegion == "Bac")
            {
                await _httpClient.PatchAsync($"{apiOrderNorth}/{id}/{productId}?Quantity={Quantity}", null);
            }

            else if (currentRegion == "Nam")
                await _httpClient.PatchAsync($"{apiOrderSouth}/{id}/{productId}?Quantity={Quantity}", null);

            else if (currentRegion == "Trung")
                await _httpClient.PatchAsync($"{apiOrderCentral}/{id}/{productId}?Quantity={Quantity}", null);

            bool check = await _orderService.UpdateQuantityItem(id, productId, Quantity);
            if (!check)
                return BadRequest(new Response<string>
                {
                    Message = "Insufficient quantity of product!"
                });

            return NoContent();
        }
    }
}
