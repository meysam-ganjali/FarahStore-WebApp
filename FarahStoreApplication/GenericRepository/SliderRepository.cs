using FarahStoreApplication.GenericRepository.IGenericRepository;
using FarahStoreModel.CommonModel;
using FarahStoreModel.Models;
using FarahStoreWeb.Data;

namespace FarahStoreApplication.GenericRepository;

public class SliderRepository : Repository<Slider>, ISliderRepository
{
    private readonly DatabaseContex _db;

    public SliderRepository(DatabaseContex db) : base(db)
    {
        _db = db;
    }
    public async Task<ResultDto> Update(Slider slider)
    {
        var sliderFromDb = await _db.Sliders.FindAsync(slider.Id);
        if (sliderFromDb == null)
        {
            return new ResultDto()
            {
                Status = false,
                Message = "اسلایدر یافت نشد"
            };
        }

        if (!string.IsNullOrWhiteSpace(slider.ImagePath))
        {
            sliderFromDb.ImagePath = slider.ImagePath;
        }
        sliderFromDb.Link=slider.Link;
        sliderFromDb.Possition=slider.Possition;
        sliderFromDb.Title = slider.Title;
        return new ResultDto()
        {
            Status = true,
        };
    }
}