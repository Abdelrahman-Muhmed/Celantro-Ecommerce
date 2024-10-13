using EcommerceCoffe_DAL.Models.BasketModel;
using EcommerceCoffe_DAL.Models.OrderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceCoffee_BLL.Service.IOrderService
{
    public interface IOrderServ
    {
        Task CreateOrderAsync(string userId, IEnumerable<BasketItem> basketItems);
    }
}
