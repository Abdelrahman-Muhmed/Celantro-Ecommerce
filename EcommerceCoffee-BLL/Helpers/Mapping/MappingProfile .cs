using Amazon_Api.Dtos;
using AutoMapper;
using EcommerceCoffe_DAL.Model.IdentityModel;
using EcommerceCoffe_DAL.Models.Product;
using EcommerceCoffee_BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceCoffee_BLL.Helpers.Mapping
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Products, ProductRetuenDto>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
              .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            // Mapping from Products to ProductDto
            CreateMap<Products, ProductDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.ExistingImagePath, opt => opt.MapFrom(src => src.PictureUrl))
                .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.BrandId))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.ImageFile, opt => opt.Ignore()); // Handled separately

            // Mapping from ProductDto to Products
            CreateMap<ProductDto, Products>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PictureUrl, opt => opt.Ignore()) // Handled separately
                .ForMember(dest => dest.ProductBrand, opt => opt.Ignore()) // To prevent circular references
                .ForMember(dest => dest.CategoryName, opt => opt.Ignore()); // To prevent circular references



            //Entity To Dto Useres 
            //ReverseMap from Dto To Entity 

            CreateMap<ApplicationUser, ApplicationUserReturnDto>()
                .ForMember(u => u.Id, u => u.MapFrom(src => src.Id))
                .ForMember(u => u.Name, u => u.MapFrom(src => src.Name))
                .ForMember(u => u.Email, u => u.MapFrom(src => src.Email))
                .ForMember(u => u.EmailConfirmed, u => u.MapFrom(src => src.EmailConfirmed))
                .ForMember(u => u.PhoneNumber, u => u.MapFrom(src => src.PhoneNumber))
                .ForMember(u => u.IsLoggedIn, u => u.MapFrom(src => src.IsLoggedIn))
                .ForMember(u => u.UserName, u => u.MapFrom(src => src.UserName))
                .ForMember(u => u.Password, u => u.MapFrom(src => src.PasswordHash))
                .ForMember(u => u.LockoutEndDateUtc, u => u.MapFrom(src => src.LockoutEndDateUtc))
                .ForMember(u => u.PhoneNumberConfirmed, u => u.MapFrom(src => src.PhoneNumberConfirmed)).ReverseMap();


            // CreateMap<ApplicationUser, ApplicationUserReturnDto>()
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString())).ReverseMap();

        }

    }
}