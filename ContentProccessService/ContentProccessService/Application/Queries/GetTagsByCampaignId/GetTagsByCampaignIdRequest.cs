using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetTagsByCampaignId
{
    public class GetTagsByCampaignIdRequest : IRequest<List<string>>
    {
        public int campaignId { get; set; }
    }
}
