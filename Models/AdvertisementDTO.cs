using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Classs;

namespace TheTop.Models
{
    public class AdvertisementDTO : BASEEntity
    {
        [Display(Name = "Name")]
        [MaxLength(255, ErrorMessage = "Name shoald not exced 255 char!")]
        [MinLength(5, ErrorMessage = "Name should not be less than 5 char!")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        //public float Rate { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public List<string> Categorys { get; set; }
        public int CustomerId { get; set; }

        [Display(Name = "Photos")]
        public ICollection<IFormFile> PhotosNames { get; set; }

     
    }
}
