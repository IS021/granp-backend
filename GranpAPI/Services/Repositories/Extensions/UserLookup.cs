using Granp.Models.Common;
using Granp.Models.Entities;
using Granp.Services.Repositories.Interfaces;

namespace Granp.Services.Repositories.Extensions
{
    public static class UserLookup
    {
        public static async Task<BaseUser?> GetUser(this IUnitOfWork unitOfWork, Guid id)
        {
            var customer = await unitOfWork.Customers.GetById(id);

            if (customer != null)
            {
                return customer;
            }
            
            var professional = await unitOfWork.Professionals.GetById(id);
            
            if (professional != null)
            {
                return professional;
            }
            return null;
        }

        public static async Task<BaseUser?> GetUser(this IUnitOfWork unitOfWork, string userId)
        {
            var customer = await unitOfWork.Customers.GetByUserId(userId);

            if (customer != null)
            {
                return customer;
            }
            
            var professional = await unitOfWork.Professionals.GetByUserId(userId);
            
            if (professional != null)
            {
                return professional;
            }
            return null;
        }
    }
}