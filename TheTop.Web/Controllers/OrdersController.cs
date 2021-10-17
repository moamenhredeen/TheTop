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

        private readonly ReviewService _serviceReview;
        private UserManager<ApplicationUser> _userManager;
        private readonly AdvertisementService _service;

        public OrdersController(ReviewService serviceReview,
                                      UserManager<ApplicationUser> userManager,
                                      AdvertisementService service)
        {
            this._serviceReview = serviceReview;
            this._userManager = userManager;
            this._service = service;
        }


        public async Task<ActionResult> Order()
        {
            var user = await _userManager.GetUserAsync(User);
            _service.AddOreder(user.Id);
            return RedirectToAction("GetOrder");
        }

        // GET: OrderDTOsController
        public async Task<ActionResult> GetOrder()
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

            OrderDTO orderDTO = _service.GetOrder(user.Id);

            OrderVM orderVM = new OrderVM { 
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
            };
            return View(orderVM);
        }

        // GET: OrderDTOsController/Details/5
        public ActionResult Details(int id)
        {
            return View("Error");
        }

        // GET: OrderDTOsController/Create
        public ActionResult Create()
        {
            return View("Error");
        }

        // POST: OrderDTOsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            return View("Error");
        }
        

        // GET: OrderDTOsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View("Error");
        }

        // POST: OrderDTOsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            return View("Error");
        }

        // GET: OrderDTOsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View("Error");
        }

        // POST: OrderDTOsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            return View("Error");
        }
    }
}
