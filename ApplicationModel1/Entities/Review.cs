using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationModel1.Entities
{
    public class Review : Entity
    {
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Column(TypeName = "nvarchar(55)")]
        public string Name { get; set; }
        public string Email { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string Subject { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Massage { get; set; }

        public bool IsActiv { get; set; }
        



        //Niv
        public User User { get; set; }

    }
}
