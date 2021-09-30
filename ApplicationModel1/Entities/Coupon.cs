using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Classs;

namespace TheTop.Models
{
    public class Coupon : Entity
    {
        [Column(TypeName = "nvarchar(55)")]
        public string Name { get; set; }

        public float Ratio { get; set; }
    }
}
