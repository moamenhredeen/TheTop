﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTop.Application.Entities;

namespace TheTop.Application.Services.DTOs
{
   public class ShoppingCartDTO
    {
        public int ShoppingCartId { get; set; }
        public int AdvertisementId { get; set; }

        public string ApplicationUserId { get; set; }

        public AdvertisementDTO Advertisement { get; set; }
        public DateTime CreatedAt = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
    }
}