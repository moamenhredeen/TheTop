using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTop.Application.Entities
{
  public  class BankAccount 
    {
        public int BankAccountId { get; set; }
        public string CardNum { get; set; }
        public float Balance { get; set; }
        
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime CreatedAt = DateTime.Now; 
        public DateTime? UpdatedAt { get; set; } 

    }
}
