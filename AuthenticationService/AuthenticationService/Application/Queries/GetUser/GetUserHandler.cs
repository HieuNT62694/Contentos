﻿using AuthenticationService.Entities;
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
    public class GetUserHandler : IRequestHandler<GetUserRequest, List<ListUserModel>>
    {
        private readonly ContentoContext _context;

        public GetUserHandler(ContentoContext context)
        {

            _context = context;
        }
        public async Task<List<ListUserModel>> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var lstUser = await _context.Users.AsNoTracking()
                .Include(x => x.Accounts)
                .Where(x=>x.Accounts.Any(i=>i.IdRole == 2))
                .Where(u => u.IdManager == request.IdMarketer).ToListAsync();
            
            var lstEditor = new List<ListUserModel>();
            foreach (var item in lstUser)
            {
                var edtUser = new ListUserModel
                {
                    Id = item.Id,
                    Name = item.Name
                };
                lstEditor.Add(edtUser);
            }
            return lstEditor;
        }
    }
}
