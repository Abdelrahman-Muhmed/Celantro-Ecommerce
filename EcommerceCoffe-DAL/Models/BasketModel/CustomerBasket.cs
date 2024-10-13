using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceCoffe_DAL.Models.BasketModel
{
    public class CustomerBasket : BaseEntity
    {
        public string? ClientSecret { get; set; }

        //For Update DelivreryMethode 
        public int? DeliveryMethodId { get; set; }

        //Show ShippingPrice
        public decimal shippingPriceBasket { get; set; }

        public List<BasketItem> basketItem { get; set; }
 
    }
}
