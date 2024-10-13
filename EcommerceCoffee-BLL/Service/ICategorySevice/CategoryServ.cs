using EcommerceCoffe_DAL.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EcommerceCoffe_DAL.Prsistence.Repositories;
using System.Data.Entity;
using EcommerceCoffe_DAL.Prsistence.Repositories.GenaricRepo;
using System.Web.Mvc;
using EcommerceCoffee_BLL.DTOs;
using System.IO;

namespace EcommerceCoffee_BLL.Service.ICategorySevice
{
    public class CategoryServ : ICategoryServ
    {
        private readonly IGenaricRepo<ProductCategory> _GenaricRepoCategory;

        public CategoryServ(IGenaricRepo<ProductCategory> genaricRepoProduct)
        {
            _GenaricRepoCategory = genaricRepoProduct;
        }

        //Get Category 
        public async Task<IEnumerable<ProductCategory>> GetAllCategory()
        {
            var query = _GenaricRepoCategory.GetAll();
            var result = await query.ToListAsync();
            return result;
           
        }

        public async Task<ProductCategory> GetCategoryById(int id)
        {
            var query = await _GenaricRepoCategory.GetByIdAsync(id);  
            return query;
        }

        public async Task<ProductCategory> CreateCatgories(ProductCategory product)
        {
            if (product.ImageFile != null && product.ImageFile.ContentLength > 0)
            {
                
                string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                string extension = Path.GetExtension(product.ImageFile.FileName);

                fileName = $"{fileName}_{DateTime.Now:yyyyMMddHHmmssfff}{extension}";

                product.PictureUrl = fileName; 

                
                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/"), fileName);

                
                product.ImageFile.SaveAs(path);
            }

            
            var createdProduct = await _GenaricRepoCategory.CreateAsync(product);

            return createdProduct;
        }

        public async Task<ProductCategory> UpdateCategory(ProductCategory product)
        {
            if (product.ImageFile != null && product.ImageFile.ContentLength > 0)
            {
                // Optionally delete the old image
                if (!string.IsNullOrEmpty(product.PictureUrl))
                {
                    var oldImagePath = HttpContext.Current.Server.MapPath(product.PictureUrl);
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                }
                string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                string extention = Path.GetExtension(product.ImageFile.FileName);

                fileName = $"{fileName}_{DateTime.Now:yyyyMMddHHmmssfff}{extention}";
                product.PictureUrl = $"/Images/{fileName}";

                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/"), fileName);

                product.ImageFile.SaveAs(path);
            }
            var updatedProduct = await _GenaricRepoCategory.updateAsync(product);
         
            return updatedProduct;
        }
        public async Task<bool> DeleteCategory(int id)
        {
            var result = await _GenaricRepoCategory.GetByIdAsync(id);

            if (result != null)
                return await _GenaricRepoCategory.DeleteAsync(id);

            return false;
        }

    }
}