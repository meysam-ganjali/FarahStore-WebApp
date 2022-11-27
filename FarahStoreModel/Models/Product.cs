using System.ComponentModel.DataAnnotations.Schema;

namespace FarahStoreModel.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Slug { get; set; }
    public string ShortDescription { get; set; }
    public string FullDescription { get; set; }
    public int NumberOfEntries { get; set; }
    public long Price { get; set; }
    public long? OldPrice { get; set; }
    public int ShowCount { get; set; } = 0;
    public bool IsSelectedProduct { get; set; } = false;
    public bool IsSpecialSale { get; set; } = false;
    public bool DiscountLable { get; set; } = false;
    public int ChaildCategoryCategoryId { get; set; }
    [ForeignKey("ChaildCategoryCategoryId")]
    public ChaildCategory ChaildCategory { get; set; }
    public ICollection<ProductImage> ProductImages { get; set; }
    public ICollection<ProductColor> ProductColors { get; set; }
    public ICollection<Warranty> Warranties { get; set; }
    public ICollection<ProductSpecifications> ProductSpecifications { get; set; }
}