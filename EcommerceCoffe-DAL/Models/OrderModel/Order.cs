using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceCoffe_DAL.Models.OrderModel
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}