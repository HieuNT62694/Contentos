using BatchjobService.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchjobService.Application.Queries.GetInteractionByCampaignId
{
    public class GetInteractionByCampaignIdRequest : IRequest<List<FacebookInteractionModel>>
    {
        public int campaignId { get; set; }
    }
}
