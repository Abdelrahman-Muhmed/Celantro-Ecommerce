using EcommerceCoffe_DAL.Models.Product;
using EcommerceCoffe_DAL.Prsistence.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;
using EcommerceCoffe_DAL.Prsistence.Repositories.ProductRepo;
using System.Linq;
using System;
using EcommerceCoffe_DAL.Prsistence.Repositories.GenaricRepo;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EcommerceCoffee_BLL.DTOs;
using System.IO;
using System.Web;

namespace EcommerceCoffee_BLL.Service.IProductService
{
    public class ProductServ : IProductServ
    {
        private readonly IGenaricRepo<Products> _GenaricRepoProduct;
        private readonly IGenaricRepo<ProductCategory> _GenaricRepoProductCategory;
        private readonly IGenaricRepo<ProductBrand> _GenaricRepoProductBrand;
        private readonly IMapper _mapper;

        public ProductServ(IGenaricRepo<Products> GenaricRepoProduct,
                           IGenaricRepo<ProductCategory> GenaricRepoProductCategory,
                           IGenaricRepo<ProductBrand> GenaricRepoProductBrand,
                           IMapper mapper)

        {
            _GenaricRepoProduct = GenaricRepoProduct;
            _GenaricRepoProductCategory = GenaricRepoProductCategory;
            _GenaricRepoProductBrand = GenaricRepoProductBrand;
            _mapper = mapper;

        }

        //Get All Products
        public async Task<IReadOnlyList<ProductRetuenDto>> GetAllProductsAsync()
        {
                var query = _GenaricRepoProduct.GetAll()
                    .Include(p => p.ProductBrand)
                    .Include(p => p.CategoryName);

            var productDtos = await query
             .ProjectTo<ProductRetuenDto>(_mapper.ConfigurationProvider)
             .ToListAsync();

            return productDtos;
        }

        //Get All Category
        public async Task<IEnumerable<ProductCategory>> GetAllCategory()
        {
      
                var query = _GenaricRepoProductCategory.GetAll();
                var result = await query.ToListAsync();
                return result;
   
        }

        //Get All Brand
        public async Task<IEnumerable<ProductBrand>> GetAllBrand()
        {

                var query = _GenaricRepoProductBrand.GetAll();
                var result = await query.ToListAsync();
                return result;

        }

        //Get Product By Id
        public async Task<ProductRetuenDto> GetProductId(int id)
        {
           
             var product = await _GenaricRepoProduct.GetByIdAsync(id);

            if(product == null)
            {
                return null;
            }
            
            var productDto = _mapper.Map<ProductRetuenDto>(product);
                return productDto; // Return the product with its details already included
    
        }
        public async Task<Products> GetProductForCartByIdAsync(int id)
        {
            var product = await _GenaricRepoProduct.GetAll()
                                .Include(p => p.ProductBrand)
                                .Include(p => p.CategoryName)
                                .FirstOrDefaultAsync(p => p.id == id);

            if (product == null)
            {
                return null;
            }

            return product;
        }

        public async Task<ProductDto> GetProductDtoById(int id)
        {
            var product = await _GenaricRepoProduct.GetByIdAsync(id);

            if (product == null)
            {
                return null;
            }

            var productDto = _mapper.Map<ProductDto>(product);
            return productDto; // Return the product with its details already included
        }

        //Get CreateProducts 
        public async Task<ProductRetuenDto> CreateProducts(ProductDto productDto)
        {
            

            Products product = _mapper.Map<Products>(productDto);

            if(productDto.ImageFile != null && productDto.ImageFile.ContentLength > 0)
            {
                string fileName = Path.GetFileNameWithoutExtension(productDto.ImageFile.FileName);
                string extention = Path.GetExtension(productDto.ImageFile.FileName);

                fileName = $"{fileName}_{DateTime.Now:yyyyMMddHHmmssfff}{extention}";
                product.PictureUrl = $"/Images/{fileName}";

                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/"), fileName);

                productDto.ImageFile.SaveAs(path);
            }

            var createdProduct = await _GenaricRepoProduct.CreateAsync(product);
            var createdProductDto = _mapper.Map<ProductRetuenDto>(createdProduct);
            return createdProductDto;
    
        }

        //Update Product
        public async Task<ProductRetuenDto> UpdateProduct(ProductDto productDto)
        {
        
           

            Products product = _mapper.Map<Products>(productDto);

            if (productDto.ImageFile != null && productDto.ImageFile.ContentLength > 0)
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
                string fileName = Path.GetFileNameWithoutExtension(productDto.ImageFile.FileName);
                string extention = Path.GetExtension(productDto.ImageFile.FileName);

                fileName = $"{fileName}_{DateTime.Now:yyyyMMddHHmmssfff}{extention}";
                product.PictureUrl = $"/Images/{fileName}";

                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/"), fileName);

                productDto.ImageFile.SaveAs(path);
            }
            var updatedProduct = await _GenaricRepoProduct.updateAsync(product);
            var updatedProductDto = _mapper.Map<ProductRetuenDto>(updatedProduct);
            return updatedProductDto;
        }

        //Delete Product 
        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                var result = await _GenaricRepoProduct.GetByIdAsync(id);

                if (result != null)
                    return await _GenaricRepoProduct.DeleteAsync(id);

                return false;
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while Delete Products: {ex.Message}", ex);

            }
        }

     
    }
}