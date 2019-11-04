using AuthenticationService.Entities;
using AuthenticationService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationService.Application.Queries.GetCustomerByIdEditor
{
    public class GetCustomerByIdEditorHandler : IRequestHandler<GetCustomerByIdEditorRequest, List<ListUserModel>>
    {
        private readonly ContentoDbContext _context;

        public GetCustomerByIdEditorHandler(ContentoDbContext context)
        {
            _context = context;
        }

        public async Task<List<ListUserModel>> Handle(GetCustomerByIdEditorRequest request, CancellationToken cancellationToken)
        {
            var idMarketer = _context.Users.Find(request.EditorId).IdManager;
            var list = await _context.Users.AsNoTracking()
                .Include(x => x.Accounts)
                .Where(x => x.Accounts.Any(i => i.IdRole == 5))
                .Where(u => u.IdManager == idMarketer).ToListAsync();

            var lstCustomer = new List<ListUserModel>();
            foreach (var item in list)
            {
                var User = new ListUserModel()
                {
                    Id = item.Id,
                    Name = item.FirstName + " " + item.LastName
                };
                lstCustomer.Add(User);
            }

            return lstCustomer;
        }
    }
}
