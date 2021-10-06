using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTop.Application.Entities
{
    public class Offer 
    {
        public int OfferId { get; set; }
        public float Ratio { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<Advertisement> Advertisements { get; set; }
        
        public DateTime CreatedAt = DateTime.Now; 
        public DateTime? UpdatedAt { get; set; } 
    }
}
