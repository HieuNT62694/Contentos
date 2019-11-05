using BatchjobService.Entities;
using BatchjobService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BatchjobService.Application.Queries.GetFanpagesByCustomerId
{
    public class GetFanpagesByCustomerIdHandler : IRequestHandler<GetFanpagesByCustomerIdRequest, List<FanpageViewModel>>
    {

        private readonly ContentoDbContext _context;

        public GetFanpagesByCustomerIdHandler(ContentoDbContext context)
        {
            _context = context;
        }
        public async Task<List<FanpageViewModel>> Handle(GetFanpagesByCustomerIdRequest request, CancellationToken cancellationToken)
        {
            var fanpages = await _context.Fanpages.Include(i => i.IdChannelNavigation).Where(w => w.IdCustomer == request.customerId && w.IdChannel == request.channelId).ToListAsync();

            List<FanpageViewModel> listFanpages = new List<FanpageViewModel>();

            if(fanpages == null)
            {
                return listFanpages;
            }

            foreach (var fanpage in fanpages)
            {
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

                listFanpages.Add(model);
            }

            return listFanpages;
        }
    }
}
