using FarahStoreApplication.GenericRepository.IGenericRepository;
using FarahStoreModel.CommonModel;
using FarahStoreModel.Models;
using FarahStoreWeb.Data;

namespace FarahStoreApplication.GenericRepository;

public class CategoryRepository:Repository<Category>,ICategoryRepositry
{
    private readonly DatabaseContex _db;

    public CategoryRepository(DatabaseContex db) : base(db)
    {
        _db = db;
    }

    public async Task<ResultDto> Update(Category category)
    {
        var categoryFromDb = await _db.Categories.FindAsync(category.Id);
        if (categoryFromDb == null)
        {
            return new ResultDto
            {
                Status = false,
                Message = "دسته بندی یافت نشد"
            };
        }

        if (!string.IsNullOrWhiteSpace(categoryFromDb.LogoPath))
        {
            categoryFromDb.LogoPath = category.LogoPath;
        }
        categoryFromDb.Name=category.Name;
        return new ResultDto
        {
            Status = true,
            Message = $"دسته بندی {categoryFromDb.Name} بروز رسانی شد"
        };
    }
}