using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarahStoreModel.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "نام دسته بندی را وارد نکرده اید")]
    [MaxLength(300,ErrorMessage = "عنوان دسته بندی بیشتر از 300 کاراکتر است")]
    public string Name { get; set; }
    public string? LogoPath { get; set; }
    public int? ParentCategoryId { get; set; }
    [ForeignKey("ParentCategoryId")]
    public virtual Category ParentCategory { get; set; }
    
    public virtual ICollection<Category> SubCategories { get; set; }
}