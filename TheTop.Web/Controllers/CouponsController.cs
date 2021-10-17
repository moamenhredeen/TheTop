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
        private readonly ReviewService _service;
        public CouponsController(ReviewService service)
        {
            this._service = service;    
        }

        public ActionResult Index()
        {
            List<CouponDTO> couponsDtoList = _service.GetAllCoupons().ToList();
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

            _service.CreateCoupon(new CouponDTO
            {
                Ratio = couponVM.Ratio,
                ValidityDate = couponVM.ValidityDate,          
            });
            return RedirectToAction(nameof(Index));
          
        }

       
        public ActionResult Edit(int id)
        {
            CouponDTO couponDTO = _service.GetCouponById(id);
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

            _service.UpdateCoupon(new CouponDTO
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
            CouponDTO couponDTO = _service.GetCouponById(id);
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
            _service.RemoveCoupon(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
