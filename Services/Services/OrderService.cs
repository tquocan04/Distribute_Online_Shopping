using AutoMapper;
using DTOs.DTOs;
using DTOs.Request;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Online_Shopping.Context;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;

namespace Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepo _orderRepo;
        private readonly IProductRepo _productRepo;

        public OrderService(IOrderRepo orderRepo, IMapper mapper, IProductRepo productRepo) 
        {
            _mapper = mapper;
            _orderRepo = orderRepo;
            _productRepo = productRepo;
        }

        public async Task AddToCart(Guid cusId, Guid prodId)
        {
            var cart = await _orderRepo.GetOrderIsCartByCusId(cusId);
            var cartId = cart.Id;
            Product product = await _productRepo.GetProductByIdAsync(prodId);
            Item existingItem = await _orderRepo.GetItem(cartId, prodId);

            if (existingItem == null)
            {
                Item item = new ()
                {
                    OrderId = cartId,
                    ProductId = prodId,
                    Quantity = 1
                };
                cart.TotalPrice += (decimal)product.Price;
                await _orderRepo.AddItemToCart(item);
            }
            else
            {
                existingItem.Quantity++;
                cart.TotalPrice += (decimal)product.Price;
                await _orderRepo.UpdateQuantityItemToCart(existingItem);
            }
            await _orderRepo.UpdateTotalPriceCart(cartId, cart.TotalPrice);
            product.Stock--;
            await _productRepo.UpdateInforProduct(product);
        }


        public async Task<Order> CreateNewCart(Guid cusId)
        {
            Order order = new ()
            {
                Id = Guid.NewGuid(),
                CustomerId = cusId,
                TotalPrice = 0,
            };
            await _orderRepo.CreateOrder(order);
            return order;
        }

        public async Task DeleteItemInCart(Guid cusId, Guid prodId)
        {
            var cart = await _orderRepo.GetOrderIsCartByCusId(cusId);
            var product = await _productRepo.GetProductByIdAsync(prodId);
            
            Item item = await _orderRepo.GetItem(cart.Id, product.Id);
            
            await _orderRepo.DeleteItemInCart(item);

            cart.TotalPrice -= (decimal)(product.Price * item.Quantity);

            cart.ResetTotalPrice(cart.TotalPrice);

            await _orderRepo.UpdateTotalPriceCart(cart.Id, cart.TotalPrice);

            product.Stock += item.Quantity;
            await _productRepo.UpdateInforProduct(product);

        }

        public async Task<OrderCartDTO> GetOrderCart(Guid cusId)
        {
            var cart = await _orderRepo.GetOrderIsCartByCusId(cusId);
            var cartDTO = _mapper.Map<OrderCartDTO>(cart);

            if (cartDTO.Items != null)
            {
                foreach (var item in cartDTO.Items)
                {
                    var product = await _productRepo.GetProductByIdAsync(item.ProductId);
                    _mapper.Map(product, item);
                }
            }
            
            return cartDTO;
        }

        public async Task<OrderCartDTO> MergeCartFromClient(Guid cusId, List<RequestItems> items)
        {
            Order order = await _orderRepo.GetOrderIsCartByCusId(cusId);
            Guid cartId = order.Id;
            OrderCartDTO cartDTO = _mapper.Map<OrderCartDTO>(order);

            if (cartDTO.Items.Count != 0)
            {
                foreach (var item in cartDTO.Items)
                {
                    var product = await _productRepo.GetProductByIdAsync(item.ProductId);
                    _mapper.Map(product, item);
                }
            }
            bool check = true;

            for (int i = 0; i < items.Count; i++)
            {
                Product product = await _productRepo.GetProductByIdAsync(items[i].ProductId);
                if (product == null)
                    return null;

                int quantity = (items[i].Quantity > product.Stock ? product.Stock : items[i].Quantity);
                if (cartDTO.Items.Count != 0)
                {
                    foreach (var item in cartDTO.Items)
                    {
                        if (items[i].ProductId == item.ProductId)
                        {
                            order.TotalPrice -= item.Quantity * product.Price;

                            order.ResetTotalPrice(order.TotalPrice);

                            if (item.Quantity + items[i].Quantity > product.Stock)
                                item.Quantity = product.Stock;
                            else
                                item.Quantity += items[i].Quantity;

                            order.IncreaseTotalPrice(item.Quantity, product.Price);

                            Item existingItem = await _orderRepo.GetItem(cartId, items[i].ProductId);
                            existingItem.Quantity = item.Quantity;
                            await _orderRepo.UpdateQuantityItemToCart(existingItem);

                            check = true;
                            break;
                        }
                        else
                        {
                            check = false;
                        }

                    }
                    if (!check)
                    {
                        Item item = new ()
                        {
                            OrderId = cartId,
                            ProductId = items[i].ProductId,
                            Quantity = quantity
                        };
                        order.IncreaseTotalPrice(item.Quantity, product.Price);
                        await _orderRepo.AddItemToCart(item);
                    }
                }
                else
                {
                    Item item = new ()
                    {
                        OrderId = cartId,
                        ProductId = items[i].ProductId,
                        Quantity = quantity
                    };
                    order.IncreaseTotalPrice(item.Quantity, product.Price);
                    await _orderRepo.AddItemToCart(item);
                }
                product.Stock -= quantity;
                await _productRepo.UpdateInforProduct(product);
            }
            await _orderRepo.UpdateTotalPriceCart(cartId, order.TotalPrice);

            Order neworder = await _orderRepo.GetOrderIsCartByCusId(cusId);
            cartDTO = _mapper.Map<OrderCartDTO>(neworder);
            foreach (var item in cartDTO.Items)
            {
                var product = await _productRepo.GetProductByIdAsync(item.ProductId);
                _mapper.Map(product, item);
            }
            return cartDTO;
        }

        public async Task<bool> UpdateQuantityItem(Guid cusId, Guid prodId, int Quantity)
        {
            var cart = await _orderRepo.GetOrderIsCartByCusId(cusId);
            var cartId = cart.Id;
            Product product = await _productRepo.GetProductByIdAsync(prodId);
            Item existingItem = await _orderRepo.GetItem(cartId, prodId);

            if (Quantity > product.Stock)
                return false;

            cart.TotalPrice -= (decimal)(existingItem.Quantity * product.Price);
            if (cart.TotalPrice < 0)
                cart.TotalPrice = 0;
            product.Stock += existingItem.Quantity;

            existingItem.Quantity = Quantity;
            await _orderRepo.UpdateQuantityItemToCart(existingItem);

            //update total price
            cart.IncreaseTotalPrice(existingItem.Quantity, product.Price);
            await _orderRepo.UpdateTotalPriceCart(cartId, cart.TotalPrice);

            //update product stock
            product.Stock -= existingItem.Quantity;
            await _productRepo.UpdateInforProduct(product);
            return true;
        }
    }
}
