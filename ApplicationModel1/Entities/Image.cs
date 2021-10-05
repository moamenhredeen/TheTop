using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationModel1.Entities
{
    public class Image 
    {
        public int ImageId { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }
        
        public int AdvertisementId { get; set; }
    }
}
