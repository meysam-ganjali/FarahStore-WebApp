using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarahStoreModel.Models;

public class ProductReView
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "عنوان بررسی کالا را وارد کنید")]
    [MaxLength(100,ErrorMessage = "تعداد کاراکتر مجاز 1000 عدد می باشد شما بیشتر از 1000 کاراکتر وارد کرده اید")]
    public string ReviewTitle { get; set; }
    [Required(ErrorMessage = "متن بررسی کالا را وارد کنید")]
    public string ReviewDescription { get; set; }
    public string? ReviewImagePath { get; set; }
    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
}