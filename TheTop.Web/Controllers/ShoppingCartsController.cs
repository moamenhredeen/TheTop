using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Application.Entities;
using TheTop.Application.Services;
using TheTop.Application.Services.DTOs;
using TheTop.ViewModels;

namespace TheTop.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ICouponService _couponService;

        public ShoppingCartsController(
            UserManager<ApplicationUser> userManager,
            IShoppingCartService shoppingCartService, 
            ICouponService couponService
        )
        {
            _userManager = userManager;
            _shoppingCartService = shoppingCartService;
            _couponService = couponService;
        }

        public async Task<IActionResult> GetAllItemToCart()
        {
            //Coupon
            CouponDTO couponDTO = _couponService.GetValidCoupon();
            CouponVM couponVM = new CouponVM
            {
                Code = couponDTO.Code,
                Ratio = couponDTO.Ratio,
                ValidityDate = couponDTO.ValidityDate,
                CreatedAT = couponDTO.CreatedAt,
            };
            ViewBag.Coupon = couponVM;

            var user = await _userManager.GetUserAsync(User);
            ViewBag.numItemCart = _shoppingCartService.GetNumItemShoppingCart(user.Id);
            ShoppingCartDTO shoppingCartDTO = _shoppingCartService.GetAdvertisementsInShoppingCart(user.Id);

            ShoppingCartVM ShoppingCartVM = new ShoppingCartVM()
            {
                Advertisements = shoppingCartDTO.Advertisements.Select(a => new AdvertisementVM
                {
                    Name = a.Name,
                    Price = a.Price,
                    Category = a.CategoryName,
                    PhotosNames = a.ImagesNames.ToList(),
                    ID = a.ID,
                }).ToList(),
                TotalPrice = shoppingCartDTO.TotalPrice,
                ShoppingCartId = shoppingCartDTO.ShoppingCartId,
                Coupon = new CouponVM(),
            };
            return View(ShoppingCartVM);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            _shoppingCartService.AddShoppingCart(id, user.Id);


            return RedirectToAction("HomePage", "Home");
        }

        public async Task<IActionResult> DeleteFromCart(int AdvId)
        {
            var user = await _userManager.GetUserAsync(User);
            _shoppingCartService.RemoveFromShoppingCart(AdvId, user.Id);


            return RedirectToAction("GetAllItemToCart");
        }
    }
}