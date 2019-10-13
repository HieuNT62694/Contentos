using AuthenticationService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Common
{
    public interface IHelperFunction
    {
        object GenerateJwtToken(string email, Accounts user, string Role);
    }
}
