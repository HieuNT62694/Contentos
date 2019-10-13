using AuthenticationService.Entities;
using AuthenticationService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace AuthenticationService.Application.Queries.GetUser
{
    public class GetUserHandler : IRequestHandler<GetUserRequest, List<ListEditorModel>>
    {
        private readonly ContentoContext _context;

        public GetUserHandler(ContentoContext context)
        {

            _context = context;
        }
        public async Task<List<ListEditorModel>> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var lstUser = _context.Users.Include(x => x.Accounts).Where(x=>x.Accounts.Any(i=>i.IdRole == 2)).ToList();
            var lstEditor = new List<ListEditorModel>();
            foreach (var item in lstUser)
            {
                var edtUser = new ListEditorModel
                {
                    Id = item.Accounts.Select(x => x.Id).FirstOrDefault(),
                    Name = item.Name
                };
                lstEditor.Add(edtUser);
            }
            return lstEditor;
        }
    }
}
