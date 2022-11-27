using FarahStoreApplication.UnitOfWorkPattern;
using FarahStoreModel.Models;
using FarahStoreUtilities.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace FarahStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        #region Ctor

        private readonly IUnitOfWork _unitOfWork;
        private IHostingEnvironment _environment;
        public SliderController(IUnitOfWork unitOfWork, IHostingEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
        }

        #endregion

        #region Slider Action Method

        [HttpGet]
        public async Task<IActionResult> Index(string? serachKey)
        {
           
            var model = await _unitOfWork.Slider.GetAll();
            if (!string.IsNullOrWhiteSpace(serachKey))
            {
                model = await _unitOfWork.Slider.GetAll(filter: u => u.Title.Equals(serachKey));
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSlider(Slider slider)
        {
            var files = HttpContext.Request.Form.Files;
            UploadHelper UploadObj = new UploadHelper(_environment);
            slider.ImagePath = UploadObj.UploadFile(files[0], $@"AdminAsset\images\sliders\").FileNameAddress;
            var res = await _unitOfWork.Slider.Add(slider);
            await _unitOfWork.Save();
            if (res.Status)
            {
                TempData["Message"] = res.Message;
                TempData["MessageType"] = "Success";
                return Redirect("/Admin/Slider/Index");
            }
            else
            {
                TempData["Message"] = res.Message;
                TempData["MessageType"] = "Error";
                return Redirect("/Admin/Slider/Index");
            }
        }
        #endregion

    }
}
