using AuthenticationService.Entities;
using MediatR;
using System;
using AuthenticationService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Application.Queries.GetWriter
{
    public class GetCustomerHandler : IRequestHandler<GetWriterRequest, List<ListUserModel>>
    {
        private readonly ContentoDbContext _context;

        public GetCustomerHandler(ContentoDbContext context)
        {
            _context = context;
        }

        public async Task<List<ListUserModel>> Handle(GetWriterRequest request, CancellationToken cancellationToken)
        {
            var list = await _context.Users.AsNoTracking()
                .Include(x => x.Accounts)
                .Where(x => x.Accounts.Any(i => i.IdRole == 3))
                .Where(u => u.IdManager == request.EditorId).ToListAsync();

            var lstWriter = new List<ListUserModel>();
            foreach (var item in list)
            {
                var User = new ListUserModel()
                {
                    Id = item.Id,
                    Name = item.FirstName + " " + item.LastName
                };
                lstWriter.Add(User);
            }
            return lstWriter;
        }

    }
}
