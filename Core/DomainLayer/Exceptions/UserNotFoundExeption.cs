using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class UserNotFoundExeption(string email) : NotFoundException($"User with email {email} was not found.")
    {

    }
}
