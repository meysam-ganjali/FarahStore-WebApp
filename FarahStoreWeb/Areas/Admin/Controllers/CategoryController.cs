using FarahStoreApplication.UnitOfWorkPattern;
using FarahStoreModel.Models;
using FarahStoreUtilities.Common;
using FarahStoreWeb.Data;
using Microsoft.AspNetCore.Mvc;

namespace FarahStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

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
            return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {

            var category = await _unitOfWork.Category.GetFirstOrDefault(filter: u => u.Id.Equals(id));
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Upsert(Category category)
        {
            if (category.Id == 0)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count() > 0)
                {
                    UploadHelper UploadObj = new UploadHelper(_environment);
                    var uploadedResultImg = UploadObj.UploadFile(files[0], $@"AdminAsset\images\category\");
                    category.LogoPath = uploadedResultImg.FileNameAddress;
                }

                var res = await _unitOfWork.Category.Add(category);
                await _unitOfWork.Save();
                if (res.Status)
                {
                    TempData["Message"] = res.Message;
                    TempData["MessageType"] = "Success";
                    return Redirect("/Admin/Category/Index");
                }
                else
                {
                    TempData["Message"] = res.Message;
                    TempData["MessageType"] = "Error";
                    return Redirect("/Admin/Category/Index");
                }

                //Create
            }
            else
            {
                //Update
            }
            return View();
        }
    }
}
