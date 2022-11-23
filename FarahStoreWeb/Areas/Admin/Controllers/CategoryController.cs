using FarahStoreApplication.UnitOfWorkPattern;
using FarahStoreModel.Models;
using FarahStoreUtilities.Common;
using FarahStoreWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FarahStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        #region Ctor And Private Interface

        private readonly IUnitOfWork _unitOfWork;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public CategoryController(IUnitOfWork unitOfWork, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
        }

        #endregion


        #region Parent Category Action

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
            if (id != null && category == null)
            {
                TempData["Message"] = "دسته یافت نشد";
                TempData["MessageType"] = "Error";
                return Redirect("/Admin/Category/Index");
            }
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Upsert(Category category)
        {
            var files = HttpContext.Request.Form.Files;
            UploadHelper UploadObj = new UploadHelper(_environment);
            if (category.Id == 0)
            {

                if (files.Count() > 0)
                {

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
                var objFromDb = await _unitOfWork.Category.GetFirstOrDefault(u => u.Id == category.Id);
                if (files.Count() > 0)
                {
                    //delete the old image
                    var oldImagePath = Path.Combine(_environment.WebRootPath, objFromDb.LogoPath.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    var uploadedResultImg = UploadObj.UploadFile(files[0], $@"AdminAsset\images\category\");
                    category.LogoPath = uploadedResultImg.FileNameAddress;
                }
                else
                {
                    category.LogoPath = objFromDb.LogoPath;
                }

                var updateRes = await _unitOfWork.Category.Update(category);
                await _unitOfWork.Save();
                if (updateRes.Status)
                {
                    TempData["Message"] = updateRes.Message;
                    TempData["MessageType"] = "Success";
                    return Redirect("/Admin/Category/Index");
                }
                else
                {
                    TempData["Message"] = updateRes.Message;
                    TempData["MessageType"] = "Error";
                    return Redirect("/Admin/Category/Index");
                }
                //Update
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _unitOfWork.Category.GetFirstOrDefault(filter: u => u.Id.Equals(id));
            if (id == null || category == null)
            {
                TempData["Message"] = "دسته یافت نشد";
                TempData["MessageType"] = "Error";
                return Redirect("/Admin/Category/Index");
            }
            if (!string.IsNullOrWhiteSpace(category.LogoPath))
            {
                //delete the old image
                var oldImagePath = Path.Combine(_environment.WebRootPath, category.LogoPath.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            var res = await _unitOfWork.Category.Remove(category);
            await _unitOfWork.Save();
            return Json(res);
        }

        #endregion

        #region Chaild Category Action

        public async Task<IActionResult> ChaildList(string? searchkey, int id)
        {
            var chaildCategoryFromDb = await _unitOfWork.ChaildCategory.GetAll(includeProperties: "Category", filter: u => u.CategoryId.Equals(id));
            if (!string.IsNullOrWhiteSpace(searchkey))
            {
                chaildCategoryFromDb = chaildCategoryFromDb.Where(u =>
                    u.Name.Contains(searchkey) || u.Category.Name.Contains(searchkey));
            }
            return View(chaildCategoryFromDb);
        }

        [HttpGet]
        public async Task<IActionResult> AddChaildCategory(int id)
        {
            var parentCategory = await _unitOfWork.Category.GetFirstOrDefault(filter: u => u.Id.Equals(id));
            if (parentCategory == null)
            {
                TempData["Message"] = "دسته پدر یافت نشد یافت نشد";
                TempData["MessageType"] = "Error";
                return Redirect("/Admin/Category/Index");
            }

            ViewBag.ParentCategoryName = parentCategory.Name;
            ViewBag.ParentCategoryId = parentCategory.Id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddChaildCategory(ChaildCategory chaildCategory)
        {
            var files = HttpContext.Request.Form.Files;
            UploadHelper UploadObj = new UploadHelper(_environment);

            if (files.Count() > 0)
            {

                var uploadedResultImg = UploadObj.UploadFile(files[0], $@"AdminAsset\images\category\chaildcategory\");
                chaildCategory.LogoPath = uploadedResultImg.FileNameAddress;
            }

            var res = await _unitOfWork.ChaildCategory.Add(chaildCategory);
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

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateChaildCategory(int id)
        {
            var parentCategory = await _unitOfWork.Category.GetAll();
            ViewBag.ParenyCategory = new SelectList(parentCategory, "Id", "Name");
            
            var chaildCategory = await _unitOfWork.ChaildCategory.GetFirstOrDefault(filter: u => u.Id.Equals(id),includeProperties:"Category");
            if (chaildCategory == null)
            {
                TempData["Message"] = "دسته پدر یافت نشد یافت نشد";
                TempData["MessageType"] = "Error";
                return Redirect("/Admin/Category/Index");
            }
            return View(chaildCategory);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateChaildCategory(ChaildCategory chaildCategory)
        {
            var objFromDb = await _unitOfWork.ChaildCategory.GetFirstOrDefault(u => u.Id == chaildCategory.Id);
            var files = HttpContext.Request.Form.Files;
            if (files.Count() > 0)
            {
                UploadHelper UploadObj = new UploadHelper(_environment);
                //delete the old image
                var oldImagePath = Path.Combine(_environment.WebRootPath, objFromDb.LogoPath.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                var uploadedResultImg = UploadObj.UploadFile(files[0], $@"AdminAsset\images\category\chaildcategory\");
                chaildCategory.LogoPath = uploadedResultImg.FileNameAddress;
            }
            else
            {
                chaildCategory.LogoPath = objFromDb.LogoPath;
            }

            var updateRes = await _unitOfWork.ChaildCategory.Update(chaildCategory);
            await _unitOfWork.Save();
            if (updateRes.Status)
            {
                TempData["Message"] = updateRes.Message;
                TempData["MessageType"] = "Success";
                return Redirect("/Admin/Category/Index");
            }
            else
            {
                TempData["Message"] = updateRes.Message;
                TempData["MessageType"] = "Error";
                return Redirect("/Admin/Category/Index");
            }
        }
        #endregion

    }
}
