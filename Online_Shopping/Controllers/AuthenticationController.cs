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
using System.Security.Claims;

namespace Online_Shopping.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IOrderRepo _orderRepo;
        private readonly IAddressService<Customer> _addressService;
        private readonly ILoginRepo _loginRepo;
        private readonly ITokenService _tokenService;
        private readonly ICredentialRepo _credentialRepo;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly string apiNorth = "http://localhost:5285/api/authentication/north";
        private readonly string apiOrderNorth = "http://localhost:5285/api/orders/north";

        private readonly string apiSouth = "http://localhost:5125/api/authentication/south";
        private readonly string apiOrderSouth = "http://localhost:5125/api/orders/south";
        
        private readonly string apiCentral = "http://localhost:5257/api/authentication/central";
        private readonly string apiOrderCentral = "http://localhost:5257/api/orders/central";

        public AuthenticationController(IUserRepo userRepo, IUserService userService,
            HttpClient httpClient, IOrderService orderService, IMapper mapper,
            IAddressService<Customer> addressService, IOrderRepo orderRepo,
            ILoginRepo loginRepo, ITokenService tokenService, ICredentialRepo credentialRepo
            ) 
        {
            _userRepo = userRepo;
            _userService = userService;
            _orderService = orderService;
            _orderRepo = orderRepo;
            _addressService = addressService;
            _loginRepo = loginRepo;
            _tokenService = tokenService;
            _credentialRepo = credentialRepo;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        [HttpPost("signup-google")]
        public async Task<IActionResult> SignUpGG([FromBody] RequestSignupGoogle requestSignupGoogle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _userRepo.checkEmailExist(requestSignupGoogle.email))
            {
                return BadRequest("Email is existed");
            }

            if (!_userRepo.checkDOB(requestSignupGoogle.year))
            {
                return BadRequest("DoB is invalid");
            }

            var newCustomer = await _userService.CreateNewUserByGoogle(requestSignupGoogle);
            Order order = await _orderService.CreateNewCart(newCustomer.Id);

            Credential credential = new()
            {
                Id = requestSignupGoogle.googleId,
                Provider = "Google",
                CustomerId = newCustomer.Id,
            };
            await _credentialRepo.CreateCredentialAsync(credential);

            DistributedCustomer distributedCustomer = new()
            {
                Id = newCustomer.Id,
                OrderId = order.Id,
            };

            distributedCustomer = _mapper.Map(requestSignupGoogle, distributedCustomer);

            if (requestSignupGoogle.regionId == "Bac")
            {
                await _httpClient.PostAsJsonAsync($"{apiNorth}/signup-google/{credential.Id}", distributedCustomer);

                await _httpClient.PostAsync($"{apiOrderNorth}/{order.Id}/{order.CustomerId}", null);
            }
            else if (requestSignupGoogle.regionId == "Nam")
            {
                await _httpClient.PostAsJsonAsync($"{apiSouth}/signup-google/{credential.Id}", distributedCustomer);

                await _httpClient.PostAsync($"{apiOrderSouth}/{order.Id}/{order.CustomerId}", null);
            }
            else if (requestSignupGoogle.regionId == "Trung")
            {
                await _httpClient.PostAsJsonAsync($"{apiCentral}/signup-google/{credential.Id}", distributedCustomer);

                await _httpClient.PostAsync($"{apiOrderCentral}/{order.Id}/{order.CustomerId}", null);
            }

            return CreatedAtAction("GetProfileUser", new { id = newCustomer.Id }, distributedCustomer);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RequestCustomer requestCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _userRepo.checkEmailExist(requestCustomer.Email))
            {
                return BadRequest("Email is existed");
            }

            if (!_userRepo.checkDOB(requestCustomer.Year))
            {
                return BadRequest("DoB is invalid");
            }

            var newCustomer = await _userService.CreateNewUser(requestCustomer);
            Order order = await _orderService.CreateNewCart(newCustomer.Id);
            
            DistributedCustomer distributedCustomer = new ()
            {
                Id = newCustomer.Id,
                Picture = newCustomer.Picture,
                OrderId = order.Id,
            };

            distributedCustomer = _mapper.Map(requestCustomer, distributedCustomer);
            
            if (requestCustomer.RegionId == "Bac")
            {
                await _httpClient.PostAsJsonAsync($"{apiNorth}/register", distributedCustomer);

                await _httpClient.PostAsync($"{apiOrderNorth}/{order.Id}/{order.CustomerId}", null);
            }
            else if (requestCustomer.RegionId == "Nam")
            {
                await _httpClient.PostAsJsonAsync($"{apiSouth}/register", distributedCustomer);

                await _httpClient.PostAsync($"{apiOrderSouth}/{order.Id}/{order.CustomerId}", null);
            }
            else if (requestCustomer.RegionId == "Trung")
            {
                await _httpClient.PostAsJsonAsync($"{apiCentral}/register", distributedCustomer);

                await _httpClient.PostAsync($"{apiOrderCentral}/{order.Id}/{order.CustomerId}", null);
            }

            return CreatedAtAction("GetProfileUser", new { id = newCustomer.Id }, distributedCustomer);
        }

        [HttpPut("profile")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateUser([FromForm] RequestCustomer requestCustomer)
        {
            Guid id = await _tokenService.GetIdCustomerByToken();

            Customer? customer = await _userRepo.GetCustomerByIdAsync(id);

            if (customer == null)
                return NotFound(new Response<string>
                {
                    Message = "This customer does not exist!"
                });

            if (requestCustomer == null)
                return BadRequest(new Response<string>
                {
                    Message = "Invalid information"
                });
            
            string currentRegionId = await _addressService.GetRegionIdOfObject(customer.Id);

            customer = await _userService.UpdateInforUser(customer, requestCustomer);

            if (customer == null)
                return BadRequest("Cannot update customer");

            DistributedCustomer distributedCustomer = new()
            {
                Id = id,
                Picture = customer.Picture,
            };

            distributedCustomer = _mapper.Map(requestCustomer, distributedCustomer);

            string credentialId = await _credentialRepo.GetCredentialIdByCustomerIdAsync(customer.Id);

            if (currentRegionId == "Bac")
            {
                if (requestCustomer.RegionId == "Bac")
                {
                    await _httpClient.PutAsJsonAsync($"{apiNorth}/profile", distributedCustomer);
                }
                else
                {
                    await _httpClient.DeleteAsync($"{apiNorth}/profile/{customer.Id}");
                    if (credentialId == null)
                    {
                        if (requestCustomer.RegionId == "Nam")
                        {
                            await _httpClient.PostAsJsonAsync($"{apiSouth}/register", distributedCustomer);
                            var listOrder = await _orderRepo.GetListOrderByCusId(customer.Id);
                            for (int i = 0; i < listOrder.Count; i++)
                            {
                                await _httpClient.PostAsJsonAsync($"{apiOrderSouth}", listOrder[i]);
                                var listItem = await _orderRepo.GetListItemByOrderId(listOrder[i].Id);
                                if (listItem.Count != 0)
                                {
                                    for (int j = 0; j < listItem.Count; j++)
                                    {
                                        await _httpClient.PostAsJsonAsync($"{apiOrderSouth}/item", listItem[j]);
                                    }
                                }
                            }
                            return Ok(new Response<string>
                            {
                                Message = "Customer has been successfully updated to the South region."
                            });
                        }
                        else if (requestCustomer.RegionId == "Trung")
                        {
                            await _httpClient.PostAsJsonAsync($"{apiCentral}/register", distributedCustomer);
                            var listOrder = await _orderRepo.GetListOrderByCusId(customer.Id);
                            for (int i = 0; i < listOrder.Count; i++)
                            {
                                await _httpClient.PostAsJsonAsync($"{apiOrderCentral}", listOrder[i]);
                                var listItem = await _orderRepo.GetListItemByOrderId(listOrder[i].Id);
                                if (listItem.Count != 0)
                                {
                                    for (int j = 0; j < listItem.Count; j++)
                                    {
                                        await _httpClient.PostAsJsonAsync($"{apiOrderCentral}/item", listItem[j]);
                                    }
                                }
                            }
                            return Ok(new Response<string>
                            {
                                Message = "Customer has been successfully updated to the Central region."
                            });
                        }
                    }
                    else
                    {
                        if (requestCustomer.RegionId == "Nam")
                        {
                            await _httpClient.PostAsJsonAsync($"{apiSouth}/signup-google/{credentialId}", distributedCustomer);
                            var listOrder = await _orderRepo.GetListOrderByCusId(customer.Id);
                            for (int i = 0; i < listOrder.Count; i++)
                            {
                                await _httpClient.PostAsJsonAsync($"{apiOrderSouth}", listOrder[i]);
                                var listItem = await _orderRepo.GetListItemByOrderId(listOrder[i].Id);
                                if (listItem.Count != 0)
                                {
                                    for (int j = 0; j < listItem.Count; j++)
                                    {
                                        await _httpClient.PostAsJsonAsync($"{apiOrderSouth}/item", listItem[j]);
                                    }
                                }
                            }
                            return Ok(new Response<string>
                            {
                                Message = "Customer has been successfully updated to the South region."
                            });
                        }
                        else if (requestCustomer.RegionId == "Trung")
                        {
                            await _httpClient.PostAsJsonAsync($"{apiCentral}/signup-google/{credentialId}", distributedCustomer);
                            var listOrder = await _orderRepo.GetListOrderByCusId(customer.Id);
                            for (int i = 0; i < listOrder.Count; i++)
                            {
                                await _httpClient.PostAsJsonAsync($"{apiOrderCentral}", listOrder[i]);
                                var listItem = await _orderRepo.GetListItemByOrderId(listOrder[i].Id);
                                if (listItem.Count != 0)
                                {
                                    for (int j = 0; j < listItem.Count; j++)
                                    {
                                        await _httpClient.PostAsJsonAsync($"{apiOrderCentral}/item", listItem[j]);
                                    }
                                }
                            }
                            return Ok(new Response<string>
                            {
                                Message = "Customer has been successfully updated to the Central region."
                            });

                        }
                    }
                }
            }
            else if (currentRegionId == "Nam")
            {
                if (requestCustomer.RegionId == "Nam")
                    await _httpClient.PutAsJsonAsync($"{apiSouth}/profile", distributedCustomer);

                else
                {
                    await _httpClient.DeleteAsync($"{apiSouth}/profile/{customer.Id}");
                    if (credentialId == null)
                    {
                        if (requestCustomer.RegionId == "Bac")
                        {
                            await _httpClient.PostAsJsonAsync($"{apiNorth}/register", distributedCustomer);
                            var listOrder = await _orderRepo.GetListOrderByCusId(customer.Id);
                            for (int i = 0; i < listOrder.Count; i++)
                            {
                                await _httpClient.PostAsJsonAsync($"{apiOrderNorth}", listOrder[i]);
                                var listItem = await _orderRepo.GetListItemByOrderId(listOrder[i].Id);
                                if (listItem.Count != 0)
                                {
                                    for (int j = 0; j < listItem.Count; j++)
                                    {
                                        await _httpClient.PostAsJsonAsync($"{apiOrderNorth}/item", listItem[j]);
                                    }
                                }
                            }
                            return Ok(new Response<string>
                            {
                                Message = "Customer has been successfully updated to the South region."
                            });
                        }
                        else if (requestCustomer.RegionId == "Trung")
                        {
                            await _httpClient.PostAsJsonAsync($"{apiCentral}/register", distributedCustomer);
                            var listOrder = await _orderRepo.GetListOrderByCusId(customer.Id);
                            for (int i = 0; i < listOrder.Count; i++)
                            {
                                await _httpClient.PostAsJsonAsync($"{apiOrderCentral}", listOrder[i]);
                                var listItem = await _orderRepo.GetListItemByOrderId(listOrder[i].Id);
                                if (listItem.Count != 0)
                                {
                                    for (int j = 0; j < listItem.Count; j++)
                                    {
                                        await _httpClient.PostAsJsonAsync($"{apiOrderCentral}/item", listItem[j]);
                                    }
                                }
                            }
                            return Ok(new Response<string>
                            {
                                Message = "Customer has been successfully updated to the Central region."
                            });
                        }
                    }
                    else
                    {
                        if (requestCustomer.RegionId == "Bac")
                        {
                            await _httpClient.PostAsJsonAsync($"{apiNorth}/signup-google/{credentialId}", distributedCustomer);
                            var listOrder = await _orderRepo.GetListOrderByCusId(customer.Id);
                            for (int i = 0; i < listOrder.Count; i++)
                            {
                                await _httpClient.PostAsJsonAsync($"{apiOrderNorth}", listOrder[i]);
                                var listItem = await _orderRepo.GetListItemByOrderId(listOrder[i].Id);
                                if (listItem.Count != 0)
                                {
                                    for (int j = 0; j < listItem.Count; j++)
                                    {
                                        await _httpClient.PostAsJsonAsync($"{apiOrderNorth}/item", listItem[j]);
                                    }
                                }
                            }
                            return Ok(new Response<string>
                            {
                                Message = "Customer has been successfully updated to the South region."
                            });
                        }
                        else if (requestCustomer.RegionId == "Trung")
                        {
                            await _httpClient.PostAsJsonAsync($"{apiCentral}/signup-google/{credentialId}", distributedCustomer);
                            var listOrder = await _orderRepo.GetListOrderByCusId(customer.Id);
                            for (int i = 0; i < listOrder.Count; i++)
                            {
                                await _httpClient.PostAsJsonAsync($"{apiOrderCentral}", listOrder[i]);
                                var listItem = await _orderRepo.GetListItemByOrderId(listOrder[i].Id);
                                if (listItem.Count != 0)
                                {
                                    for (int j = 0; j < listItem.Count; j++)
                                    {
                                        await _httpClient.PostAsJsonAsync($"{apiOrderCentral}/item", listItem[j]);
                                    }
                                }
                            }
                            return Ok(new Response<string>
                            {
                                Message = "Customer has been successfully updated to the Central region."
                            });

                        }
                    }
                }
            }
            else if (currentRegionId == "Trung")
            {
                if (requestCustomer.RegionId == "Trung")
                    await _httpClient.PutAsJsonAsync($"{apiCentral}/profile", distributedCustomer);

                else
                {
                    await _httpClient.DeleteAsync($"{apiCentral}/profile/{customer.Id}");
                    if (credentialId == null)
                    {
                        if (requestCustomer.RegionId == "Bac")
                        {
                            await _httpClient.PostAsJsonAsync($"{apiNorth}/register", distributedCustomer);
                            var listOrder = await _orderRepo.GetListOrderByCusId(customer.Id);
                            for (int i = 0; i < listOrder.Count; i++)
                            {
                                await _httpClient.PostAsJsonAsync($"{apiOrderNorth}", listOrder[i]);
                                var listItem = await _orderRepo.GetListItemByOrderId(listOrder[i].Id);
                                if (listItem.Count != 0)
                                {
                                    for (int j = 0; j < listItem.Count; j++)
                                    {
                                        await _httpClient.PostAsJsonAsync($"{apiOrderNorth}/item", listItem[j]);
                                    }
                                }
                            }
                            return Ok(new Response<string>
                            {
                                Message = "Customer has been successfully updated to the North region."
                            });
                        }
                        else if (requestCustomer.RegionId == "Nam")
                        {
                            await _httpClient.PostAsJsonAsync($"{apiSouth}/register", distributedCustomer);
                            var listOrder = await _orderRepo.GetListOrderByCusId(customer.Id);
                            for (int i = 0; i < listOrder.Count; i++)
                            {
                                await _httpClient.PostAsJsonAsync($"{apiOrderSouth}", listOrder[i]);
                                var listItem = await _orderRepo.GetListItemByOrderId(listOrder[i].Id);
                                if (listItem.Count != 0)
                                {
                                    for (int j = 0; j < listItem.Count; j++)
                                    {
                                        await _httpClient.PostAsJsonAsync($"{apiOrderSouth}/item", listItem[j]);
                                    }
                                }
                            }
                            return Ok(new Response<string>
                            {
                                Message = "Customer has been successfully updated to the South region."
                            });
                        }
                    }
                    else
                    {
                        if (requestCustomer.RegionId == "Bac")
                        {
                            await _httpClient.PostAsJsonAsync($"{apiNorth}/signup-google/{credentialId}", distributedCustomer);
                            var listOrder = await _orderRepo.GetListOrderByCusId(customer.Id);
                            for (int i = 0; i < listOrder.Count; i++)
                            {
                                await _httpClient.PostAsJsonAsync($"{apiOrderNorth}", listOrder[i]);
                                var listItem = await _orderRepo.GetListItemByOrderId(listOrder[i].Id);
                                if (listItem.Count != 0)
                                {
                                    for (int j = 0; j < listItem.Count; j++)
                                    {
                                        await _httpClient.PostAsJsonAsync($"{apiOrderNorth}/item", listItem[j]);
                                    }
                                }
                            }
                            return Ok(new Response<string>
                            {
                                Message = "Customer has been successfully updated to the North region."
                            });
                        }
                        else if (requestCustomer.RegionId == "Nam")
                        {
                            await _httpClient.PostAsJsonAsync($"{apiSouth}/signup-google/{credentialId}", distributedCustomer);
                            var listOrder = await _orderRepo.GetListOrderByCusId(customer.Id);
                            for (int i = 0; i < listOrder.Count; i++)
                            {
                                await _httpClient.PostAsJsonAsync($"{apiOrderSouth}", listOrder[i]);
                                var listItem = await _orderRepo.GetListItemByOrderId(listOrder[i].Id);
                                if (listItem.Count != 0)
                                {
                                    for (int j = 0; j < listItem.Count; j++)
                                    {
                                        await _httpClient.PostAsJsonAsync($"{apiOrderSouth}/item", listItem[j]);
                                    }
                                }
                            }
                            return Ok(new Response<string>
                            {
                                Message = "Customer has been successfully updated to the South region."
                            });
                        }
                    }
                }
            }

            return Ok(new Response<Customer>
            {
                Message = "Customer is updated successfully",
                Data = customer
            });
        }

        [HttpGet("profile")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetProfileUser()
        {
            Guid id = await _tokenService.GetIdCustomerByToken();

            string regionId = await _addressService.GetRegionIdOfObject(id);
            if (regionId == "Bac")
            {
                var response = await _httpClient.GetAsync($"{apiNorth}/profile/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<CustomerDTO>();
                    if (result == null)
                        return NotFound(new Response<string>
                        {
                            Message = "This customer does not exist in North region!"
                        });

                    return Ok(new Response<CustomerDTO>
                    {
                        Message = "This customer is in North region!",
                        Data = result
                    });
                }
            }
            else if (regionId == "Nam")
            {
                var response = await _httpClient.GetAsync($"{apiSouth}/profile/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<CustomerDTO>();
                    if (result == null)
                        return NotFound(new Response<string>
                        {
                            Message = "This customer does not exist in South region!"
                        });

                    return Ok(new Response<CustomerDTO>
                    {
                        Message = "This customer is in South region!",
                        Data = result
                    });
                }
            }
            else if (regionId == "Trung")
            {
                var response = await _httpClient.GetAsync($"{apiCentral}/profile/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<CustomerDTO>();
                    if (result == null)
                        return NotFound(new Response<string>
                        {
                            Message = "This customer does not exist in Central region!"
                        });

                    return Ok(new Response<CustomerDTO>
                    {
                        Message = "This customer is in Central region!",
                        Data = result
                    });
                }
            }

            var user = await _userService.GetProfileUser(id);
            return Ok(user);
        }

    }
}
