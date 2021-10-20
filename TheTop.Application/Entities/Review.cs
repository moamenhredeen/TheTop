using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheTop.Application.Entities
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
        
        public bool Approved { get; set; }
        
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } 
    }
}
