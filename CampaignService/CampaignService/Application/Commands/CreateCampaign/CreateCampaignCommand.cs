using CampaignService.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignService.Application.Commands.CreateCampaign
{
    public class CreateCampaignCommand : IRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? EndDate { get; set; }
        public int? IdCustomer { get; set; }
        public int? IdEditor { get; set; }
        public List<int> IdTag { get; set; }

    }
}
