﻿using AuthenticationService.Entities;
using AuthenticationService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationService.Application.Queries.GetAllWriterByIdMarketer
{
    public class GetAllWriterByIdMarketerHandler : IRequestHandler<GetAllWriterByIdMarketerRequest,List<ListUserModel>>
    {
        private readonly ContentoContext _context;

        public GetAllWriterByIdMarketerHandler(ContentoContext context)
        {
            _context = context;
        }

        public async Task<List<ListUserModel>> Handle(GetAllWriterByIdMarketerRequest request, CancellationToken cancellationToken)
        {
            var list = await _context.Users.AsNoTracking()
                .Where(x => x.Accounts.Any(i => i.IdRole == 2))
                .Where(u => u.IdManager == request.MarketerId).Select(x=>x.Id)
                .ToListAsync();
          
            var lstWriter = new List<ListUserModel>();
            foreach (var item1 in list)
            {
                var listid = await _context.Users.AsNoTracking()
               .Include(x => x.Accounts)
               .Where(x => x.Accounts.Any(i => i.IdRole == 3))
               .Where(u => u.IdManager == item1).ToListAsync();
                foreach (var item in listid)
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
            return lstWriter;
        }
    }
}
