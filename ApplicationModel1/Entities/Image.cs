using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Classs;

namespace TheTop.Models
{
    public class Image : Entity
    {
        
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        public int AdvertisementId { get; set; }

        //Niv

        public Advertisement Advertisement { get; set; }

    }
}
