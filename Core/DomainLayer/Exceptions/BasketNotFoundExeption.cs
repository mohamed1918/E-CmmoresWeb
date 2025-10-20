using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class BasketNotFoundExeption(string id) : NotFoundException($"Basket With id ={id} IS Not Found")
    {
    }
}
