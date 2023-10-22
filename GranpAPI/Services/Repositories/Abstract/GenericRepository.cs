using Granp.Models.Common;
using Granp.Services.Repositories.Interfaces;
using Granp.Data;

using Microsoft.EntityFrameworkCore;

namespace Granp.Services.Repositories
{
    public abstract class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
            where TEntity : BaseEntity
            where TContext : DbContext
    {
        protected readonly TContext _context;
        protected readonly ILogger _logger;
        protected readonly DbSet<TEntity> dbSet;

        public GenericRepository(TContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            this.dbSet = _context.Set<TEntity>();
        }

        public async Task<bool> Add(TEntity entity)
        {
            try
            {
                await dbSet.AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding entity {entity}", entity);
                return false;
            }
        }

        public async Task<bool> Update(TEntity entity)
        {
            try
            {
                dbSet.Update(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating entity {entity}", entity);
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await dbSet.FindAsync(id);
            if (entity == null)
            {
                _logger.LogError("Entity {entity} not found", entity);
                return false;
            }

            try
            {
                dbSet.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting entity {entity}", entity);
                return false;
            }
        }

        public async Task<TEntity> GetById(int id)
        {
            try
            {
                return await dbSet.FindAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting entity with id {Id}", id);
                return null;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting all entities");
                return null;
            }
        }
    }
}