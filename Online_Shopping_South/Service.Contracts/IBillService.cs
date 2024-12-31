using Online_Shopping_South.DTOs;
using Online_Shopping_South.Entities;
using Online_Shopping_South.Requests;

namespace Online_Shopping_South.Service.Contracts
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
