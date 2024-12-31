using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Shopping_Central.DTOs;
using Online_Shopping_Central.Repository.Contracts;
using Online_Shopping_Central.Requests;
using Online_Shopping_Central.Responses;
using Online_Shopping_Central.Service.Contracts;

namespace Online_Shopping_Central.Controllers
{
    [Route("api/bills/central")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IBillService _billService;
        private readonly IBillRepo _billRepo;

        public BillController(IOrderService orderService,
            IBillService billService,
            IBillRepo billRepo)
        {
            _orderService = orderService;
            _billService = billService;
            _billRepo = billRepo;
        }

        [HttpPut("payment/{id}/{orderId}")]
        public async Task<IActionResult> PaytoBill(Guid id, Guid orderId, [FromBody] RequestBill requestBill)
        {
            var neworder = await _billService.CartToBill(id, orderId, requestBill);

            if (neworder == null)
                return BadRequest();
            return Ok(neworder);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> CompletedBills(Guid id)
        {
            await _billRepo.CompletedBillAsync(id);

            return NoContent();
        }

        [HttpGet("pending-bills")]
        public async Task<IActionResult> GetListPendingBills()
        {
            List<OrderBillDTO> list = new();
            list = await _billService.EmployeeGetPendingBillList();
            if (list == null)
                return NotFound(new Response<string>
                {
                    Message = "Does not have any pending bills!"
                });

            return Ok(list);
        }

        [HttpGet("customer-pending-bills/{id}")]
        public async Task<IActionResult> CustomerGetListPendingBills(Guid id)
        {
            List<OrderBillDTO> list = new();
            list = await _billService.CustomerGetPendingBillList(id);
            if (list == null)
                return NotFound(new Response<string>
                {
                    Message = "Does not have any pending bills!"
                });

            return Ok(list);
        }

        [HttpGet("completed-bills")]
        public async Task<IActionResult> GetListCompletedBills()
        {
            List<OrderBillDTO> list = new();
            list = await _billService.EmployeeGetCompletedBillList();

            if (list == null)
                return NotFound(new Response<string>
                {
                    Message = "Does not have any completed bills!"
                });

            return Ok(list);
        }

        [HttpGet("customer-completed-bills/{id}")]
        public async Task<IActionResult> CustomerGetListCompletedBills(Guid id)
        {
            List<OrderBillDTO> list = new();
            list = await _billService.CustomerGetCompletedBillList(id);

            if (list == null)
                return NotFound(new Response<string>
                {
                    Message = "Does not have any completed bills!"
                });

            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailBills(Guid id)
        {
            var bill = await _billService.GetBillDetail(id);

            if (bill == null)
                return NotFound(new Response<string>
                {
                    Message = "Does not have any completed bills!"
                });

            return Ok(bill);
        }
    }
}
