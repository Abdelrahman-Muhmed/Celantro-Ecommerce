
using EcommerceCoffe_DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Data.Entity.Migrations.Model.UpdateDatabaseOperation;

namespace EcommerceCoffe_DAL.Model.IdentityModel
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsLoggedIn { get; set; }
        public string CurrentSessionId { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string? Name { get; set; }
        public Adress? userAdress { get; set; }

     
        public async Task<ClaimsIdentity> GenrateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}

