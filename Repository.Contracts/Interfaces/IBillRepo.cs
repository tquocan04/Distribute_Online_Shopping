using Entities.Entities;

namespace Repository.Contracts.Interfaces
{
    public interface IBillRepo
    {
        Task CartToBillAsync(Order order);
        Task<Guid?> GetCustomerIdOfBillAsync(Guid orderId);
        Task<List<Order>> GetListBillForCustomerAsync(Guid customerId);
        Task<Order?> GetBillDetailForEmployeeAsync(Guid orderId);
        Task<List<Order>> GetListPendingBillAsync();
        Task<List<Order>> GetListCompletedBillAsync();
        Task<List<Order>> CustomerGetListPendingBillAsync(Guid customerId);
        Task<List<Order>> CustomerGetListCompletedBillAsync(Guid customerId);
        Task CompletedBillAsync(Guid id);
        Task<List<Order>> GetCompletedBillInYearMonthAsync(int year, int month);
        Task<List<Order>> GetCompletedBillInYearAsync(int year);
        Task<List<Order>> GetCompletedBillByCategoryInYearMonthAsync(Guid categoryId, int year, int month);
        Task<List<Order>> GetCompletedBillByCategoryInYearAsync(Guid categoryId, int year);
    }
}
