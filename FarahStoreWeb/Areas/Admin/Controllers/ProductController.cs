using FarahStoreApplication.UnitOfWorkPattern;
using FarahStoreModel.CommonModel;
using FarahStoreModel.Models;
using FarahStoreUtilities.Common;
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

            var products = await _unitOfWork.Product.GetAll(includeProperties: "ChaildCategory.Category,ProductImages");


            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                products = await _unitOfWork.Product.GetAll(
                   includeProperties: "ChaildCategory,ProductImages",
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
        [HttpPost]
        public async Task<IActionResult> AddImageForProduct(int ProductId)
        {
            var files = HttpContext.Request.Form.Files;
            UploadHelper UploadObj = new UploadHelper(_environment);
            List<ProductImage> productImages = new List<ProductImage>();
            for (int i = 0; i < files.Count; i++)
            {

                productImages.Add(new ProductImage
                {
                    ImagePath = UploadObj.UploadFile(files[i], $@"AdminAsset\images\products\").FileNameAddress,
                    ProductId = ProductId
                });

            }
            var res = await _unitOfWork.ProductImage.AddRange(productImages);
            await _unitOfWork.Save();

            if (res.Status)
            {
                TempData["Message"] = res.Message;
                TempData["MessageType"] = "Success";
                return Redirect("/Admin/Product/Index");
            }
            else
            {
                TempData["Message"] = res.Message;
                TempData["MessageType"] = "Error";
                return Redirect("/Admin/Product/Index");
            }
            return Json("");
        }

        public async Task<IActionResult> AddFeatureForProduct(ProductSpecifications productSpecifications)
        {
            var res = await _unitOfWork.ProductSpecifications.Add(productSpecifications);

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
