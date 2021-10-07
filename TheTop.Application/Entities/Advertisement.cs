using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheTop.Application.Entities
{
    public class Advertisement 
    {
        public int AdvertisementId { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }
        public decimal Price { get; set; }
        
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        
        public int OfferId { get; set; }
        public Offer Offer { get; set; }

        public ICollection<Image> Images { get; set; }
        public ICollection<Order> Orders { get; set; }
        
        public DateTime CreatedAt = DateTime.Now; 
        public DateTime? UpdatedAt { get; set; } 

        public Advertisement()
        {
            //Reviews = new HashSet<Review>();
            Images = new HashSet<Image>();
            Orders = new HashSet<Order>();
        }
    }
}
