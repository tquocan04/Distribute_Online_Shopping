using AutoMapper;
using Online_Shopping_North.DTOs;
using Online_Shopping_North.Entities;
using Online_Shopping_North.Repository.Contracts;
using Online_Shopping_North.Requests;
using Online_Shopping_North.Service.Contracts;

namespace Online_Shopping_North.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly IMapper _mapper;
        private readonly IAddressRepo _addressRepo;
        private readonly IAddressService<CustomerDTO> _addressService;

        public CustomerService
            (ICustomerRepo customerRepo,
            IMapper mapper,
            IAddressRepo addressRepo,
            IAddressService<CustomerDTO> addressService
            )
        {
            _customerRepo = customerRepo;
            _mapper = mapper;
            _addressRepo = addressRepo;
            _addressService = addressService;

        }


        public async Task<Customer> CreateNewUser(DistributedCustomer distributedCustomer)
        {
            Customer customer = new Customer
            {
                Dob = new DateOnly(distributedCustomer.Year, distributedCustomer.Month, distributedCustomer.Day)
            };

            _mapper.Map(distributedCustomer, customer);
            await _customerRepo.CreateNewCustomer(customer);

            Address address = new Address
            {
                Id = Guid.NewGuid(),
                CustomerId = customer.Id,
                IsDefault = true,
            };
            _mapper.Map(distributedCustomer, address);

            await _addressRepo.CreateNewAddress(address);

            return customer;
        }

        public async Task<bool> UpdateInforUser(DistributedCustomer distributedCustomer)
        {
            Customer customer = new Customer
            {
                Id = distributedCustomer.Id,
            };

            // truy van tim record:
            var address = await _addressRepo.GetAddressByObjectIdAsync(distributedCustomer.Id);

            if (address.DistrictId != distributedCustomer.DistrictId || address.Street != distributedCustomer.Street)
            {
                _mapper.Map(distributedCustomer, address);
                await _addressRepo.UpdateAddress(address);
            }

            _mapper.Map(distributedCustomer, customer);

            customer.Dob = new DateOnly(distributedCustomer.Year, distributedCustomer.Month, distributedCustomer.Day);

            await _customerRepo.UpdateInforCustomer(customer);

            return true;
        }

        public async Task<CustomerDTO> GetProfileUser(string cusId)
        {
            var cus = await _customerRepo.GetCustomerByIdAsync(Guid.Parse(cusId));

            if (cus != null)
            {

                CustomerDTO cusDTO = _mapper.Map<CustomerDTO>(cus);

                cusDTO = await _addressService.SetAddress(cusDTO, cusDTO.Id);
                return cusDTO;
            }

            return null;

        }

        public async Task DeleteCustomer(Guid id)
        {
            Customer? customer = await _customerRepo.GetCustomerByIdAsync(id);
            await _customerRepo.DeleteCustomerAsync(customer);
        }

    }
}
