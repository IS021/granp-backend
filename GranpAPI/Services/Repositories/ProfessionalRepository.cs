using Granp.Models.Entities;
using Granp.Models.Types;
using Granp.Data;
using Granp.Services.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Granp.Services.Repositories
{
    public class ProfessionalRepository : GenericUserRepository<Professional, DataContext>, IProfessionalRepository
    {
        public ProfessionalRepository(DataContext context, ILogger logger) : base(context, logger) { }

        public async Task<List<Professional>> GetByFilter(SearchFilter filter)
        {
            // All the filters are optional, so we need to check if they are null
            // All the criteria are ANDed together
            // If a filter is null, it is ignored
            // If a filter is not null, it is applied
            // Do this with LINQ and without getting all the professionals from the database
            // Do this in a single efficient query

            // Start with a query that includes all professionals
            IQueryable<Professional> query = _context.Professionals;


            // Apply the other filters only if they are not null
            if (filter.Profession.HasValue)
            {
                query = query.Where(p => p.Profession == filter.Profession.Value);
            }

            if (filter.MaxHourlyRate.HasValue)
            {
                query = query.Where(p => p.HourlyRate <= filter.MaxHourlyRate.Value);
            }

            if (filter.LongTimeJob.HasValue && filter.LongTimeJob.Value)
            {
                query = query.Where(p => p.LongTimeJob == true);
            }

            if (filter.ShortTimeJob.HasValue && filter.ShortTimeJob.Value)
            {
                query = query.Where(p => p.ShortTimeJob == true);
            }

            if (filter.MinRating.HasValue)
            {
                query = query.Where(p => p.Rating >= filter.MinRating.Value);
            }

            if (filter.MinAge.HasValue)
            {
                query = query.Where(p => p.BirthDate.Year <= DateTime.Now.Year - filter.MinAge.Value);
            }

            // DateTime.Now.Year - BirthDate.Year >= filter.MinAge.Value
            // BirthDate.Year <= DateTime.Now.Year - filter.MinAge.Value

            if (filter.MaxAge.HasValue)
            {
                query = query.Where(p => p.BirthDate.Year >= DateTime.Now.Year - filter.MaxAge.Value);
            }

            // Execute query and return the results
            List<Professional> professionals = await query.ToListAsync();
            
            // Where(p => p.Address.Location.DistanceTo(filter.Location) <= p.MaxDistance);

            // Filter by distance clientside
            foreach (var professional in professionals.ToList())
            {
                if (professional.Address.Location.DistanceTo(filter.Location) > professional.MaxDistance)
                {
                    professionals.Remove(professional);
                }
            }

            // TODO: Build Better Logic for TimeSlot comparison
            if (filter.TimeSlots != null)
            {
                //query = query.Where(p => p.TimeTable.Overlap(filter.TimeSlots) >= 0.9);
                foreach (var professional in professionals.ToList())
                {
                    if (professional.TimeTable.Overlap(filter.TimeSlots) < 0.9)
                    {
                        professionals.Remove(professional);
                    }
                }
            }

            return professionals;

            // TODO: Add pagination
            // TODO: Add sorting
            
        }

    }

    // Other Professional specific methods
}
