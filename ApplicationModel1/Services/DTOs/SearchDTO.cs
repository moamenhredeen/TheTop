using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationModel1.Services.DTOs
{
    class SearchDTO
    {
        public string Name { get; set; }     
        public int CategoryId { get; set; }       
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }      
        public decimal FromPrice { get; set; }
        public decimal ToPrice { get; set; }
    }
}
