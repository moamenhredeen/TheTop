using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApplicationModel1.Services.DTOs
{
    class AdvertisementsDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public ICollection<string> ImagesNames { get; set; }
    }
}
