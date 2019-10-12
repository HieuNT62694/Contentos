using CampaignService.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignService.Application.Queries.GetListCampaignByUserId
{
    public class GetListCampaignByUserIdRequest : IRequest<Campaign>
    {

        public int IdCustomer { get; set; }

    }
}
