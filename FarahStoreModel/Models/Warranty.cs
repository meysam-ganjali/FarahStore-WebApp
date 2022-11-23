using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarahStoreModel.Models;

public class Warranty
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "عنوان گارانتی را اتخاب کنید")]
    [MaxLength(500,ErrorMessage = "عنوان گارانتی بشتر از 500 کاراکتر می باشد")]
    public string WarrantyName { get; set; }
    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
}