using Shared.DTO.IdentityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.OrderModule
{
    public class OrderDto
    {
        public string BasketId { get; set; } = default!;

        public int DeliveryMethodId { get; set; } = default!;

        public AddressDto Address { get; set; } = default!;
    }
}
