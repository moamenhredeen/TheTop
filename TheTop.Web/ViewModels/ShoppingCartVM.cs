using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheTop.ViewModels
{
    public class ShoppingCartVM
    {
        public int ShoppingCartId { get; set; }
        public int AdvertisementId { get; set; }

        public string ApplicationUserId { get; set; }
        public AdvertisementVM Advertisement { get; set; }

        public DateTime CreatedAt = DateTime.Now;
    }
}
