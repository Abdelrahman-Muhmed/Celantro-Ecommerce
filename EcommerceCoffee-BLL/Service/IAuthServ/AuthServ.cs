using EcommerceCoffe_DAL.Model.IdentityModel;
using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EcommerceCoffee_BLL.Service.IAuthServ
{
    public class AuthServ : IAuthServ
    {
  
        public async Task<string> CreateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager)
        {
            // Start Create Token By Claim 
            //1- Create New Claim 
            var claims = new List<Claim>() 
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            // 2- Create Roles For Users 

            var roles = await userManager.GetRolesAsync(user.Id);

            // 3- Get Roles With By Forech For Add Roles For Claim 
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // 4- Create SymmtricKey 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dsfsfda6666adf6daffafadddddd6fad45afd"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 5- Create RegisterClaim 
            var Token = new JwtSecurityToken
            (
                 issuer: "https://localhost:44350/",
                 audience: "asdasffdasffdsfdsadsac",
                 claims: claims,
                 expires: DateTime.UtcNow.AddHours(3),
                 signingCredentials: creds
            );


            return new JwtSecurityTokenHandler().WriteToken(Token);
        }

    }
}