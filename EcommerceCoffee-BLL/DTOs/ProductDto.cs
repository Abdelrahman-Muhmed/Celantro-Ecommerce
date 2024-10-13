// EcommerceCoffee_BLL.DTOs.ProductDto.cs
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace EcommerceCoffee_BLL.DTOs
{
    public class ProductDto
    {
        // For update operations, Id is required. For create, it can be ignored.
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product description is required.")]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Brand selection is required.")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Category selection is required.")]
        public int CategoryId { get; set; }

        // For image upload
        //[AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg", ".gif" })]
        public HttpPostedFileBase ImageFile { get; set; }

        // To display the existing image in update operations
        public string ExistingImagePath { get; set; }
    }
}
