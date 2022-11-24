using FarahStoreApplication.UnitOfWorkPattern;
using FarahStoreModel.Models;
using FarahStoreWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace FarahStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        #region Ctor


        private readonly IUnitOfWork _unitOfWork;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public ProductController(IUnitOfWork unitOfWork, IHostingEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
        }

        #endregion

        #region Product Action Method

        public async Task<IActionResult> Index(string? searchKey)
        {

            var products = await _unitOfWork.Product.GetAll(includeProperties: "ChaildCategory.Category");


            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                products = await _unitOfWork.Product.GetAll(
                   includeProperties: "ChaildCategory",
                   filter: u => u.Name.Contains(searchKey));
            }
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.Category = new SelectList(await _unitOfWork.ChaildCategory.GetAll(includeProperties: "Category"),
                "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            var res = await _unitOfWork.Product.Add(product);

            if (res.Status)
            {
                TempData["Message"] = res.Message;
                TempData["MessageType"] = "Success";
                await _unitOfWork.Save();
                return Redirect("/Admin/Product/Index");
                
            }

            TempData["Message"] = res.Message;
            TempData["MessageType"] = "Error";
            return Redirect("/Admin/Product/Index");
        }
        #endregion

    }
}
