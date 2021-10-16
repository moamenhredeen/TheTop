using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTop.Application.Entities
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }

        public ICollection<Advertisement> Advertisements { get; set; }
       

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public ShoppingCart()
        {
            Advertisements = new HashSet<Advertisement>();
        }
    }
}
