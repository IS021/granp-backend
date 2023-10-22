using Granp.Data;
using Granp.Models.Entities;
using Granp.Services.Repositories.Interfaces;

namespace Granp.Services.Repositories
{
    public class CustomerRepository : GenericUserRepository<Customer, DataContext>, ICustomerRepository
    {
        public CustomerRepository(DataContext context, ILogger logger) : base(context, logger) { }

        // Other Customer specific methods
    }
}