﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class DeliveryMethodNotFoundException(int Id) : NotFoundException($"No Delivery Method Found With Id = {Id}")
    {
    }
}
