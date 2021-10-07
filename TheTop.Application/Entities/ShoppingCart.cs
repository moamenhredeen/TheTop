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

        public decimal Price { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime CreatedAt = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
