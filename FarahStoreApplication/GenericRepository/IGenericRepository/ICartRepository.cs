using FarahStoreModel.CommonModel;
using FarahStoreModel.Models;

namespace FarahStoreApplication.GenericRepository.IGenericRepository;

public interface ICartRepository:IRepository<Cart>
{
    Task<ResultDto> Decrease(int DecreaseNumber, int CartItemId);
    Task<ResultDto> Increase(int IncreaseNumber, int CartItemId);
}