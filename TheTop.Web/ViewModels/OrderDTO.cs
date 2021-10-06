using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Classs;

namespace TheTop.Models
{
    public class OrderDTO : BASEEntity
    {
        public float TotalPrice { get; set; }

        public int CustomerId { get; set; }



    }
}
