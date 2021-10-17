using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Classs;

namespace TheTop.ViewModels
{
    public class CouponVM : BASEEntity
    {

        public string Code { get; set; }

        [Required(ErrorMessage = "Ratio is Required!")]
        public float Ratio { get; set; }

        public DateTime ValidityDate { get; set; }

    }
}
