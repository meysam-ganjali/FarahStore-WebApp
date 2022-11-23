using FarahStoreModel.CommonModel;
using FarahStoreModel.Models;

namespace FarahStoreApplication.GenericRepository.IGenericRepository;

public interface IChaildCategoryRepository:IRepository<ChaildCategory>
{
    Task<ResultDto> Update(ChaildCategory chaildCategory);
}