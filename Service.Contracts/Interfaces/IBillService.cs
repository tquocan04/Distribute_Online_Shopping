using DTOs.DTOs;
using DTOs.Request;
using Entities.Entities;

namespace Service.Contracts.Interfaces
{
    public interface IBillService
    {
        Task<(Order, Order)> CartToBill(Guid customerId, RequestBill requestBill);
        Task<List<OrderBillDTO>> GetOrderBill(Guid id);
        Task<List<OrderBillDTO>> EmployeeGetPendingBillList();
        Task<List<OrderBillDTO>> EmployeeGetCompletedBillList();
        Task<List<OrderBillDTO>> CustomerGetPendingBillList(Guid customerId);
        Task<List<OrderBillDTO>> CustomerGetCompletedBillList(Guid customerId);
        Task<OrderBillDTO?> GetBillDetail(Guid orderId);
    }
}
