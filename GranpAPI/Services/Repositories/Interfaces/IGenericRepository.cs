using Granp.Models.Common;

namespace Granp.Services.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll(); 
        Task<T> GetById(int id);
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(int id);
    }
}