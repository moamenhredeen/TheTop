using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTop.Application.Services.DTOs
{
    class ReviewsDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Massage { get; set; }
        public bool Approved { get; set; }
        public int UserId { get; set; }
<<<<<<< HEAD:ApplicationModel1/Services/DTOs/ReviewsDTO.cs
        public UserDTO User { get; set; }

        public DateTime CreatedAt { get; set; }
=======
>>>>>>> 8af398e5de6034043dbd40b97550e623d8766657:TheTop.Application/Services/DTOs/ReviewsDTO.cs
    }
}
