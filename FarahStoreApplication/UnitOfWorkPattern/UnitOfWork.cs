using FarahStoreApplication.GenericRepository;
using FarahStoreApplication.GenericRepository.IGenericRepository;
using FarahStoreWeb.Data;

namespace FarahStoreApplication.UnitOfWorkPattern;


public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContex _db;

    public UnitOfWork(DatabaseContex db)
    {
        _db = db;
        Category = new CategoryRepository(_db);
        ChaildCategory = new ChaildCategoryRepository(_db);
        Product = new ProductRepository(_db);
        ProductImage = new ProductImageRepository(_db);
        ProductSpecifications = new SpecificationsRepository(_db);
        ProductColor = new ProductColorRepository(_db);
        Slider = new SliderRepository(_db);
        Cart = new CartRepository(_db);

    }

    public ICategoryRepositry Category { get; private set; }
    public IChaildCategoryRepository ChaildCategory { get; private set; }
    public IProductRepository Product { get; private set; }
    public IProductImageRepository ProductImage { get; private set; }
    public ISpecificationsRepository ProductSpecifications { get; private set; }
    public IProductColorRepository ProductColor { get; private set; }
    public ISliderRepository Slider { get; private set; }
    public ICartRepository Cart { get; private set; }
    public async Task Save()
    {
        await _db.SaveChangesAsync();
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}