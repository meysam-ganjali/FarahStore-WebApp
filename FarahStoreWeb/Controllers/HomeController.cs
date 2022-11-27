using FarahStoreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FarahStoreApplication.UnitOfWorkPattern;
using FarahStoreModel.ViewModels;

namespace FarahStoreWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            IndexPageViewModel model = new IndexPageViewModel();
            var slider = await _unitOfWork.Slider.GetAll();
            model.SliderLst = slider.ToList();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}