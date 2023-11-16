using Granp.Models.Entities;
using Granp.Models.Types;

namespace Granp.Services.Repositories.Interfaces
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        Task<List<Reservation>> GetByProfessionalId(Guid professionalId);
        Task<List<Reservation>> GetByCustomerId(Guid customerId);
    }    
}