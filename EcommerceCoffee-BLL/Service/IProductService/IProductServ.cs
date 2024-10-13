using EcommerceCoffe_DAL.Models.Product;
using EcommerceCoffee_BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceCoffee_BLL.Service.IProductService
{
    public interface IProductServ
    {
        Task<IReadOnlyList<ProductRetuenDto>> GetAllProductsAsync();
       
        Task<IEnumerable<ProductBrand>> GetAllBrand();
        Task<IEnumerable<ProductCategory>> GetAllCategory();

        Task<ProductRetuenDto> GetProductId(int id);
        Task<Products> GetProductForCartByIdAsync(int id);

        Task<ProductDto> GetProductDtoById(int id);


        Task<ProductRetuenDto> CreateProducts(ProductDto product);

        Task<ProductRetuenDto> UpdateProduct(ProductDto product);

        Task<bool> DeleteProduct(int id);


    
    }
}
