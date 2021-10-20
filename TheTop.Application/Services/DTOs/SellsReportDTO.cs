using System.Collections.Generic;

namespace TheTop.Application.Services.DTOs
{
    public class SellsReportDTO
    {
        public ICollection<OrderDTO> orders { get; set; }
        public decimal? TotalPrice { get; set; }
        public long SellsCount { get; set; }
        public decimal? Profit { get; set; }
    }
}