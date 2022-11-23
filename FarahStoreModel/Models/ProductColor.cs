using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarahStoreModel.Models;

public class ProductColor
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "نام رنگ را وارد کنید")]
    [MaxLength(250,ErrorMessage = "نام رنگ بیشتر از 250 کاراکتر می باشد")]
    public string ColorName { get; set; }
    [MaxLength(30, ErrorMessage = "کد هگزادسیمال رنگ بیشتر از 30 کاراکتر می باشد")]
    public string? ColorHex { get; set; }
    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
}