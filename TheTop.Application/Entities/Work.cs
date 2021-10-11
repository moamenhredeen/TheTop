using System;
using System.ComponentModel.DataAnnotations;

namespace TheTop.Application.Entities
{
    public class Work
    {
        public int WorkId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        //CreatedAt ?
    }
}
