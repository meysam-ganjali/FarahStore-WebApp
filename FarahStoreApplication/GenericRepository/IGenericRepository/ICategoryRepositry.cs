using FarahStoreModel.CommonModel;
using FarahStoreModel.Models;

namespace FarahStoreApplication.GenericRepository.IGenericRepository;

public interface ICategoryRepositry:IRepository<Category>
{
    Task<ResultDto> Update(Category category);
}
