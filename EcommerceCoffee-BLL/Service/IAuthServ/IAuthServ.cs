using EcommerceCoffe_DAL.Model.IdentityModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceCoffee_BLL.Service.IAuthServ
{
    public interface IAuthServ
    {
        Task<string> CreateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager);

  



    }
}
