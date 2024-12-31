using AutoMapper;
using CloudinaryDotNet;
using DTOs.DTOs;
using DTOs.Request;
using DTOs.Responses;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;

namespace Online_Shopping.Controllers
{
    [Route("api/bills")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IPaymentRepo _paymentRepo;
        private readonly ITokenService _tokenService;
        private readonly IBillService _billService;
        private readonly IShippingRepo _shippingRepo;
        private readonly IBillRepo _billRepo;
        private readonly IEmployeeRepo _employeeRepo;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IAddressService<Customer> _addressService;

        private readonly string apiNorth = "http://localhost:5285/api/bills/north";
        private readonly string apiSouth = "http://localhost:5125/api/bills/south";
        private readonly string apiCentral = "http://localhost:5257/api/bills/central";
        private readonly string apiOrderNorth = "http://localhost:5285/api/orders/north";
        private readonly string apiOrderSouth = "http://localhost:5125/api/orders/south";
        private readonly string apiOrderCentral = "http://localhost:5257/api/orders/central";

        public BillController(IOrderService orderService,
            IPaymentRepo paymentRepo,
            IMapper mapper, IBillService billService,
            IShippingRepo shippingRepo,
            IBillRepo billRepo,
            IEmployeeRepo employeeRepo,
            HttpClient httpClient,
            IAddressService<Customer> addressService, ITokenService tokenService)
        {
            _orderService = orderService;
            _paymentRepo = paymentRepo;
            _tokenService = tokenService;
            _billService = billService;
            _shippingRepo = shippingRepo;
            _billRepo = billRepo;
            _employeeRepo = employeeRepo;
            _mapper = mapper;
            _httpClient = httpClient;
            _addressService = addressService;
        }
        
        [HttpPut("payment")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> PaytoBill([FromBody] RequestBill requestBill)
        {
            if (await _paymentRepo.GetPaymentIdAsync(requestBill.PaymentId) == null)
                return BadRequest(new Response<string>
                {
                    Message = "Does not exist this payment!"
                });

            if (await _shippingRepo.GetShippingMethodByIdAsync(requestBill.ShippingMethodId) == null)
                return BadRequest(new Response<string>
                {
                    Message = "Does not exist this payment!"
                });

            Guid id = await _tokenService.GetIdCustomerByToken();
            if (id == Guid.Empty)
                return BadRequest(new Response<string>
                {
                    Message = "Customer is invalid!"
                });

            string currentRegion = await _addressService.GetRegionIdOfObject(id);

            var (order, newOrder) = await _billService.CartToBill(id, requestBill);

            if (order == null)
                return BadRequest(new Response<string>
                {
                    Message = "Cannot create this bill!"
                });

            if (currentRegion == "Bac")
            {
                await _httpClient.PutAsJsonAsync($"{apiNorth}/payment/{id}/{order.Id}", requestBill);
                await _httpClient.PostAsJsonAsync($"{apiOrderNorth}", newOrder);
            }

            else if (currentRegion == "Nam")
            {

                await _httpClient.PutAsJsonAsync($"{apiSouth}/payment/{id}/{order.Id}", requestBill);
                await _httpClient.PostAsJsonAsync($"{apiOrderSouth}", newOrder);
            }

            else if (currentRegion == "Trung")
            {
                await _httpClient.PutAsJsonAsync($"{apiCentral}/payment/{id}/{order.Id}", requestBill);
                await _httpClient.PostAsJsonAsync($"{apiOrderCentral}", newOrder);
            }

            return Ok(order);
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetBills()
        {

            Guid id = await _tokenService.GetIdCustomerByToken();
            if (id == Guid.Empty)
                return BadRequest(new Response<string>
                {
                    Message = "Customer is invalid!"
                });

            var listBills = await _billService.GetOrderBill(id);

            if (listBills == null)
                return NotFound(new Response<string>
                {
                    Message = "Does not have any bills!"
                });

            return Ok(listBills);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> CompletedBills(Guid id)
        {
            Guid? customerId = await _billRepo.GetCustomerIdOfBillAsync(id);
            if (customerId != null)
            {
                var customerAddress = await _addressService.GetRegionIdOfObject((Guid)customerId);
                if (customerAddress == "Bac")
                    await _httpClient.PatchAsync($"{apiNorth}/{id}", null);
                else if (customerAddress == "Nam")
                    await _httpClient.PatchAsync($"{apiSouth}/{id}", null);
                else if (customerAddress == "Trung")
                    await _httpClient.PatchAsync($"{apiCentral}/{id}", null);
            }
            await _billRepo.CompletedBillAsync(id);

            return NoContent();
        }

        [HttpGet("pending-bills")]
        [Authorize]
        public async Task<IActionResult> GetListPendingBills()
        {
            string role = _tokenService.GetRoleByToken();
            
            List<OrderBillDTO> list = new();

            if (role == "Admin" || role == "Staff")
            {
                Guid id = await _tokenService.GetIdEmployeeByToken();
                var region = await _addressService.GetRegionIdOfObject(id);
                if (region == "Bac")
                {
                    var response = await _httpClient.GetAsync($"{apiNorth}/pending-bills");
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(new Response<List<OrderBillDTO>>
                        {
                            Message = "Pending bills list from North",
                            Data = await response.Content.ReadFromJsonAsync<List<OrderBillDTO>>()
                        });
                    }
                }
                else if (region == "Nam")
                {
                    var response = await _httpClient.GetAsync($"{apiSouth}/pending-bills");
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(new Response<List<OrderBillDTO>>
                        {
                            Message = "Pending bills list from South",
                            Data = await response.Content.ReadFromJsonAsync<List<OrderBillDTO>>()
                        });
                    }
                }
                else if (region == "Trung")
                {
                    var response = await _httpClient.GetAsync($"{apiCentral}/pending-bills");
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(new Response<List<OrderBillDTO>>
                        {
                            Message = "Pending bills list from Central",
                            Data = await response.Content.ReadFromJsonAsync<List<OrderBillDTO>>()
                        });
                    }
                }

                list = await _billService.EmployeeGetPendingBillList();
            }
            else
            {
                Guid id = await _tokenService.GetIdCustomerByToken();
                var region = await _addressService.GetRegionIdOfObject(id);
                if (region == "Bac")
                {
                    var response = await _httpClient.GetAsync($"{apiNorth}/customer-pending-bills/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(new Response<List<OrderBillDTO>>
                        {
                            Message = "Customer get pending bills list from North",
                            Data = await response.Content.ReadFromJsonAsync<List<OrderBillDTO>>()
                        });
                    }
                }
                else if (region == "Nam")
                {
                    var response = await _httpClient.GetAsync($"{apiSouth}/customer-pending-bills/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(new Response<List<OrderBillDTO>>
                        {
                            Message = "Customer get pending bills list from South",
                            Data = await response.Content.ReadFromJsonAsync<List<OrderBillDTO>>()
                        });
                    }
                }
                else if (region == "Trung")
                {
                    var response = await _httpClient.GetAsync($"{apiCentral}/customer-pending-bills/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(new Response<List<OrderBillDTO>>
                        {
                            Message = "Customer get pending bills list from Central",
                            Data = await response.Content.ReadFromJsonAsync<List<OrderBillDTO>>()
                        });
                    }
                }
                list = await _billService.CustomerGetPendingBillList(await _tokenService.GetIdCustomerByToken());
            }

            if (list == null)
                return NotFound(new Response<string>
                {
                    Message = "Does not have any pending bills!"
                });

            return Ok(new Response<List<OrderBillDTO>>
            {
                Message = "Customer get pending bills list from Central",
                Data = list
            });
        }
        
        [HttpGet("completed-bills")]
        [Authorize]
        public async Task<IActionResult> GetListCompletedBills()
        {
            string role = _tokenService.GetRoleByToken();

            List<OrderBillDTO> list = new();

            if (role == "Admin" || role == "Staff")
            {
                Guid id = await _tokenService.GetIdEmployeeByToken();
                var region = await _addressService.GetRegionIdOfObject(id);
                if (region == "Bac")
                {
                    var response = await _httpClient.GetAsync($"{apiNorth}/completed-bills");
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(new Response<List<OrderBillDTO>>
                        {
                            Message = "Completed bills list from North",
                            Data = await response.Content.ReadFromJsonAsync<List<OrderBillDTO>>()
                        });
                    }
                }

                else if (region == "Nam")
                {
                    var response = await _httpClient.GetAsync($"{apiSouth}/completed-bills");
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(new Response<List<OrderBillDTO>>
                        {
                            Message = "Completed bills list from South",
                            Data = await response.Content.ReadFromJsonAsync<List<OrderBillDTO>>()
                        });
                    }
                }

                else if (region == "Trung")
                {
                    var response = await _httpClient.GetAsync($"{apiCentral}/completed-bills");
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(new Response<List<OrderBillDTO>>
                        {
                            Message = "Completed bills list from Central",
                            Data = await response.Content.ReadFromJsonAsync<List<OrderBillDTO>>()
                        });
                    }
                }

                list = await _billService.EmployeeGetCompletedBillList();
            }
            else
            {
                Guid id = await _tokenService.GetIdCustomerByToken();
                var region = await _addressService.GetRegionIdOfObject(id);
                if (region == "Bac")
                {
                    var response = await _httpClient.GetAsync($"{apiNorth}/customer-completed-bills/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(new Response<List<OrderBillDTO>>
                        {
                            Message = "Customer get completed bills list from North",
                            Data = await response.Content.ReadFromJsonAsync<List<OrderBillDTO>>()
                        });
                    }
                }
                else if (region == "Nam")
                {
                    var response = await _httpClient.GetAsync($"{apiSouth}/customer-completed-bills/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(new Response<List<OrderBillDTO>>
                        {
                            Message = "Customer get completed bills list from South",
                            Data = await response.Content.ReadFromJsonAsync<List<OrderBillDTO>>()
                        });
                    }
                }
                else if (region == "Trung")
                {
                    var response = await _httpClient.GetAsync($"{apiCentral}/customer-completed-bills/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(new Response<List<OrderBillDTO>>
                        {
                            Message = "Customer get completed bills list from Central",
                            Data = await response.Content.ReadFromJsonAsync<List<OrderBillDTO>>()
                        });
                    }
                }
                list = await _billService.CustomerGetCompletedBillList(await _tokenService.GetIdCustomerByToken());
            }

            if (list == null)
                return NotFound(new Response<string>
                {
                    Message = "Does not have any completed bills!"
                });

            return Ok(list);
        }
        
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetDetailBills(Guid id)
        {
            string role = _tokenService.GetRoleByToken();
            Guid userid = Guid.Empty;

            if (role == "Customer")
                userid = await _tokenService.GetIdCustomerByToken();
            else
                userid = await _tokenService.GetIdEmployeeByToken();

            var region = await _addressService.GetRegionIdOfObject(userid);

            if (region == "Bac")
            {
                var response = await _httpClient.GetAsync($"{apiNorth}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return Ok(new Response<OrderBillDTO>
                    {
                        Message = "Detail bill from North",
                        Data = await response.Content.ReadFromJsonAsync<OrderBillDTO>()
                    });
                }
            }
            else if (region == "Nam")
            {
                var response = await _httpClient.GetAsync($"{apiSouth}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return Ok(new Response<OrderBillDTO>
                    {
                        Message = "Detail bill from South",
                        Data = await response.Content.ReadFromJsonAsync<OrderBillDTO>()
                    });
                }
            }
            else if (region == "Trung")
            {
                var response = await _httpClient.GetAsync($"{apiCentral}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return Ok(new Response<OrderBillDTO>
                    {
                        Message = "Detail bill from Central",
                        Data = await response.Content.ReadFromJsonAsync<OrderBillDTO>()
                    });
                }
            }

            var bill = await _billService.GetBillDetail(id);
                
            if (bill == null)
                return NotFound(new Response<string>
                {
                    Message = "Does not have this bill!"
                });

            return Ok(bill);
        }
    }
}
