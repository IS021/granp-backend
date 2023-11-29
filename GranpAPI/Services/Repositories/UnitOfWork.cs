using Granp.Data;
using Granp.Services.Repositories.Extensions;
using Granp.Services.Repositories.Interfaces;

namespace Granp.Services.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable // IDisposable is used to free unmanaged resources
{
    private readonly DataContext _context;
    private readonly MongoDbContext _mongoDbContext;
    private readonly ILogger _logger;

    public ICustomerRepository Customers { get; private set; }
    public IProfessionalRepository Professionals { get; private set;}
    public IReservationRepository Reservations { get; private set; }
    public IChatRepository Chats { get; private set; }
    public IMessageRepository Messages { get; private set; }

    public UnitOfWork(DataContext context, MongoDbContext mongoDbContext, ILoggerFactory loggerFactory)
    {
        _context = context;
        _mongoDbContext = mongoDbContext;
        _logger = loggerFactory.CreateLogger("logs");

        Customers = new CustomerRepository(_context, _logger);
        Professionals = new ProfessionalRepository(_context, _logger);
        Reservations = new ReservationRepository(_context, _logger);
        Chats = new ChatRepository(_mongoDbContext);
        Messages = new MessageRepository(_mongoDbContext);
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
        // No need to save changes in MongoDB (Needs to be tested)
    }

    public void Dispose()
    {
        _context.Dispose();
    }

}