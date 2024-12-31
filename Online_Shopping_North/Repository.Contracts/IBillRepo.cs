using Online_Shopping_North.Entities;

namespace Online_Shopping_North.Repository.Contracts
{
    public interface IBillRepo
    {
        Task CartToBillAsync(Order order);
        Task<List<Order>> GetListBillForCustomerAsync(Guid customerId);
        Task<Order?> GetBillDetailForEmployeeAsync(Guid orderId);
        Task<List<Order>> GetListPendingBillAsync();
        Task<List<Order>> GetListCompletedBillAsync();
        Task<List<Order>> CustomerGetListPendingBillAsync(Guid customerId);
        Task<List<Order>> CustomerGetListCompletedBillAsync(Guid customerId);
        Task CompletedBillAsync(Guid id);
    }
}
