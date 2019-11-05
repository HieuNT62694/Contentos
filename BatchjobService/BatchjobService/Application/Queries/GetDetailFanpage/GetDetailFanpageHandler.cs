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
    public class GetDetailFanpageHandler : IRequestHandler<GetDetailFanpageRequest, EditViewModel>
    {
        private readonly ContentoDbContext _context;

        public GetDetailFanpageHandler(ContentoDbContext context)
        {
            _context = context;
        }

        public async Task<EditViewModel> Handle(GetDetailFanpageRequest request, CancellationToken cancellationToken)
        {
            Fanpages fanpage = await _context.Fanpages.FindAsync(request.id);

            _context.Entry(fanpage).Reference(p => p.IdChannelNavigation).Load();

            EditViewModel model = new EditViewModel();

            model.id = fanpage.Id;
            model.name = fanpage.Name;
            model.channel = fanpage.IdChannelNavigation.Id;
            if (fanpage.IdCustomer != null)
            {
                var customer = _context.Users.Find(fanpage.IdCustomer);
                model.customer = customer.Id;
            }

            model.modifiedDate = fanpage.ModifiedDate;
            model.token = fanpage.Token;

            return model;
        }
    }
}
