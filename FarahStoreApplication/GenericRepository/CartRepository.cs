using FarahStoreApplication.GenericRepository.IGenericRepository;
using FarahStoreModel.CommonModel;
using FarahStoreModel.Models;
using FarahStoreWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace FarahStoreApplication.GenericRepository;

public class CartRepository:Repository<Cart>,ICartRepository
{
    private readonly DatabaseContex _db;

    public CartRepository(DatabaseContex db) : base(db)
    {
        _db = db;
    }
    public async Task<ResultDto> Decrease(int DecreaseNumber, int CartItemId)
    {
        var cartItem = await _db.Carts.Include(p=>p.Product).FirstOrDefaultAsync(c => c.Id == CartItemId);
        if (cartItem == null)
        {
            return new ResultDto
            {
                Status = false,
                Message = "آیتمی با این مشخصات یافت نشد"
            };
        }

        cartItem.Count -= DecreaseNumber;
        _db.SaveChangesAsync();
        return new ResultDto
        {
            Status = true,
            Message = $"{DecreaseNumber} واحد از کالای {cartItem.Product.Name} کم شد"
        };
    }

    public async Task<ResultDto> Increase(int IncreaseNumber, int CartItemId)
    {
        var cartItem = await _db.Carts.Include(p => p.Product).FirstOrDefaultAsync(c => c.Id == CartItemId);
        if (cartItem == null)
        {
            return new ResultDto
            {
                Status = false,
                Message = "آیتمی با این مشخصات یافت نشد"
            };
        }

        cartItem.Count += IncreaseNumber;
        _db.SaveChangesAsync();
        return new ResultDto
        {
            Status = true,
            Message = $"{IncreaseNumber} واحد به تعداد کالای {cartItem.Product.Name} اضافه شد شد"
        };
    }
}