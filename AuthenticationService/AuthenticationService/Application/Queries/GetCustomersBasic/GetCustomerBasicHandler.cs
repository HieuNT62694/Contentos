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
    public class GetCustomerBasicHandler : IRequestHandler<GetCustomerBasicRequest, List<ListUserModel>>
    {
        private readonly ContentoContext _context;

        public GetCustomerBasicHandler(ContentoContext context)
        {
            _context = context;
        }

        public async Task<List<ListUserModel>> Handle(GetCustomerBasicRequest request, CancellationToken cancellationToken)
        {
            var list = await _context.Users.AsNoTracking()
                .Include(x => x.Accounts)
                .Where(x => x.Accounts.Any(i => i.IdRole == 5))
                .Where(u => u.IdManager == request.MarketerId).ToListAsync();
            
            var lstCustomer = new List<ListUserModel>();
            foreach (var item in list)
            {
                var User = new ListUserModel()
                {
                    Id = item.Id,
                    Name = item.Name
                };
                lstCustomer.Add(User);
            }

            return lstCustomer;
        }
        

    }
}
