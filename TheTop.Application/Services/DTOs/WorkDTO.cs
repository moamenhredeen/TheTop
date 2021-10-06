using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTop.Application.Services.DTOs
{
    class WorkDTO
    {
        public int ID { get; set; }
        public DateTime StartDate { get; set; }     
        public DateTime EndDate { get; set; }
        public int ApplicationUserId { get; set; }
    }
}
