using FarahStoreApplication.GenericRepository.IGenericRepository;
using FarahStoreModel.CommonModel;
using FarahStoreModel.Models;
using FarahStoreWeb.Data;

namespace FarahStoreApplication.GenericRepository;

public class ProductImageRepository:Repository<ProductImage>,IProductImageRepository
{
    private readonly DatabaseContex _db;

    public ProductImageRepository(DatabaseContex db) : base(db)
    {
        _db = db;
    }

    public Task<ResultDto> Update(ProductImage productImage)
    {
        throw new NotImplementedException();
    }
}