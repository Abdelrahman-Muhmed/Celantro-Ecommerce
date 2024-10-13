using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceCoffe_DAL.Models.Product
{
    public class Products : BaseEntity
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Picture URL is required")]
        public string PictureUrl { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Brand is required")]
        public int BrandId { get; set; } // FK ProductBrand
        public ProductBrand ProductBrand { get; set; } //Navigational Prop (One) ==> For Table ProductBrand

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; } // FK ProdactCategory 
        public ProductCategory CategoryName { get; set; }  //Navigational Prop (One) ==> For Table ProdactCategory 

        //I will Handle FK By Fluint Api 

    }
}
