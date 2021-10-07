using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheTop.Models
{
    public class SearchVM
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
        public decimal FromPrice { get; set; }

        [Display(Name = "To Price")]
        public decimal ToPrice { get; set; }

        public IEnumerable<CategoryVM> Categorys { get; set; }
        public IEnumerable<AdvertisementVM> Advertisements { get; set; }

        public SearchVM()
        {
            Categorys = new HashSet<CategoryVM>();
            Advertisements = new HashSet<AdvertisementVM>();
        }
    }
}
