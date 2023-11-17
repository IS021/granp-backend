namespace Granp.Services.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; } 
        IProfessionalRepository Professionals { get; }
        IReservationRepository Reservations { get; }
        Task CompleteAsync();
    }

}