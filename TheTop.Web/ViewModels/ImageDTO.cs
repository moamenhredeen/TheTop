using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Classs;

namespace TheTop.ViewModels
{
    public class ImageDTO : BASEEntity
    {
        public string Name { get; set; }

        public int AdvertisementId { get; set; }

     

    }
}
