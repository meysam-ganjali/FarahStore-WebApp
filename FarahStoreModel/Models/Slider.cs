using System.ComponentModel.DataAnnotations;

namespace FarahStoreModel.Models;

public class Slider
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "عنوان اسلایدر را انتخاب کنید")]
    [MaxLength(500,ErrorMessage = "تعداد حروف مجاز 500 کاراکتر میباشد")]
    public string Title { get; set; }
    [Required(ErrorMessage = "مکان نمایش اسلایدر را مشخص کنید")]
    [Range(1,9,ErrorMessage = "مقدار صحیح اعداد بین 1-9 میباشد")]
    public Possition Possition { get; set; }
    [Required(ErrorMessage = "لینک مربوط به اسلایدر را وارد نکرده اید")]
    public string Link { get; set; }
    public string ImagePath { get; set; }

}