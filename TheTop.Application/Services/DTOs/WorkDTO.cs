using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTop.Application.Services.DTOs
{
    public class WorkDTO
    {
        public int ID { get; set; }
        public DateTime StartDate { get; set; }     
        public DateTime EndDate { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
