using ApplicationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTop.Application.Services.DTOs
{
    class TaskDTO
    {
        public int ID { get; set; }
        public int Duration { get; set; }
        public DateTime DueDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public StatusType Status { get; set; }
        public PriorityType Priority { get; set; }

        public int ApplicationUserId { get; set; }
        public UserDTO User { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
