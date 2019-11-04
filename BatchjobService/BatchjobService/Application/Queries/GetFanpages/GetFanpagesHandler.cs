﻿using BatchjobService.Entities;
using BatchjobService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BatchjobService.Application.Queries.GetFanpages
{
    public class GetFanpagesHandler : IRequestHandler<GetFanpagesRequest, List<FanpageViewModel>>
    {
        private readonly ContentoDbContext _context;

        public GetFanpagesHandler(ContentoDbContext context)
        {
            _context = context;
        }

        public async Task<List<FanpageViewModel>> Handle(GetFanpagesRequest request, CancellationToken cancellationToken)
        {
            var fanpages = await _context.Fanpages.Include(i => i.IdChannelNavigation).ToListAsync();

            List<FanpageViewModel> listFanpages = new List<FanpageViewModel>();

            foreach(var fanpage in fanpages)
            {
                FanpageViewModel model = new FanpageViewModel();

                var customer = _context.Users.Find(fanpage.IdCustomer);

                model.id = fanpage.Id;
                model.name = fanpage.Name;
                model.channel = new Channel { id = fanpage.IdChannelNavigation.Id, name = fanpage.IdChannelNavigation.Name };
                model.customer = new Customer { id = customer.Id, name = customer.FirstName + " " + customer.LastName };
                model.modifiedDate = DateTime.UtcNow;

                listFanpages.Add(model);
            }

            return listFanpages;
        }
    }
}
