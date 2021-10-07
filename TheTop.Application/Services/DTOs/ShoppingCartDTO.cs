using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTop.Application.Entities;

namespace TheTop.Application.Services.DTOs
{
    class ShoppingCartDTO
    {
        public int ShoppingCartId { get; set; }
        public decimal Price { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime CreatedAt = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
    }
}
