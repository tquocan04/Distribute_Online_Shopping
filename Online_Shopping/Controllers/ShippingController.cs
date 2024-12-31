using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Interfaces;
using Services.Services;

namespace Online_Shopping.Controllers
{
    [Route("api/shippingmethods")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly IShippingService _shippingService;

        public ShippingController(IShippingService shippingService) 
        {
            _shippingService = shippingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShippingMethods()
        {
            var list = await _shippingService.GetShippingListAsync();
            return Ok(list);
        }
    }
}
