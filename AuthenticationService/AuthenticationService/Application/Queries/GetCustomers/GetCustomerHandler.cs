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
    public class GetCustomerHandler : IRequestHandler<GetCustomerRequest, List<CreateUserModel>>
    {
        private readonly ContentoContext _context;

        public GetCustomerHandler(ContentoContext context)
        {
            _context = context;
        }

        public async Task<List<CreateUserModel>> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
        {
            var list = await _context.Users.AsNoTracking()
                .Include(x => x.Accounts)
                .Where(x => x.Accounts.Any(i => i.IdRole == 5))
                .Where(u => u.IdManager == request.MarketerId).ToListAsync();
            
            var lstCustomer = new List<CreateUserModel>();
            foreach (var item in list)
            {
                var User = new CreateUserModel()
                {
                    Id = item.Id,
                    FullName = item.Name,
                    Email = item.Accounts.First().Email,
                    CompanyName = item.Company,
                    Phone = ""
                };
                lstCustomer.Add(User);
            }

            return lstCustomer;
        }
        

    }
}
