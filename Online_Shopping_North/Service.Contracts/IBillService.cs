using Online_Shopping_North.DTOs;
using Online_Shopping_North.Entities;
using Online_Shopping_North.Requests;

namespace Online_Shopping_North.Service.Contracts
{
    public interface IBillService
    {
        Task<Order> CartToBill(Guid customerId, Guid orderId, RequestBill requestBill);
        Task<List<OrderBillDTO>> GetOrderBill(Guid id);
        Task<List<OrderBillDTO>> EmployeeGetPendingBillList();
        Task<List<OrderBillDTO>> EmployeeGetCompletedBillList();
        Task<List<OrderBillDTO>> CustomerGetPendingBillList(Guid customerId);
        Task<List<OrderBillDTO>> CustomerGetCompletedBillList(Guid customerId);
        Task<OrderBillDTO?> GetBillDetail(Guid orderId);
    }
}
