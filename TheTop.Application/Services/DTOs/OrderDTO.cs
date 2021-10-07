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
        public float TotalPrice { get; set; }
        public string ApplicationUserId { get; set; }

        public ICollection<AdvertisementDTO> Advertisements { get; set; }
    }
}
