using Granp.Data;
using Granp.Models.Entities;
using Granp.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Granp.Services.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation, DataContext>, IReservationRepository
    {
        public ReservationRepository(DataContext context, ILogger logger) : base(context, logger) { }

        public async Task<List<Reservation>> GetByProfessionalId(Guid professionalId)
        {
            return await dbSet.Where(r => r.Professional.Id == professionalId).ToListAsync();
        }

        public async Task<List<Reservation>> GetByCustomerId(Guid customerId)
        {
            return await dbSet.Where(r => r.Customer.Id == customerId).ToListAsync();
        }
    }
}