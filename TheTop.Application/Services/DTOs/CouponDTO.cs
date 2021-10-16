using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTop.Application.Services.DTOs
{
   public class CouponDTO
    {
        public int CouponId { get; set; }
        public string Code { get; set; }
        public float Ratio { get; set; }
        public DateTime ValidityDate { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
