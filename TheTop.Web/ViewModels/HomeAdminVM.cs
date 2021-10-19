using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheTop.ViewModels
{
    public class HomeAdminVM
    {
        public SearchVM Search { get; set; }

        public ICollection<OrderVM> Orders { get; set; }
    }
}
