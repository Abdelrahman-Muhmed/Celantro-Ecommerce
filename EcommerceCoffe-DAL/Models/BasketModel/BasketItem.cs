using EcommerceCoffe_DAL.Models.OrderModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceCoffe_DAL.Models.BasketModel
{
    public class BasketItem : BaseEntity
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Picture URL is required.")]
        [Url(ErrorMessage = "Please enter a valid URL.")]
        [StringLength(200, ErrorMessage = "Picture URL cannot exceed 200 characters.")]
        public string PictureUrl { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(50, ErrorMessage = "Category name cannot exceed 50 characters.")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Brand name is required.")]
        [StringLength(50, ErrorMessage = "Brand name cannot exceed 50 characters.")]
        public string BrandName { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; }


    }
}
