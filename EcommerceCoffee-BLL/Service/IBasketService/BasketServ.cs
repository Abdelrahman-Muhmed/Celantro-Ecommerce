using EcommerceCoffe_DAL.Models.BasketModel;
using EcommerceCoffe_DAL.Prsistence.Repositories.GenaricRepo;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq; // Ensure this is included
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace EcommerceCoffee_BLL.Service.IBasketService
{
    public class BasketServ : IBasketServ
    {
        private readonly IGenaricRepo<BasketItem> _GenaricBasketItemRepo;

        public BasketServ(IGenaricRepo<BasketItem> GenaricBasketItemRepo)
        {
            _GenaricBasketItemRepo = GenaricBasketItemRepo;
        }

        private string GetCurrentUserId()
        {
            // Use HttpContext.Current to get the current user
            if (HttpContext.Current != null && HttpContext.Current.User != null)
            {
                return HttpContext.Current.User.Identity.GetUserId(); 
            }
            return null;
        }

        public async Task<BasketItem> AddToCartAsync(BasketItem basketItem, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new InvalidOperationException("User ID cannot be null or empty."); // Handle appropriately
            }

            // Set the UserId for the basket item
            basketItem.UserId = userId;

            var result = await _GenaricBasketItemRepo.CreateAsync(basketItem);
            return result;
        }

        public async Task ClearBasketAsync(string userId)
        {
            var basketItems = _GenaricBasketItemRepo.GetAll().Where(b => b.UserId == userId).ToList();
            foreach (var item in basketItems)
            {
                await _GenaricBasketItemRepo.DeleteAsync(item.id);
            }
        }

        public async Task<IEnumerable<BasketItem>> GetAllBasketItemAsync(string userId)
        {
            var query = _GenaricBasketItemRepo.GetAll().Where(b => b.UserId == userId);
            var result = await query.ToListAsync();
            return result;
        }
    }
}
