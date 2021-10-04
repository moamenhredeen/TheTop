using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationModel1.Entities
{
    public class Offer :Entity
    {
        public float Ratio { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

       

        //Niv
        public IEnumerable<Advertisement> Advertisements { get; set; }
    }
}
