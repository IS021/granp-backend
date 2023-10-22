using Granp.Models.Common;
using Granp.Models.Entities;
using Granp.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Granp.Services.Repositories
{
    public abstract class GenericUserRepository<TUser, TContext>: GenericRepository<TUser, TContext>, IUserRepository<TUser>
        where TUser : BaseUser
        where TContext : DbContext
    {
        public GenericUserRepository(TContext context, ILogger logger) : base(context, logger) { }

        public async Task<TUser> GetByUserId(string userId)
        {
            return await dbSet.FirstOrDefaultAsync(u => u.UserId == userId);
        }
    }
}