using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTop.Application.Services.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string ApplicationUserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<AdvertisementDTO> Advertisements { get; set; }


    }
}
