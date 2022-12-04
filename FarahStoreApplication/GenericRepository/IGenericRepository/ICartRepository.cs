using FarahStoreModel.CommonModel;
using FarahStoreModel.Models;

namespace FarahStoreApplication.GenericRepository.IGenericRepository;

public interface ICartRepository:IRepository<Cart>
{
    Task<ResultDto> Decrease( int CartItemId);
    Task<ResultDto> Increase(int CartItemId);
}