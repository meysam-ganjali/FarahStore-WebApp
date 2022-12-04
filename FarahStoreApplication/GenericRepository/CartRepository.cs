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
    public async Task<ResultDto> Decrease( int CartItemId)
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

        cartItem.Count -= 1;
        if (cartItem.Count < 1)
        {
            _db.Carts.Remove(cartItem);
        }
        return new ResultDto
        {
            Status = true,
            Message = $"1 واحد از کالای {cartItem.Product.Name} کم شد"
        };
    }

    public async Task<ResultDto> Increase( int CartItemId)
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

        cartItem.Count += 1;
        return new ResultDto
        {
            Status = true,
            Message = $"1 واحد به تعداد کالای {cartItem.Product.Name} اضافه شد شد"
        };
    }
}