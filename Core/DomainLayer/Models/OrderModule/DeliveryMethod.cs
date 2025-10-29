﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModule
{
    public class DeliveryMethod : BaseEntity<int>
    {
        public string ShortName { get; set; } = default!;

        public string Descrption { get; set; } = default!;

        public string DeliveryTime { get; set; } = default!;

        public decimal Price { get; set; } 
    }
}
