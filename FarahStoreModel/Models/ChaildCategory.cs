using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarahStoreModel.Models;

public class ChaildCategory
{

    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "نام دسته بندی را وارد نکرده اید")]
    [MaxLength(300, ErrorMessage = "عنوان دسته بندی بیشتر از 300 کاراکتر است")]
    public string Name { get; set; }
    public string? LogoPath { get; set; }
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
}