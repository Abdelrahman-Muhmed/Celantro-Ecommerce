using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceCoffe_DAL.Models.OrderModel
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
    }
}