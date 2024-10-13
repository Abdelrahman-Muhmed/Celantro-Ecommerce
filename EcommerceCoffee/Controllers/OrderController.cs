using EcommerceCoffee_BLL.Service.IOrderService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EcommerceCoffee.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderServ _orderServ;

        public OrderController(IOrderServ orderServ)
        {
            _orderServ = orderServ;
        }

       
    }
}