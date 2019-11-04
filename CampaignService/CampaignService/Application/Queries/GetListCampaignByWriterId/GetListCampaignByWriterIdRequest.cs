using CampaignService.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignService.Application.Queries.GetListCampaignByWriterId
{
    public class GetListCampaignByWriterIdRequest : IRequest<List<CampaignModels>>
    {
        public int IdWriter { get; set; }
    }
}