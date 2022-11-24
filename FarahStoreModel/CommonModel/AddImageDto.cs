using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FarahStoreModel.CommonModel;

public class AddImageDto
{
    public List<IFormFile> Image { get; set; }
}