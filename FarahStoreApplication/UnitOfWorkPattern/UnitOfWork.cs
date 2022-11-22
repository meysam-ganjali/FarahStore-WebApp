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
      
    }

    public ICategoryRepositry Category { get; private set; }
   
    public async Task Save()
    {
        await _db.SaveChangesAsync();
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}