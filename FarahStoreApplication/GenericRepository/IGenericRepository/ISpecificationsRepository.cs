using FarahStoreModel.CommonModel;
using FarahStoreModel.Models;

namespace FarahStoreApplication.GenericRepository.IGenericRepository;

public interface ISpecificationsRepository:IRepository<ProductSpecifications>
{
    Task<ResultDto> Update(ProductSpecifications specifications);
}