using BatchjobService.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchjobService.Application.Queries.GetCountInteractionByCampaignId
{
    public class GetCountInteractionByCampaignIdRequest : IRequest<List<FacebookPageStatistics>>
    {
        public int campaignId { get; set; }
    }
}
