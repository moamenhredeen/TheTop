using ApplicationModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheTop.Application.Entities
{
    public class TaskEntity 
    {
        public int TaskEntityId { get; set; }
        public int Duration { get; set; }
        public DateTime DueDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        public StatusType Status { get; set; }
        public PriorityType Priority { get; set; }

        public string  ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime CreatedAt = DateTime.Now; 
        public DateTime? UpdatedAt { get; set; } 
    }
}
