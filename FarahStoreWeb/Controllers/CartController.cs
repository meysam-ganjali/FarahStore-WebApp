using System.Collections;
using FarahStoreApplication.UnitOfWorkPattern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FarahStoreModel.Models;

namespace FarahStoreWeb.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(int UserId)
        {
            IEnumerable<Cart> ShoppingCarts = new List<Cart>();
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCarts = await _unitOfWork.Cart.GetAll(filter: u => u.ApplicationUserId.Equals(claim.Value), includeProperties: "Product,ApplicationUser,Product.ProductImages");
            }


            return View(ShoppingCarts);
        }


        public async Task<IActionResult> AddItemToCart(int id)
        {

            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userCarts =
                await _unitOfWork.Cart.GetAll(filter: u => u.ApplicationUserId.Equals(claim.Value),includeProperties: "Product,ApplicationUser");

            var productIsExist = userCarts.FirstOrDefault(u => u.Product.Id.Equals(id));
            if (productIsExist == null)
            {
                Cart cart = new Cart
                {
                    ApplicationUserId = claim.Value,
                    Count = 1,
                    ProductId = id,
                };
                await _unitOfWork.Cart.Add(cart);
                await _unitOfWork.Save();
                return Redirect("/Home/Index");
            }
            else
            {
                //await _unitOfWork.Cart.Increase(1, productIsExist.Id);
                await _unitOfWork.Save();
                return Redirect("/Home/Index");
            }

        }

        public async Task<IActionResult> PlusItem(int id)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userCarts =
                await _unitOfWork.Cart.GetFirstOrDefault(filter: u => u.ApplicationUserId.Equals(claim.Value), includeProperties: "Product,ApplicationUser");


            await _unitOfWork.Cart.Increase(id);
                await _unitOfWork.Save();
                return Redirect("/Home/Index");
            
        }
    }
}
