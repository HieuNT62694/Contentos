using BatchjobService.Entities;
using BatchjobService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BatchjobService.Application.Queries.GetFanpagesByMarketerId
{
    public class GetFanpagesByMarketerIdHandler : IRequestHandler<GetFanpagesByMarketerIdRequest, List<FanpageViewModel>>
    {

        private readonly ContentoDbContext _context;

        public GetFanpagesByMarketerIdHandler(ContentoDbContext context)
        {
            _context = context;
        }
        public async Task<List<FanpageViewModel>> Handle(GetFanpagesByMarketerIdRequest request, CancellationToken cancellationToken)
        {
            var fanpages = await _context.Fanpages.Include(i => i.IdChannelNavigation).Where(w => w.IdMarketer == request.marketerId && w.IdChannel == request.channelId).ToListAsync();

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
                else
                {
                    model.customer = new Customer { id = 0, name = "" };
                }

                model.modifiedDate = fanpage.ModifiedDate;

                listFanpages.Add(model);
            }

            return listFanpages;
        }
    }
}
