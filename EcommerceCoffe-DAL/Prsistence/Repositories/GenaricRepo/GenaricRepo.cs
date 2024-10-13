using System.Threading.Tasks;
using EcommerceCoffe_DAL.Models;
using EcommerceCoffe_DAL.Prsistence.Data;
using System;
using System.Data.Entity;
using System.Linq;


namespace EcommerceCoffe_DAL.Prsistence.Repositories.GenaricRepo
{
    public class GenaricRepo<T> : IGenaricRepo<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;
        public GenaricRepo(ApplicationDbContext dbContext) //Make DepdancyInjecation Ask CLR To Create object from ApplicationDbContext
        {
            _dbContext = dbContext;
        }

        //Get All Modeles 
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

        //Get Any Modele By Id 
        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                var query = await _dbContext.Set<T>()
                            .FirstOrDefaultAsync(p => p.id == id);
                return query;
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while retrieving data: {ex.Message}", ex);

            }
        }

        //Create Any Modele 
        public async Task<T> CreateAsync(T model)
        {
           
            try
            {
                var query =  _dbContext.Set<T>().Add(model);
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

        //update Any Modele  
        public async Task<T> updateAsync(T model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model), "The model cannot be null.");
                }
                //var query = _dbContext.Entry(model).State = EntityState.Modified; // Not Valid 
              
                var existingEntity = await _dbContext.Set<T>().FindAsync(model.id);

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

        public async Task<bool> DeleteAsync(int id)
        {                   

            var getById = await _dbContext.Set<T>().FindAsync(id);

            if (getById != null)
            {
                _dbContext.Set<T>().Remove(getById);
                _dbContext.SaveChanges();
            }
            return true;
        }

      
    }
}