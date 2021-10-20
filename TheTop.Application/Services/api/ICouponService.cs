using System.Collections.Generic;
using TheTop.Application.Services.DTOs;

namespace TheTop.Application.Services
{
    public interface ICouponService
    {
        void CreateCoupon(CouponDTO couponDto);
        void UpdateCoupon(CouponDTO couponDto);
        void RemoveCoupon(int couponId);
        ICollection<CouponDTO> GetAllCoupons();
        CouponDTO GetValidCoupon();
        CouponDTO GetCouponById(int couponId);
        bool ApplyCoupon(string codeCoupon, int orderId);
    }
}