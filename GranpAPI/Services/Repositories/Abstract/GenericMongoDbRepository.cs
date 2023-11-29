using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Granp.Services.Repositories.Interfaces;
using Granp.Models.Types;
using Granp.Models.Common;
using Granp.Data;

namespace Granp.Services.Repositories
{
    public abstract class GenericMongoDbRepository<T>: IGenericRepository<T> where T : BaseEntity
    {
        protected readonly IMongoCollection<T> _collection;

        public GenericMongoDbRepository(MongoDbContext mongoDbContext, string collectionName)
        {
            _collection = mongoDbContext.GetCollection<T>(collectionName);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Add(T entity)
        {
            try
            {
                await _collection.InsertOneAsync(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(T entity)
        {
            try
            {
                await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                await _collection.DeleteOneAsync(x => x.Id == id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        
    }
}