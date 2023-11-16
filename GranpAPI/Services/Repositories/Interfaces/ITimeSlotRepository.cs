using Granp.Models.Entities;
using Granp.Models.Types;
using Granp.Models.Enums;

namespace Granp.Services.Repositories.Interfaces
{
    public interface ITimeSlotRepository : IGenericRepository<TimeSlot>
    {
        Task<List<TimeSlot>> GetTimeSlotsByProfessionalId(Guid professionalId);
    }
}