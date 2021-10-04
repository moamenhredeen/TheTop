using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationModel1.Entities
{
    public class Advertisement : Entity
    {
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        public double Price { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Offer")]
        public int OfferId { get; set; }

        // Niv
        public Category Category { get; set; }
        public User User { get; set; }
        public Offer Offer { get; set; }

        //public ICollection<Review> Reviews { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<OrderAdv> OrderAdvs { get; set; }

        public Advertisement()
        {
            //Reviews = new HashSet<Review>();
            Images = new HashSet<Image>();
            OrderAdvs = new HashSet<OrderAdv>();
        }
    }
}
