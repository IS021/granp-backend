using Granp.Data;
using Granp.Services.Repositories.Interfaces;

namespace Granp.Services.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable // IDisposable is used to free unmanaged resources
{
    private readonly DataContext _context;
    private readonly ILogger _logger;

    public ICustomerRepository Customers { get; private set; }
    public IProfessionalRepository Professionals { get; private set;}
    public IReservationRepository Reservations { get; private set; }

    public UnitOfWork(DataContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = loggerFactory.CreateLogger("logs");

        Customers = new CustomerRepository(_context, _logger);
        Professionals = new ProfessionalRepository(_context, _logger);
        Reservations = new ReservationRepository(_context, _logger);

    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

}