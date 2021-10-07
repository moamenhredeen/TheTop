using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheTop.Application.Entities
{
    public class Order 
    {
        public int OrderId { get; set; }
        public float TotalPrice { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        
        public ICollection<Advertisement> Advertisements { get; set; }


        public DateTime CreatedAt = DateTime.Now; 
        public DateTime? UpdatedAt { get; set; } 

        public Order()
        {
            Advertisements = new HashSet<Advertisement>();
        }

    }
}
