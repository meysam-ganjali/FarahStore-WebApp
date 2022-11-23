using FarahStoreModel.CommonModel;
using FarahStoreModel.Models;

namespace FarahStoreApplication.GenericRepository.IGenericRepository;

public interface IProductRepository:IRepository<Product>
{
    Task<ResultDto> Update(Product product);
}