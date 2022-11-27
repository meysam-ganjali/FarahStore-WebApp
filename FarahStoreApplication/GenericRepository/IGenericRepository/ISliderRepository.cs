using FarahStoreModel.CommonModel;
using FarahStoreModel.Models;

namespace FarahStoreApplication.GenericRepository.IGenericRepository;

public interface ISliderRepository:IRepository<Slider>
{
    Task<ResultDto> Update(Slider slider);
}