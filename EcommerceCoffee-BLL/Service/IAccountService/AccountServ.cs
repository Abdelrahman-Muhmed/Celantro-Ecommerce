using AutoMapper;
using AutoMapper.QueryableExtensions;
using EcommerceCoffe_DAL.Model.IdentityModel;
using EcommerceCoffe_DAL.Models.Product;
using EcommerceCoffe_DAL.Prsistence.Repositories;
using EcommerceCoffee_BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EcommerceCoffee_BLL.Service.IAccountService
{
    public class AccountServ : IAccountServ
    {
        private readonly IGnaricAccountRepo<ApplicationUser> _AccountRepo;
        private readonly IMapper _mapper; 
        public AccountServ(IGnaricAccountRepo<ApplicationUser> AccountRepo, IMapper mapper)
        {
            _AccountRepo = AccountRepo;
            _mapper = mapper;
        }

        //Get All Useres
        public async Task<IEnumerable<ApplicationUserReturnDto>> GetAllUsers()
        {
            var query = _AccountRepo.GetAll();

            var result = await query
             .ProjectTo<ApplicationUserReturnDto>(_mapper.ConfigurationProvider)
             .ToListAsync();

            return result;
        }


        public async Task<ApplicationUserReturnDto> GetUserById(string id)
        {
            // var query = _AccountRepo.GetByIdAsync(id);

            // var result = await query
            //.ProjectTo<ApplicationUserReturnDto>(_mapper.ConfigurationProvider)
            //.FirstAsync(id);   

            // return result; 

            return null;

        }

        public async Task<ApplicationUser> CreateUserAccount(ApplicationUser user)
        {
            var query = await _AccountRepo.CreateAsync(user);
            return query;
        }

        public async Task<ApplicationUser> UpdateUserAccount(ApplicationUser user)
        {
            var query = await _AccountRepo.updateAsync(user);
            return query;

        }

        public async Task<bool> DeleteUser(string id)
        {
            var result = await _AccountRepo.GetByIdAsync(id);

            if (result != null)
                return await _AccountRepo.DeleteAsync(id);

            return false;
        }


       

      
    }
}