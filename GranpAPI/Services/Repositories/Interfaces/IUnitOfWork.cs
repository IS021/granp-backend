namespace Granp.Services.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; } 
        IProfessionalRepository Professionals { get; }
        IReservationRepository Reservations { get; }
        IChatRepository Chats { get; }
        IMessageRepository Messages { get; }
        Task CompleteAsync();
    }

}