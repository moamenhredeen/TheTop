using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheTop.ViewModels
{
    public class ShoppingCartVM
    {
        public int ShoppingCartId { get; set; }
       
        public ICollection<AdvertisementVM> Advertisements { get; set; }

        public decimal TotalPrice { get; set; }

        public CouponVM Coupon { get; set; }
    }
}
