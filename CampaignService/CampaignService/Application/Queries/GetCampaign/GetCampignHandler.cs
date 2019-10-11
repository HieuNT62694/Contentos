using CampaignService.Entities;
using MediatR;
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
            return await contentodbContext.Campaign.FindAsync(request.IdCampaign);
        }

    }
}
