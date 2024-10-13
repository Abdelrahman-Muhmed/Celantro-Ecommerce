using EcommerceCoffe_DAL.Model.IdentityModel;
using EcommerceCoffe_DAL.Models;
using EcommerceCoffe_DAL.Prsistence.Data;
using EcommerceCoffe_DAL.Prsistence.IdentityData;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EcommerceCoffe_DAL.Prsistence.Repositories
{

    public class GenaricAccountRepo<T> : IGnaricAccountRepo<T> where T : class
    {
        private readonly ApplicationIdentityContext _dbContext;
        public GenaricAccountRepo(ApplicationIdentityContext dbContext) //Make DepdancyInjecation Ask CLR To Create object from ApplicationDbContext
        {
            _dbContext = dbContext;

        }

        public IQueryable<T> GetAll()
        {
            try
            {
                var query = _dbContext.Set<T>().AsNoTracking();
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving data: {ex.Message}", ex);
            }
        }

   

        public async Task<T> GetByIdAsync(string id)
        {
            try
            {
                var query = await _dbContext.Set<T>().FindAsync(id);
                return query;
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while retrieving data: {ex.Message}", ex);

            }
        }
        public async Task<T> CreateAsync(T model)
        {
            try
            {
                var query = _dbContext.Set<T>().Add(model);
                if (query != null)
                {
                    await _dbContext.SaveChangesAsync();
                }
                return query;
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while Creating data: {ex.Message}", ex);
            }
        }

        public async Task<T> updateAsync(ApplicationUser model)
        {
       

            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model), "The model cannot be null.");
                }
                //var query = _dbContext.Entry(model).State = EntityState.Modified; // Not Valid 
              
                var existingEntity = await _dbContext.Set<T>().FindAsync(model);

                if (existingEntity == null)
                {
                    throw new InvalidOperationException("Entity not found.");
                }
                _dbContext.Entry(existingEntity).CurrentValues.SetValues(model);

                await _dbContext.SaveChangesAsync();

                return existingEntity;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating data: {ex.Message}", ex);
            }
        }


        public async Task<bool> DeleteAsync(string id)
        {
            var getById = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (getById != null)
            {
                _dbContext.Users.Remove(getById);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

    }
}