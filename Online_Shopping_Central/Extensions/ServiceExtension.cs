using Online_Shopping_Central.Repositories;
using Online_Shopping_Central.Repository.Contracts;
using Online_Shopping_Central.Service.Contracts;
using Online_Shopping_Central.Services;

namespace Online_Shopping_Central.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IVoucherService, VoucherService>();

            services.AddScoped(typeof(IAddressService<>), typeof(AddressService<>));

            return services;
        }
        public static IServiceCollection ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IAddressRepo, AddressRepo>();
            services.AddScoped<IBranchRepo, BranchRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<ICustomerRepo, CustomerRepo>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IBillRepo, BillRepo>();
            services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            services.AddScoped<IVoucherRepo, VoucherRepo>();
            services.AddScoped<ICredentialRepo, CredentialRepo>();

            return services;
        }
    }
}
