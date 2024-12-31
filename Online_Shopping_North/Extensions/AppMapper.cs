using AutoMapper;
using Online_Shopping_North.DTOs;
using Online_Shopping_North.Entities;
using Online_Shopping_North.Requests;

namespace Online_Shopping_North.Extensions
{
    public class AppMapper : Profile
    {
        public AppMapper()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<CategoryDTO, RequestCategory>().ReverseMap();
            CreateMap<Category, RequestCategory>().ReverseMap();

            CreateMap<City, CityDTO>();
            CreateMap<CityDTO, City>();

            CreateMap<RegionDTO, Region>().ReverseMap();

            CreateMap<District, DistrictDTO>().ReverseMap();

            CreateMap<Payment, PaymentDTO>();
            CreateMap<PaymentDTO, Payment>();

            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<RequestProduct, ProductDTO>().ReverseMap();
            CreateMap<RequestProduct, Product>()
                .ForMember(dest => dest.Image,
                            opt => opt.Ignore());

            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<RequestCustomer, CustomerDTO>().ReverseMap();
            CreateMap<RequestCustomer, Customer>()
                .ForMember(dest => dest.Dob,
                            opt => opt.Ignore())
                .ForMember(dest => dest.Picture,
                            opt => opt.Ignore())
                .ReverseMap();
            CreateMap<DistributedCustomer, Address>()
                .ForMember(dest => dest.Id,
                            opt => opt.Ignore());
            CreateMap<DistributedCustomer, Customer>();

            CreateMap<Order, OrderCartDTO>().ReverseMap();
            CreateMap<Order, OrderBillDTO>().ReverseMap();
            CreateMap<Item, ItemDTO>().ReverseMap();

            CreateMap<Product, ItemDTO>()
                .ForMember(dest => dest.ProductId,
                            opt => opt.MapFrom(src => src.Id));


            CreateMap<ShippingMethod, ShippingDTO>().ReverseMap();

            CreateMap<RequestBranch, BranchDTO>().ReverseMap();
            CreateMap<BranchDTO, Branch>().ReverseMap();
            CreateMap<RequestBranch, Branch>();
            CreateMap<RequestBranch, Address>();
            CreateMap<BranchDTO, Address>().ReverseMap();

            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<RequestEmployee, EmployeeDTO>();
            CreateMap<RequestEmployee, Employee>()
                .ForMember(dest => dest.Dob,
                            opt => opt.Ignore());

            CreateMap<RequestEmployee, Address>()
                .ForMember(dest => dest.BranchId,
                            opt => opt.Ignore());

            CreateMap<RequestVoucher, Voucher>()
                .ForMember(dest => dest.Id,
                            opt => opt.Ignore())
                .ForMember(dest => dest.Code,
                            opt => opt.MapFrom(src => src.Code.ToUpper()));
            CreateMap<VoucherDTO, Voucher>().ReverseMap();
            CreateMap<VoucherDTO, RequestVoucher>().ReverseMap();

        }
    }
}
