using FarahStoreApplication.GenericRepository.IGenericRepository;

namespace FarahStoreApplication.UnitOfWorkPattern;

public interface IUnitOfWork : IDisposable
{
    ICategoryRepositry Category { get; }
    Task Save();
}