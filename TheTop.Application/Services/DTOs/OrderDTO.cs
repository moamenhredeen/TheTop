using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTop.Application.Services.DTOs
{
    class OrderDTO
    {
        public int OrderId { get; set; }
        public float TotalPrice { get; set; }
        public int ApplicationUserId { get; set; }

    }
}
