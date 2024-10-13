

using System;
using System.Collections.Generic;

namespace Amazon_Api.Dtos
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }

        public string ByerEmail { get; set; } 
        public DateTimeOffset dateTime { get; set; } = DateTimeOffset.UtcNow;

        public string orderStatus { get; set; }    //Handle it in Configure

        //DeliveryMethode 
        public string DeliveryMethod { get; set; }
        public decimal cost { get; set; }

        public ICollection<OrderItemToReturnDto> orderItem { get; set; } //Handle it in Configure

        public decimal subTotal { get; set; }

        public decimal Total => subTotal + cost;

        //public decimal GetTotal() => subTotal + deliveryMethod.cost;

        public string paymentIntedId { get; set; } = string.Empty;

    }
}
