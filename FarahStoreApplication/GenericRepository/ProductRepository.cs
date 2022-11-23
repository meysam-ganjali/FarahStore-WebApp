using FarahStoreApplication.GenericRepository.IGenericRepository;
using FarahStoreModel.CommonModel;
using FarahStoreModel.Models;
using FarahStoreWeb.Data;

namespace FarahStoreApplication.GenericRepository;

public class ProductRepository:Repository<Product>, IProductRepository
{
    private readonly DatabaseContex _db;

    public ProductRepository(DatabaseContex db) : base(db)
    {
        _db = db;
    }
    public async Task<ResultDto> Update(Product product)
    {
        throw new NotImplementedException();
    }
}