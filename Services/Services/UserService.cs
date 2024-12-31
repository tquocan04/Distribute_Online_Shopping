using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DTOs.DTOs;
using DTOs.MongoDb;
using DTOs.Request;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Repository.Contracts.Interfaces;
using Service.Contracts;
using Service.Contracts.Interfaces;

namespace Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly IAddressRepo _addressRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly IAddressService<CustomerDTO> _addressService;
        private readonly IMetadataService _metadataService;
        private readonly Cloudinary _cloudinary;

        public UserService
            (IUserRepo userRepo,
            IMapper mapper,
            IAddressRepo addressRepo,
            IOrderRepo orderRepo,
            IAddressService<CustomerDTO> addressService,
            IMetadataService metadataService,
            Cloudinary cloudinary
            )
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _addressRepo = addressRepo;
            _orderRepo = orderRepo;
            _addressService = addressService;
            _metadataService = metadataService;
            _cloudinary = cloudinary;

        }

        private async Task<CustomerMetadata> ConvertCustomerToCustomerMetadata(Customer customer)
        {
            CustomerDTO cusDTO = _mapper.Map<CustomerDTO>(customer);
            cusDTO = await _addressService.SetAddress(cusDTO, cusDTO.Id);
            var cusMetadata = _mapper.Map<CustomerMetadata>(cusDTO);
            cusMetadata.Id = customer.Id.ToString();
            return cusMetadata;
        }


        public async Task<Customer> CreateNewUser(RequestCustomer requestCustomer)
        {
            Customer customer = new Customer
            {
                Id = Guid.NewGuid(),
                Dob = new DateOnly(requestCustomer.Year, requestCustomer.Month, requestCustomer.Day)
            };

            // Xử lý ảnh và tải lên Cloudinary nếu có
            if (requestCustomer.Picture != null && requestCustomer.Picture.Length > 0)
            {
                await UploadImage(customer, requestCustomer.Picture);
            }

            _mapper.Map(requestCustomer, customer);
            await _userRepo.CreateNewCustomer(customer);

            CustomerAddress customerAddress = new CustomerAddress(customer.Id);
            _mapper.Map(requestCustomer, customerAddress);
                        
            await _addressRepo.CreateNewAddress(customerAddress);

            await _metadataService.CreateCustomerMetadataAsync(await ConvertCustomerToCustomerMetadata(customer));
            return customer;
        }

        public async Task<Customer> UpdateInforUser(Customer customer, RequestCustomer requestCustomer)
        {
            if (!_userRepo.checkDOB(requestCustomer.Year)
                || !await _userRepo.checkEmailById(customer.Id, requestCustomer.Email))
            {
                return null;
            }

            // truy van tim record:
            var address = await _addressRepo.GetAddressByObjectIdAsync(customer.Id);

            if (address.DistrictId != requestCustomer.DistrictId || address.Street != requestCustomer.Street)
            {
                _mapper.Map(requestCustomer,address);
                await _addressRepo.UpdateAddress(address);
            }

            _mapper.Map(requestCustomer, customer);

            if (requestCustomer.Picture != null && requestCustomer.Picture.Length > 0)
            {
                await UploadImage(customer, requestCustomer.Picture);
            }


            customer.Dob = new DateOnly(requestCustomer.Year, requestCustomer.Month, requestCustomer.Day);

            await _userRepo.UpdateInforCustomer(customer);

            await _metadataService.UpdateCustomerMetadataAsync(await ConvertCustomerToCustomerMetadata(customer));

            return customer;
        }

        public async Task<Customer> CreateNewUserByGoogle(RequestSignupGoogle requestSignupGoogle)
        {
            Customer customer = new()
            {
                Id = Guid.NewGuid(),
                Dob = new DateOnly(requestSignupGoogle.year, requestSignupGoogle.month, requestSignupGoogle.day)
            };

            _mapper.Map(requestSignupGoogle, customer);
            await _userRepo.CreateNewCustomer(customer);

            Address address = new()
            {
                Id = Guid.NewGuid(),
                CustomerId = customer.Id,
                IsDefault = true,
            };
            _mapper.Map(requestSignupGoogle, address);

            await _addressRepo.CreateNewAddress(address);

            await _metadataService.CreateCustomerMetadataAsync(await ConvertCustomerToCustomerMetadata(customer));
            return customer;
        }

        public async Task<CustomerDTO> GetProfileUser(Guid cusId)
        {
            var cus = await _userRepo.GetCustomerByIdAsync(cusId);

            if (cus != null)
            {
                CustomerDTO cusDTO = _mapper.Map<CustomerDTO>(cus);
                cusDTO.Year = cus.Dob.Year;
                cusDTO.Month = cus.Dob.Month;
                cusDTO.Day = cus.Dob.Day;

                cusDTO = await _addressService.SetAddress(cusDTO, cusDTO.Id);
                return cusDTO;
            }

            return null;

        }

        public async Task UploadImage(Customer customer, IFormFile file)
        {
            var fileName = $"cusonlineshopping_{file.FileName}";
            var filePath = Path.Combine(Path.GetTempPath(), fileName);

            // luu tam cua he thong C:\Users\[UserName]\AppData\Local\Temp
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // upload anh len Cloudinary
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileName, filePath),
                PublicId = $"customer_{Guid.NewGuid()}"
            };

            var uploadResult = _cloudinary.Upload(uploadParams);
            var imageUrl = uploadResult.SecureUrl.AbsoluteUri;

            // luu url
            customer.Picture = imageUrl;

            // xoa file tam sau khi upload
            System.IO.File.Delete(filePath);
        }
    }
}