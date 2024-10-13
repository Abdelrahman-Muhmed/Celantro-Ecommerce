using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceCoffe_DAL.Models.BasketModel
{
    public class CustomerBasket : BaseEntity
    {
        [StringLength(100, ErrorMessage = "Client secret cannot exceed 100 characters.")]
        public string? ClientSecret { get; set; }

        [Display(Name = "Delivery Method")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid delivery method.")]
        public int? DeliveryMethodId { get; set; }

        [Display(Name = "Shipping Price")]
        [Range(0, double.MaxValue, ErrorMessage = "Shipping price cannot be negative.")]
        public decimal ShippingPriceBasket { get; set; }

        [Required(ErrorMessage = "Basket items are required.")]
        [MinLength(1, ErrorMessage = "At least one basket item is required.")]
        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();

    }
}
