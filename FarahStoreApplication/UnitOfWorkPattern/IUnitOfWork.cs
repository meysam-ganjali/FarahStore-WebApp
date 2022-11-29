using FarahStoreApplication.GenericRepository.IGenericRepository;

namespace FarahStoreApplication.UnitOfWorkPattern;

public interface IUnitOfWork : IDisposable
{
    ICategoryRepositry Category { get; }
    IChaildCategoryRepository ChaildCategory { get; }
    IProductRepository Product { get; }
    IProductImageRepository ProductImage { get; }
    ISpecificationsRepository ProductSpecifications { get; }
    IProductColorRepository ProductColor { get; }
    ISliderRepository Slider { get; }
    ICartRepository Cart { get; }
    Task Save();
}