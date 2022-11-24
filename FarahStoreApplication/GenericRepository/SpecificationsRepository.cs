using FarahStoreApplication.GenericRepository.IGenericRepository;
using FarahStoreModel.CommonModel;
using FarahStoreModel.Models;
using FarahStoreWeb.Data;

namespace FarahStoreApplication.GenericRepository;

public class SpecificationsRepository:Repository<ProductSpecifications>,ISpecificationsRepository
{
    private readonly DatabaseContex _db;

    public SpecificationsRepository(DatabaseContex db) : base(db)
    {
        _db = db;
    }
    public async Task<ResultDto> Update(ProductSpecifications specifications)
    {
        var productSpecifications = await _db.ProductSpecifications.FindAsync(specifications.Id);
        if (productSpecifications == null)
        {
            return new ResultDto
            {
                Status = false,
                Message = "مشخصه فوق وجود ندارد"
            };
        }
        productSpecifications.Title=specifications.Title;
        productSpecifications.Value=specifications.Value;
        return new ResultDto
        {
            Status = true,
            Message = "مشخصه فوق بروزرسانی شد"
        };
    }
}