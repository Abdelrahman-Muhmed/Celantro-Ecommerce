using EcommerceCoffe_DAL.Models.OrderModel;
using EcommerceCoffe_DAL.Models.Product;
using EcommerceCoffe_DAL.Prsistence.Data;
using EcommerceCoffe_DAL.Prsistence.Repositories;
using EcommerceCoffe_DAL.Prsistence.Repositories.GenaricRepo;
using EcommerceCoffe_DAL.Prsistence.Repositories.ProductRepo;
using EcommerceCoffee_BLL.Service.IProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EcommerceCoffee_BLL.Service.IBasketService;
using EcommerceCoffe_DAL.Models.BasketModel;

namespace EcommerceCoffee_BLL.Service.IOrderService
{
    public class OrderServ : IOrderServ
    {
        private readonly IBasketServ _basketService;
        private readonly IGenaricRepo<Order> _orderRepo;
        private readonly IGenaricRepo<OrderItem> _orderItemRepo;

        public OrderServ(IBasketServ basketService,
                        IGenaricRepo<Order> orderRepo,
                        IGenaricRepo<OrderItem> orderItemRepo)
        {
            _basketService = basketService;
            _orderRepo = orderRepo;
            _orderItemRepo = orderItemRepo;
           
        }

        public async Task CreateOrderAsync(string userId, IEnumerable<BasketItem> basketItems)
        {
            if (basketItems == null || !basketItems.Any())
                throw new ArgumentException("Basket is empty.");

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = basketItems.Sum(item => item.Price * item.Quantity),
                OrderItems = basketItems.Select(item => new OrderItem
                {
                    ProductName = item.ProductName,
                    PictureUrl = item.PictureUrl,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    CategoryName = item.CategoryName,
                    BrandName = item.BrandName
                }).ToList()
            };

            await _orderRepo.CreateAsync(order);
        }

    }
    
}