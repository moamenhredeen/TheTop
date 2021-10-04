using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationModel1.Entities
{
    [Table("order_advertisement")]
    public class OrderAdv : Entity
    {

        public int OrderId { get; set; }
        public int AdvertisementId { get; set; }

        //Niv 
        public Order Order { get; set; }
        public Advertisement Advertisement { get; set; }
    }
}
