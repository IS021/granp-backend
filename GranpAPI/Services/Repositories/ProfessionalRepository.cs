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

        public async Task<List<Professional>> GetProfessionalsByFilter(ProfessionalFilter filter)
        {
            // All the filters are optional, so we need to check if they are null
            // All the criteria are ANDed together
            // If a filter is null, it is ignored
            // If a filter is not null, it is applied
            // Do this with LINQ and without getting all the professionals from the database
            // Do this in a single efficient query

            // Example:
            // SELECT * FROM Professionals
            // WHERE (Profession = filter.Profession OR filter.Profession IS NULL)
            // AND (Location = filter.Location OR filter.Location IS NULL)
            // AND (TimeSlot = filter.TimeSlot OR filter.TimeSlot IS NULL)
            // AND (HourlyRate <= filter.MaxHourlyRate OR filter.MaxHourlyRate IS NULL)
            // AND (LongTimeJob = filter.LongTimeJob OR filter.LongTimeJob IS NULL)
            // AND (ShortTimeJob = filter.ShortTimeJob OR filter.ShortTimeJob IS NULL)
            // AND (Distance <= filter.MaxDistance OR filter.MaxDistance IS NULL)
            // AND (WeeksInAdvance <= filter.MaxWeeksInAdvance OR filter.MaxWeeksInAdvance IS NULL)
            // AND (Rating >= filter.MinRating OR filter.MinRating IS NULL)
            // AND (Age >= filter.MinAge OR filter.MinAge IS NULL)
            // AND (Age <= filter.MaxAge OR filter.MaxAge IS NULL)

            // Start with a query that includes all professionals
            IQueryable<Professional> query = _context.Professionals;

            // Apply the location filter
            if (filter.Location != null)
            {
                query = query.Where(p => p.Address.Location.DistanceTo(filter.Location) <= p.MaxDistance);
            }

            // Apply the other filters only if they are not null
            if (filter.Profession.HasValue)
            {
                query = query.Where(p => p.Profession == filter.Profession.Value);
            }

            // TODO: Build Better Logic for TimeSlot comparison
            if (filter.TimeSlots != null)
            {
                query = query.Where(p => p.TimeTable.Overlap(filter.TimeSlots) >= 0.9);
            }

            if (filter.MaxHourlyRate.HasValue)
            {
                query = query.Where(p => p.HourlyRate <= filter.MaxHourlyRate.Value);
            }

            if (filter.LongTimeJob.HasValue)
            {
                query = query.Where(p => p.LongTimeJob == filter.LongTimeJob.Value);
            }

            if (filter.ShortTimeJob.HasValue)
            {
                query = query.Where(p => p.ShortTimeJob == filter.ShortTimeJob.Value);
            }

            if (filter.MaxWeeksInAdvance.HasValue)
            {
                query = query.Where(p => p.WeeksInAdvance <= filter.MaxWeeksInAdvance.Value);
            }

            if (filter.MinRating.HasValue)
            {
                query = query.Where(p => p.Rating >= filter.MinRating.Value);
            }

            if (filter.MinAge.HasValue)
            {
                query = query.Where(p => p.Age >= filter.MinAge.Value);
            }

            if (filter.MaxAge.HasValue)
            {
                query = query.Where(p => p.Age <= filter.MaxAge.Value);
            }

            // TODO: Add pagination
            // TODO: Add sorting

            // Execute the query and return the results
            return await query.ToListAsync();
        }

    }

    // Other Professional specific methods
}
