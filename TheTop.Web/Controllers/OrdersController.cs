using Microsoft.AspNetCore.Http;
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
    public class OrdersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICouponService _couponService;
        private readonly IOrderService _orderService;
        private readonly IShoppingCartService _shoppingCartService;

        public OrdersController(
            UserManager<ApplicationUser> userManager,
            ICouponService couponService,
            IShoppingCartService shoppingCartService, 
            IOrderService orderService
        )
        {
            _userManager = userManager;
            _couponService = couponService;
            _shoppingCartService = shoppingCartService;
            _orderService = orderService;
        }


        public async Task<ActionResult> Order()
        {
            var user = await _userManager.GetUserAsync(User);
            int orderId = _orderService.AddOreder(user.Id);
            return RedirectToAction("GetOrder", new {orderId = orderId, flag = false});
        }


        public async Task<ActionResult> GetOrder(int orderId, bool flag)
        {
            var user = await _userManager.GetUserAsync(User);
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

            //Cart
            ViewBag.numItemCart = _shoppingCartService.GetNumItemShoppingCart(user.Id);

            OrderDTO orderDTO = _orderService.GetOrder(orderId);

            OrderVM orderVM = new OrderVM
            {
                Advertisements = orderDTO.Advertisements.Select(a => new AdvertisementVM
                {
                    Name = a.Name,
                    Price = a.Price,
                    Category = a.CategoryName,
                    PhotosNames = a.ImagesNames.ToList(),
                    ID = a.ID,
                }).ToList(),
                TotalPrice = orderDTO.TotalPrice,
                ID = orderDTO.OrderId,
                DiscountPrice = orderDTO.DiscountPrice,
                InvaildCoupon = flag == true ? "Invalid Coupon" : " ",
            };
            return View(orderVM);
        }

        public IActionResult ApplyCoupon(string textCoupon, int orderId)
        {
            if (textCoupon == null)
            {
                return RedirectToAction("GetOrder", new {orderId = orderId});
            }

            bool flag = _couponService.ApplyCoupon(textCoupon, orderId);

            return RedirectToAction("GetOrder", new {orderId = orderId, flag = flag == true ? false : true});
        }

        public async Task<ActionResult> Checkout(int orderId)
        {
            var user = await _userManager.GetUserAsync(User);
            bool Confirmed = _orderService.CheckoutOrder(user.Id, orderId);

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

            return View("OrderConfirmed", new OrderConfirmed {Confirm = Confirmed});
        }


        public async Task<ActionResult> OrderCancel(int orderId)
        {
            var user = await _userManager.GetUserAsync(User);

            _orderService.RemoveOrder(orderId);

            ViewBag.numItemCart = _shoppingCartService.GetNumItemShoppingCart(user.Id);

            return RedirectToAction("GetAllItemToCart", "ShoppingCarts");
        }
    }
}