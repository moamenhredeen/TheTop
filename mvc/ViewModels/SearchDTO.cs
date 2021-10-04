using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheTop.Models
{
    public class SearchDTO
    {
        public string Name { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "From Date")]
        public DateTime FromDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "To Date")]
        public DateTime ToDate { get; set; }

        [Display(Name = "From Price")]
        public double FromPrice { get; set; }

        [Display(Name = "To Price")]
        public double ToPrice { get; set; }

        public IEnumerable<CategoryDTO> Categorys { get; set; }
        public IEnumerable<AdvertisementDTO> Advertisements { get; set; }

        public SearchDTO()
        {
            Categorys = new HashSet<CategoryDTO>();
            Advertisements = new HashSet<AdvertisementDTO>();
        }
    }
}
