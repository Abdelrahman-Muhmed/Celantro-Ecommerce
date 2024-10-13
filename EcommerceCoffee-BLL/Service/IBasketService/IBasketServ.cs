using EcommerceCoffe_DAL.Models.BasketModel;
using EcommerceCoffe_DAL.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceCoffee_BLL.Service.IBasketService
{
    public interface IBasketServ
    {
        Task<IEnumerable<BasketItem>> GetAllBasketItemAsync(string userId);
        Task<BasketItem> AddToCartAsync(BasketItem basketItem, string userId);
        Task ClearBasketAsync(string userId);
    }
}
