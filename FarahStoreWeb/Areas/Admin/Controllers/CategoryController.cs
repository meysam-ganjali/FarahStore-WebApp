using FarahStoreApplication.UnitOfWorkPattern;
using FarahStoreWeb.Data;
using Microsoft.AspNetCore.Mvc;

namespace FarahStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public CategoryController(IUnitOfWork unitOfWork, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
        }
        public async Task<IActionResult> Index(string? searchKey)
        {
            var res = await _unitOfWork.Category.GetAll(includeProperties: "ChaildCategories");
            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                res = res.Where(u => u.Name.Contains(searchKey));
            }
            return View( res );
        }
    }
}
