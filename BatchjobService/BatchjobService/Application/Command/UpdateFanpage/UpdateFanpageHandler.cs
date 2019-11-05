﻿using BatchjobService.Entities;
using BatchjobService.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BatchjobService.Application.Command.UpdateFanpage
{
    public class UpdateFanpageHandler : IRequestHandler<UpdateFanpageCommand, FanpageViewModel>
    {
        private readonly ContentoDbContext _context;

        public UpdateFanpageHandler(ContentoDbContext context)
        {
            _context = context;
        }
        public async Task<FanpageViewModel> Handle(UpdateFanpageCommand request, CancellationToken cancellationToken)
        {
            Fanpages fanpage = _context.Fanpages.Find(request.fanpageId);

            fanpage.IdChannel = request.channelId;
            if (request.customerId > 0)
            {
                fanpage.IdCustomer = request.customerId;
            }
            fanpage.IsActive = true;
            fanpage.Token = request.token;
            fanpage.ModifiedDate = DateTime.Now;
            fanpage.Name = request.name;

            _context.Update(fanpage);

            await _context.SaveChangesAsync();

            _context.Entry(fanpage).Reference(p => p.IdChannelNavigation).Load();

            FanpageViewModel model = new FanpageViewModel();

            model.id = fanpage.Id;
            model.name = fanpage.Name;
            model.channel = new Channel { id = fanpage.IdChannelNavigation.Id, name = fanpage.IdChannelNavigation.Name };
            if (fanpage.IdCustomer != null)
            {
                var customer = _context.Users.Find(fanpage.IdCustomer);
                model.customer = new Customer { id = customer.Id, name = customer.FirstName + " " + customer.LastName };
            }

            model.modifiedDate = fanpage.ModifiedDate;

            return model;
        }
    }
}
