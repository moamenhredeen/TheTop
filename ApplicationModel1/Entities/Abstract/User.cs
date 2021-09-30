using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheTop.Classs
{
    public class User
    {
        [Key()]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "nvarchar(55)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(55)")]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        [Column(TypeName = "nvarchar(55)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Password { get; set; }

        [Column(TypeName = "nvarchar(55)")]
        public string Username { get; set; }

        [Column(TypeName = "nvarchar(15)")]
        public string Phone { get; set; }

    }
}


