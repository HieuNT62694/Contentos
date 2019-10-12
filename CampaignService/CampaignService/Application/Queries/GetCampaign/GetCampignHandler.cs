using CampaignService.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CampaignService.Application.Queries.GetCampaign
{
    public class GetCampaignHandler : IRequestHandler<GetCampaignRequest, Campaign>
    {
        private readonly ContentoContext contentodbContext;
        public GetCampaignHandler(ContentoContext contentodbContext)
        {
            this.contentodbContext = contentodbContext;
        }

        public async Task<Campaign> Handle(GetCampaignRequest request, CancellationToken cancellationToken)
        {
            //Task<Campaign> returnResult = await contentodbContext.Campaign.find(request.IdCampaign);
            //return await returnResult;
            return await contentodbContext.Campaign.Include(i => i.Tasks).FirstOrDefaultAsync(x => x.Id == request.IdCampaign);
            //return await contentodbContext.Campaign.FindAsync(request.IdCampaign);
        }


    }
}
