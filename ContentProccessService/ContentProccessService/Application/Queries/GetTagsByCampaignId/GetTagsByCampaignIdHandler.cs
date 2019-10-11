using ContentProccessService.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetTagsByCampaignId
{
    public class GetTagsByCampaignIdHandler : IRequestHandler<GetTagsByCampaignIdRequest, List<string>>
    {
        private readonly ContentoContext contentodbContext;
        public GetTagsByCampaignIdHandler(ContentoContext contentodbContext)
        {
            this.contentodbContext = contentodbContext;
        }
        public async Task<List<string>> Handle(GetTagsByCampaignIdRequest request, CancellationToken cancellationToken)
        {
            var tmp = await contentodbContext.CampaignTags.Include(t => t.IdTagsNavigation).Where(w => w.IdCampaign == request.campaignId).ToListAsync();
            List<string> ls = new List<string>();
            foreach(var item in tmp)
            {
                ls.Add(item.IdTagsNavigation.Name);
            }
            return ls;
        }
    }
}
