using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity.Lifetime;
using EcommerceCoffe_DAL.Prsistence.Repositories;
using EcommerceCoffe_DAL.Models.Product;
using EcommerceCoffee_BLL.Service.IProductService;
using EcommerceCoffee_BLL.Service.IAuthServ;
using EcommerceCoffe_DAL.Model.IdentityModel;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using EcommerceCoffe_DAL.Prsistence.Data;
using EcommerceCoffe_DAL.Prsistence.IdentityData;
using System.Data.Entity;
using System.Web;
using Microsoft.Owin;
using EcommerceCoffee_BLL.Service.ICategorySevice;
using EcommerceCoffee_BLL.Service.IAccountService;
using EcommerceCoffee_BLL.Service.IBasketService;
using EcommerceCoffee_BLL.Service.IOrderService;
using EcommerceCoffe_DAL.Prsistence.Repositories.ProductRepo;
using EcommerceCoffe_DAL.Prsistence.Repositories.GenaricRepo;
using EcommerceCoffe_DAL.Models.BasketModel;
using AutoMapper;
using EcommerceCoffee_BLL.Helpers.Mapping;
using EcommerceCoffee_BLL.DTOs;

namespace EcommerceCoffee
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Register DbContexts
            container.RegisterType<ApplicationIdentityContext>(
                new HierarchicalLifetimeManager()
            );

            container.RegisterType<ApplicationDbContext>(
                new HierarchicalLifetimeManager()
            );

            // Register Generic Repositories
            container.RegisterType(typeof(IGenaricRepo<>), typeof(GenaricRepo<>), new HierarchicalLifetimeManager());

            // Register Generic Account Repositories
            container.RegisterType(typeof(IGnaricAccountRepo<>), typeof(GenaricAccountRepo<>), new HierarchicalLifetimeManager());

            // Register Specific Repositories
            container.RegisterType<IGenaricRepo<Products>, GenaricRepo<Products>>(new HierarchicalLifetimeManager());
            container.RegisterType<IGenaricRepo<ProductCategory>, GenaricRepo<ProductCategory>>(new HierarchicalLifetimeManager());
            container.RegisterType<IGenaricRepo<ProductBrand>, GenaricRepo<ProductBrand>>(new HierarchicalLifetimeManager());
            container.RegisterType<IGenaricRepo<CustomerBasket>, GenaricRepo<CustomerBasket>>(new HierarchicalLifetimeManager());
            container.RegisterType<IGenaricRepo<BasketItem>, GenaricRepo<BasketItem>>(new HierarchicalLifetimeManager());

            

            // Initialize AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            config.AssertConfigurationIsValid();

            var mapper = config.CreateMapper();

            // Register AutoMapper as a Singleton
            container.RegisterInstance<IMapper>(mapper);

            // Register Services
            container.RegisterType<IProductServ, ProductServ>(new HierarchicalLifetimeManager());
            container.RegisterType<ICategoryServ, CategoryServ>(new HierarchicalLifetimeManager());
            container.RegisterType<IAccountServ, AccountServ>(new HierarchicalLifetimeManager());
            container.RegisterType<IAuthServ, AuthServ>(new HierarchicalLifetimeManager());
            container.RegisterType<IBasketServ, BasketServ>(new HierarchicalLifetimeManager());
            container.RegisterType<IOrderServ, OrderServ>(new HierarchicalLifetimeManager());

            // Register IUserStore with InjectionConstructor to pass the correct DbContext
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
                new HierarchicalLifetimeManager(),
                new InjectionConstructor(container.Resolve<ApplicationIdentityContext>())
            );

            // Register UserManager
            container.RegisterType<UserManager<ApplicationUser>>(
                new HierarchicalLifetimeManager(),
                new InjectionConstructor(
                    container.Resolve<IUserStore<ApplicationUser>>()
                )
            );

            // Register IAuthenticationManager using InjectionFactory
            container.RegisterType<IAuthenticationManager>(
                new HierarchicalLifetimeManager(),
                new InjectionFactory(c =>
                    HttpContext.Current.GetOwinContext().Authentication
                )
            );

            // Register SignInManager
            container.RegisterType<SignInManager<ApplicationUser, string>>(
                new HierarchicalLifetimeManager(),
                new InjectionFactory(c =>
                    new SignInManager<ApplicationUser, string>(
                        c.Resolve<UserManager<ApplicationUser>>(),
                        c.Resolve<IAuthenticationManager>()
                    )
                )
            );

            // Register the container with UnityDependencyResolver
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
