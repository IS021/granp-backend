using Granp.Models.Common;

namespace Granp.Services.Repositories.Interfaces
{
    public interface IUserRepository<T>: IGenericRepository<T> where T : BaseUser
    {
        Task<T> GetByUserId(string userId);
    }
}