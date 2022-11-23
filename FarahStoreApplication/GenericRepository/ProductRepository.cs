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
        var productFromDb = await _db.Products.FindAsync(product.Id);
        if (productFromDb == null)
        {
            return new ResultDto
            {
                Status = false,
                Message = "محصولی با این مشخصات در سیستم وجود ندارد"
            };
        }
        productFromDb.Name=product.Name;
        productFromDb.ChaildCategoryCategoryId=product.ChaildCategoryCategoryId;
        productFromDb.DiscountLable=product.DiscountLable;
        productFromDb.Price = product.Price;
        productFromDb.FullDescription=product.FullDescription;
        productFromDb.NumberOfEntries=product.NumberOfEntries;
        productFromDb.OldPrice=product.OldPrice;
        productFromDb.ShortDescription=product.ShortDescription;
        productFromDb.Slug=product.Slug;
        return new ResultDto
        {
            Status = true,
            Message = "محصول مورد نظر بروز شد"
        };
    }
}