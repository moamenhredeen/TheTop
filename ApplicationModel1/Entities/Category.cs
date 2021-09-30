using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Classs;

namespace TheTop.Models
{
    public class Category :Entity
    {
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        //Niv
       public ICollection<Advertisement> Advertisements { get; set; }


       public Category()
        {
            Advertisements = new HashSet<Advertisement>();
        }
    }
}
