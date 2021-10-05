using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationModel1.Entities
{
    public class Category 
    {
        public int CategoryId { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

       public ICollection<Advertisement> Advertisements { get; set; }

        public DateTime CreatedAt = DateTime.Now; 
        public DateTime? UpdatedAt { get; set; } 

       public Category()
        {
            Advertisements = new HashSet<Advertisement>();
        }
    }
}
