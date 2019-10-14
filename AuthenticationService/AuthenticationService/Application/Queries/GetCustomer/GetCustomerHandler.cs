using AuthenticationService.Entities;
using MediatR;
using System;
using AuthenticationService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthenticationService.Application.Queries.GetWriter;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Application.Queries.GetCustomer
{
    public class GetCustomerHandler : IRequestHandler<GetCustomerRequest, List<ListUserModel>>
    {
        private readonly ContentoContext _context;

        public GetCustomerHandler(ContentoContext context)
        {
            _context = context;
        }

        public async Task<List<ListUserModel>> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
        {
            var list = _context.Users.Include(x => x.Accounts).Where(x => x.Accounts.Any(i => i.IdRole == 5)).Where(u => u.IdManager == request.MarketerId).ToList();
            var lstWriter = new List<ListUserModel>();
            foreach (var item in list)
            {
                var User = new ListUserModel()
                {
                    Id = item.Id,
                    Name = item.Name
                };
                lstWriter.Add(User);
            }
            return lstWriter;
        }
        

    }
}
