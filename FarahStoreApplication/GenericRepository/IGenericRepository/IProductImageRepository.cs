using FarahStoreModel.CommonModel;
using FarahStoreModel.Models;

namespace FarahStoreApplication.GenericRepository.IGenericRepository;

public interface IProductImageRepository:IRepository<ProductImage>
{
    Task<ResultDto> Update(ProductImage productImage);
}