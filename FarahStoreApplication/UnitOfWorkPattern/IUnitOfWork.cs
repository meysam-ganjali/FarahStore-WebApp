using FarahStoreApplication.GenericRepository.IGenericRepository;

namespace FarahStoreApplication.UnitOfWorkPattern;

public interface IUnitOfWork : IDisposable
{
    ICategoryRepositry Category { get; }
    IChaildCategoryRepository ChaildCategory { get; }
    IProductRepository Product { get; }
    Task Save();
}