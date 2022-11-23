using FarahStoreApplication.GenericRepository.IGenericRepository;
using FarahStoreModel.CommonModel;
using FarahStoreModel.Models;
using FarahStoreWeb.Data;

namespace FarahStoreApplication.GenericRepository;

public class ChaildCategoryRepository:Repository<ChaildCategory>,IChaildCategoryRepository
{
    private readonly DatabaseContex _db;

    public ChaildCategoryRepository(DatabaseContex db) : base(db)
    {
        _db = db;
    }
    public async Task<ResultDto> Update(ChaildCategory chaildCategory)
    {
        var category = await _db.ChaildCategories.FindAsync(chaildCategory.Id);
        if (category == null)
        {
            return new ResultDto
            {
                Status = false,
                Message = "زیر دسته مورد نظر وجود ندارد"
            };
        }

        if (!string.IsNullOrWhiteSpace(chaildCategory.LogoPath))
        {
            category.LogoPath = chaildCategory.LogoPath;
        }
        category.Name=chaildCategory.Name;
        chaildCategory.CategoryId=chaildCategory.CategoryId;
        return new ResultDto
        {
            Status = true,
            Message = "دسته بندی بروز شد"
        };
    }
}