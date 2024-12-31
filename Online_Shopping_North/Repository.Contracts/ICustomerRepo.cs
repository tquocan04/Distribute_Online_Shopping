﻿using Online_Shopping_North.Entities;

namespace Online_Shopping_North.Repository.Contracts
{
    public interface ICustomerRepo
    {
        Task CreateNewCustomer(Customer customer);
        Task<Customer?> GetCustomerByIdAsync(Guid id);
        Task UpdateInforCustomer(Customer customer);
        Task DeleteCustomerAsync(Customer customer);
        Task<string> GetCustomerNameByIdAsync(Guid id);
    }
}