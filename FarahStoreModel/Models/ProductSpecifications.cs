using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarahStoreModel.Models;

public class ProductSpecifications
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage="عنوان مشخصه را وارد کنید")]
    [MaxLength(300,ErrorMessage = "تعداد کاراکتر بیشتر از حد مجاز است . حد مجاز تا 300 کاراکتر")]
    public string Title { get; set; }
    [Required(ErrorMessage = "مقدار مشخصه را وارد کنید")]
    [MaxLength(300, ErrorMessage = "تعداد کاراکتر بیشتر از حد مجاز است . حد مجاز تا 300 کاراکتر")]
    public string Value { get; set; }
    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
}