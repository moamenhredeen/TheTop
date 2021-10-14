using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTop.Application.Services.DTOs
{
    public class ReviewDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Massage { get; set; }
        public bool Approved { get; set; }
        public string UserId { get; set; }
        public UserDTO User { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
