using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TheTop.Application.Entities
{
    public class Coupon
    {
        public int CouponId { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string Code { get; set; }
        public float Ratio { get; set; }
        public DateTime ValidityDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Order> Orders { get; set; }
        public Coupon()
        {
            Orders = new HashSet<Order>(); 
        }
    }
}

