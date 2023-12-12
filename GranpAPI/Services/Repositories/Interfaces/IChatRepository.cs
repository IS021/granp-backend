using Granp.Models.Entities;
using Granp.Models.Types;

namespace Granp.Services.Repositories.Interfaces
{
    public interface IChatRepository : IGenericRepository<Chat>
    {
        Task<List<Chat>> GetByMemberId(Guid memberId);
        Task<Chat?> GetByMembers(List<Guid> members);
    }    
}