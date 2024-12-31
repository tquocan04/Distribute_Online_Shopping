using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Shopping_Central.DTOs;
using Online_Shopping_Central.Entities;
using Online_Shopping_Central.Repository.Contracts;
using Online_Shopping_Central.Requests;
using Online_Shopping_Central.Service.Contracts;

namespace Online_Shopping_Central.Controllers
{
    [Route("api/orders/central")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderRepo _orderRepo;

        public OrderController(IOrderService orderService, IOrderRepo orderRepo)
        {
            _orderService = orderService;
            _orderRepo = orderRepo;
        }

        [HttpGet("cart/{cusId}")]
        public async Task<IActionResult> GetCart(Guid cusId)
        {
            OrderCartDTO cart = await _orderService.GetOrderCart(cusId);
            return Ok(cart);
        }

        [HttpPost("new-item/{cusId}/{prodId}")]
        public async Task<IActionResult> AddToCart(Guid cusId, Guid prodId)
        {
            await _orderService.AddToCart(cusId, prodId);
            return NoContent();
        }

        [HttpDelete("{cusId}/{prodId}")]
        public async Task<IActionResult> DeleteItemInCart(Guid cusId, Guid prodId)
        {
            await _orderService.DeleteItemInCart(cusId, prodId);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            await _orderRepo.CreateOrder(order);
            return CreatedAtAction(nameof(CreateOrder), new { id = order.Id });
        }

        [HttpPost("{id}/{cusId}")]
        public async Task<IActionResult> RegisterOrder(Guid id, Guid cusId)
        {
            Order order = new()
            {
                Id = id,
                CustomerId = cusId,
                TotalPrice = 0,
            };
            await _orderRepo.CreateOrder(order);
            return CreatedAtAction(nameof(RegisterOrder), new { id = order.Id });
        }

        [HttpPost("item")]
        public async Task<IActionResult> CreateItem([FromBody] Item item)
        {
            await _orderRepo.AddItemToCart(item);
            return CreatedAtAction(nameof(CreateItem), new { id = item.OrderId });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            await _orderRepo.DeleteOrderAsync(id);
            return NoContent();
        }

        [HttpPost("all-items/{id}")]
        public async Task<IActionResult> MergeItems(Guid id, [FromBody] List<RequestItems> products)
        {
            OrderCartDTO cart = await _orderService.MergeCartFromClient(id, products);
            if (cart == null)
                return BadRequest();

            return Ok();
        }

        [HttpPatch("{id}/{productId}")]
        public async Task<IActionResult> UpdateQuantityItem(Guid id, Guid productId, [FromQuery] int Quantity)
        {
            bool check = await _orderService.UpdateQuantityItem(id, productId, Quantity);
            if (!check)
                return BadRequest();

            return NoContent();
        }
    }
}
