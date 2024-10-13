using EcommerceCoffe_DAL.Models;
using EcommerceCoffe_DAL.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceCoffe_DAL.Prsistence.Repositories.GenaricRepo
{
    public interface IGenaricRepo<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T model);
        Task<T> updateAsync(T model);
        Task<bool> DeleteAsync(int id);

    }
}
