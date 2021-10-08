using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Classs;

namespace TheTop.ViewModels
{
    public class ReviewDTO : BASEEntity
    {
       
        [Display(Name = "First Name")]
        [MaxLength(55, ErrorMessage = "Name shoald not exced 55 char!")]
        [MinLength(3, ErrorMessage = "Name should not be less than 3 char!")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalide email address")]
        [MaxLength(55, ErrorMessage = "Email shoald not exced 55 char!")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Display(Name = "Subject")]
        [MaxLength(55, ErrorMessage = "Subject shoald not exced 55 char!")]
        [MinLength(3, ErrorMessage = "Subject should not be less than 3 char!")]
        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }


        [Display(Name = "Massage")]
        [MaxLength(255, ErrorMessage = "Massage shoald not exced 255 char!")]
        [MinLength(3, ErrorMessage = "Massage should not be less than 3 char!")]
        [Required(ErrorMessage = "Massage is required")]
        public string Massage { get; set; }

        public bool IsActiv { get; set; }
        public int CustomerId { get; set; }


        

    }
}
