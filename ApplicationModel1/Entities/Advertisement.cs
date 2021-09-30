using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Classs;

namespace TheTop.Models
{
    public class Advertisement : Entity
    {
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        public double Price { get; set; }

        public float Rate { get; set; }

        public int CategoryId { get; set; }

        public int CustomerId { get; set; }

        // Niv
        public Category Category { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<OrderAdv> OrderAdvs { get; set; }

        public Advertisement()
        {
            Reviews = new HashSet<Review>();
            Images = new HashSet<Image>();
            OrderAdvs = new HashSet<OrderAdv>();
        }
    }
}
