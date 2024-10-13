using EcommerceCoffe_DAL.Model.IdentityModel;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;


using System.Data.Entity;
namespace EcommerceCoffe_DAL.Prsistence.IdentityData
{
    public class ApplicationIdentityContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationIdentityContext() : base("name=IdentityDb")
        {

        }

       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure the one-to-one relationship between ApplictionUser and Adress
            modelBuilder.Entity<ApplicationUser>()
                .HasOptional(u => u.userAdress)
                .WithRequired(a => a.ApplictionUser);
                
        }

     
        public DbSet<Adress> Adresses { get; set; }

        //public System.Data.Entity.DbSet<EcommerceCoffee_BLL.DTOs.ApplicationUserReturnDto> ApplicationUserReturnDtoes { get; set; }

        //public System.Data.Entity.DbSet<EcommerceCoffee_BLL.DTOs.ApplicationUserReturnDto> ApplicationUserReturnDtoes { get; set; }
    }
}