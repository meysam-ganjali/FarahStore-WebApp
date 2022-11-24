using FarahStoreModel.CommonModel;
using FarahStoreModel.Models;

namespace FarahStoreApplication.GenericRepository.IGenericRepository;

public interface IProductColorRepository:IRepository<ProductColor>
{
    Task<ResultDto> Update(ProductColor productColor);
}