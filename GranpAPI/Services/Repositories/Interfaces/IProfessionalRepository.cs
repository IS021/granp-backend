using Granp.Models.Entities;
using Granp.Models.Types;
using Granp.Models.Enums;

namespace Granp.Services.Repositories.Interfaces
{
    public interface IProfessionalRepository : IUserRepository<Professional>
    {
        Task<List<Professional>> GetProfessionalsByFilter(ProfessionalFilter filter);
    }
}