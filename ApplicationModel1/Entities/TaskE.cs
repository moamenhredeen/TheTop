using ApplicationModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationModel1.Entities
{
    public class TaskE : Entity
    {

        [Column(TypeName = "nvarchar(55)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Description { get; set; }

        public int Duration { get; set; }

        public DateTime DueDate { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public StatusType Status { get; set; }

        public PriorityType Priority { get; set; }

        //Niv
        public User User { get; set; }

    }
}
