﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationModel1.Services.DTOs
{
    class CategoryDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public DateTime CreateAt { get; set; } 
        public DateTime UpdateAt { get; set; }
    }
}
