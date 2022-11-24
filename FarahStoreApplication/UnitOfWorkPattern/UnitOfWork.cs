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

    }

    public ICategoryRepositry Category { get; private set; }
    public IChaildCategoryRepository ChaildCategory { get; private set; }
    public IProductRepository Product { get; private set; }
    public IProductImageRepository ProductImage { get; private set; }

    public async Task Save()
    {
        await _db.SaveChangesAsync();
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}