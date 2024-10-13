using EcommerceCoffe_DAL.Model.IdentityModel;
using EcommerceCoffe_DAL.Models.Product;
using EcommerceCoffee_BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceCoffee_BLL.Service.IAccountService
{
    public interface IAccountServ
    {
        Task<IEnumerable<ApplicationUserReturnDto>> GetAllUsers();

        Task<ApplicationUserReturnDto> GetUserById(string id);

        Task<ApplicationUser> CreateUserAccount(ApplicationUser product);

        Task<ApplicationUser> UpdateUserAccount(ApplicationUser product);

        Task<bool> DeleteUser(string id);
    }
}
