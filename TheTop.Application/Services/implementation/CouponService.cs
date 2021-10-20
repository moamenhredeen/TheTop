using System;
using System.Collections.Generic;
using System.Linq;
using TheTop.Application.Dao;
using TheTop.Application.Entities;
using TheTop.Application.Services.DTOs;

namespace TheTop.Application.Services
{
    public class CouponService : ICouponService
    {
        
        private readonly AppDbContext _appDbContext;
        public CouponService(AppDbContext appDbContext) => _appDbContext = appDbContext;
        // Coupon Service 
        public void CreateCoupon(CouponDTO couponDto)
        {
            
        _appDbContext.Add(new Coupon
            {
                Code = "ABZ"+couponDto.Ratio,
                Ratio = couponDto.Ratio,
                ValidityDate = couponDto.ValidityDate,
            });
            _appDbContext.SaveChanges();
        }

        public void UpdateCoupon(CouponDTO couponDto)
        {
            _appDbContext.Update(new Coupon
            {
                CouponId = couponDto.CouponId,
                Code = couponDto.Code,
                Ratio = couponDto.Ratio,
                ValidityDate = couponDto.ValidityDate,
            });
            _appDbContext.SaveChanges();
        }

        public void RemoveCoupon(int couponId)
        {
            _appDbContext.Remove(new Coupon { CouponId = couponId });
            _appDbContext.SaveChanges();
        }

        public ICollection<CouponDTO> GetAllCoupons()
        {
            var coupons = _appDbContext.Coupons.ToList();

            return coupons.Select(coupon => new CouponDTO
            {
                Code = coupon.Code,
                ValidityDate = coupon.ValidityDate,
                Ratio = coupon.Ratio,
                CouponId = coupon.CouponId,
                CreatedAt = coupon.CreatedAt
            }).ToList();
        }

        public CouponDTO GetValidCoupon()
        {
            var coupon = _appDbContext.Coupons.SingleOrDefault(coupon => coupon.ValidityDate > DateTime.Now);

             if(coupon is null)
            {
                return new CouponDTO();
            }

            return new CouponDTO { 
             ValidityDate = coupon.ValidityDate,
             Code = coupon.Code,
             Ratio = coupon.Ratio,
             CreatedAt = coupon.CreatedAt,
            };
        }

        public CouponDTO GetCouponById(int couponId)
        {
            var coupon = _appDbContext.Coupons
                .SingleOrDefault(coupon => coupon.CouponId == couponId);
            return new CouponDTO
            {
                ValidityDate = coupon.ValidityDate,
                Code = coupon.Code,
                Ratio = coupon.Ratio,
                CreatedAt = coupon.CreatedAt,
                CouponId = coupon.CouponId,
            };
        }
        public bool ApplyCoupon(string codeCoupon, int orderId)
        {
            var coupon = _appDbContext.Coupons
                .SingleOrDefault(coupon => coupon.Code == codeCoupon && coupon.ValidityDate > DateTime.Now);

            if (coupon is null)
            {
                return false;
            }
            var order = _appDbContext.Orders.Single(order => order.OrderId == orderId);

            order.CouponId = coupon.CouponId;
            order.DiscountPrice = order.TotalPrice - (order.TotalPrice * ((decimal)coupon.Ratio / 100));
            _appDbContext.SaveChanges();

            return true;
            
        }
    }
}