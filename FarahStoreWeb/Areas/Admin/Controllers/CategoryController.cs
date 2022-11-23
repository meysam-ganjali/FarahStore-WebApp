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
                var objFromDb =await _unitOfWork.Category.GetFirstOrDefault(u => u.Id == category.Id);
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
            return View();
        }

        #endregion

    }
}
