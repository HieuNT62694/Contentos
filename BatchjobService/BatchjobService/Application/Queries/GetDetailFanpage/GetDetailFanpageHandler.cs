using BatchjobService.Entities;
using BatchjobService.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BatchjobService.Application.Queries.GetDetailFanpage
{
    public class GetDetailFanpageHandler : IRequestHandler<GetDetailFanpageRequest, FanpageViewModel>
    {
        private readonly ContentoDbContext _context;

        public GetDetailFanpageHandler(ContentoDbContext context)
        {
            _context = context;
        }

        public async Task<FanpageViewModel> Handle(GetDetailFanpageRequest request, CancellationToken cancellationToken)
        {
            Fanpages fanpage = await _context.Fanpages.FindAsync(request.id);

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
            else
            {
                model.customer = new Customer { id = 0, name = "" };
            }

            model.modifiedDate = fanpage.ModifiedDate;

            return model;
        }
    }
}
