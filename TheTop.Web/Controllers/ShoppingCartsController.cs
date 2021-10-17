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
        private readonly ReviewService _serviceReview;
        private UserManager<ApplicationUser> _userManager;
        private readonly AdvertisementService _service;

        public ShoppingCartsController(ReviewService serviceReview,
                                      UserManager<ApplicationUser> userManager,
                                      AdvertisementService service)
        {
            this._serviceReview = serviceReview;
            this._userManager = userManager;
            this._service = service;
        }
        public async Task<IActionResult> GetAllItemToCart()
        {
            //Coupon
            CouponDTO couponDTO = _serviceReview.GetValidCoupon();
            CouponVM couponVM = new CouponVM
            {
                Code = couponDTO.Code,
                Ratio = couponDTO.Ratio,
                ValidityDate = couponDTO.ValidityDate,
                CreatedAT = couponDTO.CreatedAt,
            };
            ViewBag.Coupon = couponVM;

            var user = await _userManager.GetUserAsync(User);
            ViewBag.numItemCart = _service.GetNumItemShoppingCart(user.Id);
            ShoppingCartDTO shoppingCartDTO = _service.GetAdvertisementsInShoppingCart(user.Id);


            // if(!(shoppingCartDTO.Advertisements is null))
            //{
            //    return View(new ShoppingCartVM());
            //}
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
            _service.AddShoppingCart(id, user.Id);


            return RedirectToAction("HomePage","Home");
        }

        public async Task<IActionResult> DeleteFromCart(int AdvId)
        {
            var user = await _userManager.GetUserAsync(User);
            _service.RemoveFromShoppingCart(AdvId, user.Id);


            return RedirectToAction("GetAllItemToCart");
        }


        public async Task<IActionResult> Coupon(string textCoupon)
        {
            if (textCoupon == null)
            {
                return RedirectToAction("GetAllItemToCart");
            }
            //return Coupon If found
            CouponDTO couponDTO = _serviceReview.GetValidCoupon();
            CouponVM couponVM = new CouponVM
            {
                Code = couponDTO.Code,
                Ratio = couponDTO.Ratio,
                ValidityDate = couponDTO.ValidityDate,
                CreatedAT = couponDTO.CreatedAt,
            };
            ViewBag.Coupon = couponVM;
            /////////////////////////////////////////
           
            CouponDTO Findcoupon = _serviceReview.CheckCoupon(textCoupon);
           
            var user = await _userManager.GetUserAsync(User);
            ViewBag.numItemCart = _service.GetNumItemShoppingCart(user.Id);
            ShoppingCartDTO shoppingCartDTO = _service.GetAdvertisementsInShoppingCart(user.Id);

            ShoppingCartVM ShoppingCartVM = new ShoppingCartVM()
            {
                Advertisements = shoppingCartDTO.Advertisements.Select(a => new AdvertisementVM
                {
                    Name = a.Name,
                    Price = a.Price,
                    Category = a.CategoryName,
                    PhotosNames = a.ImagesNames.ToList()
                }).ToList(),
                TotalPrice = shoppingCartDTO.TotalPrice,
                ShoppingCartId = shoppingCartDTO.ShoppingCartId, 
            };
            if (!(Findcoupon is null))
            {
                ShoppingCartVM.Coupon = new CouponVM
                {
                    ID = Findcoupon.CouponId,
                    Ratio = Findcoupon.Ratio,
                };
            }
            else
            {
                ShoppingCartVM.Coupon = new CouponVM();
            }

            return View("GetAllItemToCart", ShoppingCartVM);
        }

       
    }
}
