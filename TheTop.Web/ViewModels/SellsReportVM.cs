using System.Collections.Generic;
using TheTop.ViewModels;

namespace TheTop.ViewModels
{
    public class SellsReportVM
    {
        public bool asPdf { get; set; }
        public ICollection<OrderVM> Orders { get; set; }
        public decimal? TotalPrice { get; set; }
        public long SellsCount { get; set; }
        public decimal? Profit { get; set; }
    }
}