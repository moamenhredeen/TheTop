using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Classs;

namespace TheTop.Models
{
    public class Order : Entity
    {
        public float TotalPrice { get; set; }

        public int CustomerId { get; set; }

        //Niv 
        public Customer Customer { get; set; }
        public ICollection<OrderAdv> OrderAdvs { get; set; }



        public Order()
        {
            OrderAdvs = new HashSet<OrderAdv>();
        }

    }
}
