using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationModel1.Services.DTOs
{
    class ReviewsDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Massage { get; set; }
        public bool IsActiv { get; set; }
        public int UserId { get; set; }
        public UserDTO User { get; set; }
    }
}
