using AuthenticationService.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Application.Queries.GetlistCustomer
{
    public class GetListCustomerRequest : IRequest
    {
        public string Id { get; set; }
    }
}
