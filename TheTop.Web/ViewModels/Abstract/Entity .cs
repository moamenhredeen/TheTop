using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheTop.Classs
{
    public class BASEEntity
    {
        [Key()]
        public int ID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Create At")]
        public DateTime CreatedAT { get; set; } = DateTime.Now;


        [DataType(DataType.Date)]
        [Display(Name = "Update At")]
        public DateTime UpdatedAt { get; set; }
    }
}
