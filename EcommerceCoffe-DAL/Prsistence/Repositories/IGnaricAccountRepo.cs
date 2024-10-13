using EcommerceCoffe_DAL.Model.IdentityModel;
using EcommerceCoffe_DAL.Models;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceCoffe_DAL.Prsistence.Repositories
{
    public interface IGnaricAccountRepo<T> where T : class
    {
  
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(string id);
        Task<T> CreateAsync(T model);
        Task<T> updateAsync(ApplicationUser model);
        Task<bool> DeleteAsync(string id);
    }

}
