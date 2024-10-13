using EcommerceCoffe_DAL.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EcommerceCoffee_BLL.Service.ICategorySevice
{
    public interface ICategoryServ
    {
        Task<IEnumerable<ProductCategory>> GetAllCategory();

        Task<ProductCategory> GetCategoryById(int id);

        Task<ProductCategory> CreateCatgories(ProductCategory product);

        Task<ProductCategory> UpdateCategory(ProductCategory product);

        Task<bool> DeleteCategory(int id);
    }
}