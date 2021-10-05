using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationModel1.Entities
{
    public class Review 
    {
        public int ReviewId { get; set; }
        public string Email { get; set; }
        
        // TODO : delete name and subject properties 
        [Column(TypeName = "nvarchar(55)")]
        public string Name { get; set; }
        
        [Column(TypeName = "nvarchar(255)")]
        public string Subject { get; set; }
        
        [Column(TypeName = "nvarchar(255)")]
        public string Massage { get; set; }
        
        // TODO : change approved usages ( IsActive to Approved ) 
        public bool Approved { get; set; }
        
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime CreatedAt = DateTime.Now; 
        public DateTime? UpdatedAt { get; set; } 
    }
}
