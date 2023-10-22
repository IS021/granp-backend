namespace Granp.Services.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; } 
        IProfessionalRepository Professionals { get; }
        Task CompleteAsync();
    }

}