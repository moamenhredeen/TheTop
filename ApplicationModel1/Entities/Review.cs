using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Classs;

namespace TheTop.Models
{
    public class Review : Entity
    {
        [Column(TypeName = "nvarchar(255)")]
        public string Content { get; set; }

        public int AdvertisementId { get; set; }

        public int CustomerId { get; set; }


        //Niv
        public Advertisement Advertisement { get; set; }

        public Customer Customer { get; set; }

    }
}
