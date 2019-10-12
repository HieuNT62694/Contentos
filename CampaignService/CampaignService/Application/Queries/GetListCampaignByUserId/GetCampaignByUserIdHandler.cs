using CampaignService.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CampaignService.Application.Queries.GetListCampaignByUserId
{
    public class GetCampaignByUserIdHandler : IRequestHandler<GetListCampaignByUserIdRequest, Campaign>
    {
        private readonly ContentoContext contentodbContext;
        public GetCampaignByUserIdHandler(ContentoContext contentodbContext)
        {
            this.contentodbContext = contentodbContext;
        }

        public async Task<Campaign> Handle(GetListCampaignByUserIdRequest request, CancellationToken cancellationToken)
        {
            //Task<Campaign> returnResult = await contentodbContext.Campaign.find(request.IdCampaign);
            //return await returnResult;
            //return await contentodbContext.Campaign.Include(i => i.Tasks).FirstOrDefaultAsync(x => x.Id == request.IdCustomer);
            return await contentodbContext.Campaign.FindAsync(request.IdCustomer);
        }
    }
}
