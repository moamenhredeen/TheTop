using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTop.Application.Entities;

namespace TheTop.Application.Services.DTOs
{
   public class ShoppingCartDTO
    {
        public int ShoppingCartId { get; set; }
       

        public ICollection<AdvertisementDTO> Advertisements { get; set; }

        public decimal TotalPrice { get; set; }

    }
}
