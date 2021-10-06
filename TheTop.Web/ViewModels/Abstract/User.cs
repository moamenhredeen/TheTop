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
        public int ID { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(55, ErrorMessage = "First Name shoald not exced 55 char!")]
        [MinLength(3, ErrorMessage = "First Name should not be less than 3 char!")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(55, ErrorMessage = "Last Name shoald not exced 55 char!")]
        [MinLength(3, ErrorMessage = "Last Name should not be less than 3 char!")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [EmailAddress(ErrorMessage = "Invalide email address")]
        [MaxLength(55, ErrorMessage = "Email shoald not exced 55 char!")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        [MaxLength(255, ErrorMessage = "Password shoald not exced 255 char!")]
        [MinLength(8, ErrorMessage = "Password should not be less than 8 char!")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password must be match!")]
        [Required(ErrorMessage = "Password is required")]
        public string ConfirmPassword { get; set; }

        [StringLength(55, MinimumLength = 3, ErrorMessage = "Username should have 3 up to 55 char!")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        [Display(Name = "Phone", Prompt = "+00 000 000 000")]
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

    }
}


