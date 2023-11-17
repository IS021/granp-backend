namespace Granp.Services.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; } 
        IProfessionalRepository Professionals { get; }
        IReservationRepository Reservations { get; }
        ITimeSlotRepository TimeSlots { get; }
        Task CompleteAsync();
    }

}