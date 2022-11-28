using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FarahStoreModel.Models;

public class ApplicationUser: IdentityUser
{
    public bool IsActive { get; set; } = true;
    [Required(ErrorMessage = "نام و نام خانوادگی را وارد کنید")]
    public string FullName { get; set; }
    public string Address { get; set; }
}