using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheTop.Models
{
    public class HomeDTO
    {
        [Display(Name = "Logo")]
        public string Logo { get; set; }

        [Display(Name = "Text On Image")]
        public string TextOnImage { get; set; }

        [Display(Name = "Title About")]
        public string TitleAbout { get; set; }

        [Display(Name = "Header About")]
        public string HeaderAbout { get; set; }

        [Display(Name = "Main About")]
        public string MainAbout { get; set; }

      
        [Display(Name = "CONTACT US")]
        public string TitleContactUs { get; set; }

        [Display(Name = "Text Contact Us")]
        public string TextContactUs { get; set; }

        public string ADDRESS { get; set; }

        [Display(Name = "PHONE NUMBER")]
        public string PhoneCombny { get; set; }

        [Display(Name = "EMAIL")]
        [EmailAddress(ErrorMessage = "Invalide email address")]
        public string Email { get; set; }

        [Display(Name = "Copyright ")]
        public string CopyRight { get; set; }

        [Display(Name = "Designed by")]
        public string DesignerBy { get; set; }

        public ReviewDTO Review { get; set; }

        public List<AdvertisementVM> AdvertisementsList { get; set; }

        public List<ReviewDTO> ReviewsList { get; set; }

        public List<string> CategorysList { get; set; }
    }
}
