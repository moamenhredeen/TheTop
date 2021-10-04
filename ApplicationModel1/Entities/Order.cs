using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationModel1.Entities
{
    public class Order : Entity
    {
        public float TotalPrice { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        //Niv 
        public User User { get; set; }
        public IEnumerable<OrderAdv> OrderAdvs { get; set; }



        public Order()
        {
            OrderAdvs = new HashSet<OrderAdv>();
        }

    }
}
