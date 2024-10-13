using EcommerceCoffe_DAL.Prsistence.Data;
using EcommerceCoffe_DAL.Models.OrderModel;
using EcommerceCoffe_DAL.Models.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Amazon_EF.Data.Config;
using EcommerceCoffe_DAL.Prsistence.Data.Configruations;
using EcommerceCoffe_DAL.Models.BasketModel;
using EcommerceCoffe_DAL.Model.IdentityModel;

namespace EcommerceCoffe_DAL.Prsistence.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext() : base("name=CoffeeAppDB")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

         
            modelBuilder.Configurations.Add(new ProductBrandConfiguration());
            modelBuilder.Configurations.Add(new ProductCategoryConfiguration());
            modelBuilder.Configurations.Add(new ProductConfigurations());

            //// Add the custom converter for OrderStatus enum
            //modelBuilder.Conventions.Add(new EnumToStringConverter<OrderStatus>());

            //base.OnModelCreating(modelBuilder);

       

        }
        public DbSet<Products> Product { get; set; }
        public DbSet<ProductBrand> ProductBrand { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<Order> order { get; set; }
        public DbSet<OrderItem> orderitem { get; set; }
        public DbSet<BasketItem> basketItems { get; set; }

        public DbSet<CustomerBasket> customerBasket { get; set; }


    }
}