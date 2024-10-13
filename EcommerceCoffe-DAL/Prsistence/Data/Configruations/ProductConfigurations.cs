using EcommerceCoffe_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using EcommerceCoffe_DAL.Models.Product;

namespace Amazon_EF.Data.Config
{
    internal class ProductConfigurations : EntityTypeConfiguration<Products>
    {
        public ProductConfigurations()
        {
            Property(p => p.Name)
                .IsRequired();

            Property(p => p.Description)
                .IsRequired();

            Property(p => p.PictureUrl)
                .IsRequired();

            Property(p => p.Price)
                   .HasPrecision(12, 2);

            HasRequired(p => p.ProductBrand)
                .WithMany()
                .HasForeignKey(p => p.BrandId);



            HasRequired(p => p.CategoryName)
                .WithMany()
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
