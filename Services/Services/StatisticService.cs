using AutoMapper;
using DTOs.DTOs;
using Entities.Entities;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;

namespace Services.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IBillRepo _billRepo;
        private readonly IProductRepo _productRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;

        public StatisticService(IBillRepo billRepo, IProductRepo productRepo,
            ICategoryRepo categoryRepo,
            IMapper mapper) 
        {
            _billRepo = billRepo;
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        private async Task MapProductToItemDTO(OrderBillDTO orderBillDTO)
        {
            foreach (var item in orderBillDTO.Items)
            {
                var product = await _productRepo.GetProductByIdAsync(item.ProductId);
                _mapper.Map(product, item);
            }
        }

        private async Task<List<OrderBillDTO>> MapListOrderBill(List<Order> orders)
        {
            List<OrderBillDTO> list = new List<OrderBillDTO>();

            for (int i = 0; i < orders.Count; i++)
            {
                list.Add(_mapper.Map<OrderBillDTO>(orders[i]));
                if (list[i].Items != null)
                    await MapProductToItemDTO(list[i]);
            }
            return list;
        }

        private void revenue(ItemDTO item, Dictionary<string, decimal?> revenueByCategory, string name)
        {
            decimal? itemRevenue = item.Quantity * item.Price;

            if (revenueByCategory.ContainsKey(name))
            {
                revenueByCategory[name] += itemRevenue;
            }
            else
            {
                revenueByCategory[name] = itemRevenue;
            }
        }

        private async Task<Dictionary<string, decimal?>> GetRevenue(List<OrderBillDTO> orderBillDTOs)
        {
            Dictionary<string, decimal?> revenueByCategory = new Dictionary<string, decimal?>();

            foreach (var order in orderBillDTOs)
            {
                foreach (var item in order.Items)
                {
                    Guid categoryId = await _productRepo.GetCategoryIdByProductId(item.ProductId);

                    string categoryName = await _categoryRepo.GetCategoryNameAsync(categoryId);

                    revenue(item, revenueByCategory, categoryName);
                }
            }

            return revenueByCategory;
        }
        
        private async Task<Dictionary<string, decimal?>> GetRevenueCategory(Guid categoryId, 
            List<OrderBillDTO> orderBillDTOs)

        {
            Dictionary<string, decimal?> revenueByCategory = new Dictionary<string, decimal?>();

            for (int i = 0; i < orderBillDTOs.Count; i++)
            {
                foreach (var item in orderBillDTOs[i].Items)
                {
                    if (await _productRepo.GetCategoryIdByProductId(item.ProductId) == categoryId)
                    {
                        string productName = await _productRepo.GetProductNameByIdAsync(item.ProductId);

                        revenue(item, revenueByCategory, productName);
                    }
                }
            }

            return revenueByCategory;
        }

        public async Task<Dictionary<string, decimal?>> GetRevenueCategoriesInYear(int year)
        {
            var orderList = await _billRepo.GetCompletedBillInYearAsync(year);

            List<OrderBillDTO> list = await MapListOrderBill(orderList);

            return await GetRevenue(list);
        }

        public async Task<Dictionary<string, decimal?>> GetRevenueCategoriesInYearMonth(int year, int month)
        {
            var orderList = await _billRepo.GetCompletedBillInYearMonthAsync(year, month);

            List<OrderBillDTO> list = await MapListOrderBill(orderList);

            return await GetRevenue(list);
        }

        public async Task<Dictionary<string, decimal?>> GetRevenueByCategoriesInYearMonth(Guid categoryId, 
            int year, int month)

        {
            var orderList = await _billRepo.GetCompletedBillInYearMonthAsync(year, month);

            List<OrderBillDTO> list = await MapListOrderBill(orderList);

            return await GetRevenueCategory(categoryId, list);
        }

        public async Task<Dictionary<string, decimal?>> GetRevenueByCategoriesInYear(Guid categoryId, int year)
        {
            var orderList = await _billRepo.GetCompletedBillInYearAsync(year);

            List<OrderBillDTO> list = await MapListOrderBill(orderList);
            
            return await GetRevenueCategory(categoryId, list);
        }
    }
}
