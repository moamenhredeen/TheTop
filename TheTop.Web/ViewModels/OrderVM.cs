using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Classs;

namespace TheTop.ViewModels
{
    public class OrderVM : BASEEntity
    {
        public decimal TotalPrice { get; set; }

        public decimal DiscountPrice { get; set; }
        public int CustomerId { get; set; }

        public ICollection<AdvertisementVM> Advertisements { get; set; }

    }
}
