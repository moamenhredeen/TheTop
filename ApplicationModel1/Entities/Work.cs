using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationModel1.Entities
{
    public class Work
    {
        public int WorkId { get; set; }

        [DataType(DataType.Time)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Time)]
        public DateTime EndDate { get; set; }
        
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
