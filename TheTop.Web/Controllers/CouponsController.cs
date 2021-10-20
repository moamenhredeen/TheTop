using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Application.Services;
using TheTop.Application.Services.DTOs;
using TheTop.ViewModels;

namespace TheTop.Controllers
{
    public class CouponsController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponsController(ICouponService couponService)
        {
            _couponService = couponService;    
        }

        public ActionResult Index()
        {
            List<CouponDTO> couponsDtoList = _couponService.GetAllCoupons().ToList();
            List<CouponVM> couponsVMList = new();
            couponsDtoList.ForEach(c => {
                couponsVMList.Add(new CouponVM
                {
                    Code = c.Code,
                    Ratio = c.Ratio,
                    CreatedAT = c.CreatedAt,
                    ValidityDate = c.ValidityDate,
                    ID = c.CouponId,
                });
            });
            return View(couponsVMList);
        }       

        public ActionResult Create()
        {
            return View(new CouponVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CouponVM couponVM)
        {
            if ( ! ModelState.IsValid)
            {
                return View();
            }

            _couponService.CreateCoupon(new CouponDTO
            {
                Ratio = couponVM.Ratio,
                ValidityDate = couponVM.ValidityDate,          
            });
            return RedirectToAction(nameof(Index));
          
        }

       
        public ActionResult Edit(int id)
        {
            CouponDTO couponDTO = _couponService.GetCouponById(id);
            return View(new CouponVM { 
            Code = couponDTO.Code,
            Ratio = couponDTO.Ratio,
            CreatedAT = couponDTO.CreatedAt,
            ValidityDate = couponDTO.ValidityDate,
            ID = couponDTO.CouponId,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CouponVM couponVM)
        {
            if (! ModelState.IsValid)
            {
                return View();
            }

            _couponService.UpdateCoupon(new CouponDTO
            {
                Ratio = couponVM.Ratio,
                ValidityDate = couponVM.ValidityDate,
                CouponId = couponVM.ID,
                Code = couponVM.Code,
            });
            return RedirectToAction(nameof(Index));
        }


        public ActionResult Delete(int id)
        {
            CouponDTO couponDTO = _couponService.GetCouponById(id);
            return View(new CouponVM
            {
                Code = couponDTO.Code,
                Ratio = couponDTO.Ratio,
                CreatedAT = couponDTO.CreatedAt,
                ValidityDate = couponDTO.ValidityDate,
                ID = couponDTO.CouponId,
            });
        }

       
        public ActionResult DeleteCoupon(int id)
        {
            _couponService.RemoveCoupon(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
