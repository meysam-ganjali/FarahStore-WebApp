using FarahStoreApplication.UnitOfWorkPattern;
using FarahStoreUtilities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FarahStoreWeb.ViewComponents;

public class MenuComponent : ViewComponent
{
    private readonly IUnitOfWork _unitOfWork;
    public MenuComponent(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var res = await _unitOfWork.Category.GetAll(includeProperties: "ChaildCategories");
        return View(viewName: "MenuComponent", res);
    }

}