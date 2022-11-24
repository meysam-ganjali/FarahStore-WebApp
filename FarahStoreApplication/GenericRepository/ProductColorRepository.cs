using FarahStoreApplication.GenericRepository.IGenericRepository;
using FarahStoreModel.CommonModel;
using FarahStoreModel.Models;
using FarahStoreWeb.Data;

namespace FarahStoreApplication.GenericRepository;

public class ProductColorRepository:Repository<ProductColor>,IProductColorRepository
{
    private readonly DatabaseContex _db;

    public ProductColorRepository(DatabaseContex db) : base(db)
    {
        _db = db;
    }
    public async Task<ResultDto> Update(ProductColor productColor)
    {
        var productColorFromDb = await _db.ProductColors.FindAsync(productColor.Id);
        if (productColorFromDb==null)
        {
            return new ResultDto
            {
                Status = false,
                Message = "مقدار یافت نشد"
            };

        }

        productColorFromDb.ColorName = productColor.ColorName;
        productColorFromDb.ColorHex=productColor.ColorHex;
        return new ResultDto
        {
            Status = true,
            Message = "مقدار رنگ تعویض شد"
        };
    }
}