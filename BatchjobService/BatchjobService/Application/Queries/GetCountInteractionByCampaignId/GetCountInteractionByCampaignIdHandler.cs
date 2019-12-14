using BatchjobService.Entities;
using BatchjobService.Models;
using BatchjobService.Utulity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BatchjobService.Application.Queries.GetCountInteractionByCampaignId
{
    public class GetCountInteractionByCampaignIdHandler : IRequestHandler<GetCountInteractionByCampaignIdRequest, FacebookPageStatistics>
    {
        private readonly ContentoDbContext _context;

        public GetCountInteractionByCampaignIdHandler(ContentoDbContext context)
        {
            _context = context;
        }

        public async Task<FacebookPageStatistics> Handle(GetCountInteractionByCampaignIdRequest request, CancellationToken cancellationToken)
        {
            var taskFanpages = _context.TasksFanpages.Include(i => i.IdFanpageNavigation).Include(i => i.IdTaskNavigation)
               .Where(w => w.IdFanpageNavigation.IdChannel == 2 && w.IdTaskNavigation.IdCampaign == request.campaignId).ToList();

            var campaign = _context.Campaigns.Find(request.campaignId);

            int count = 0;

            foreach (var taskFanpage in taskFanpages)
            {
                var interaction = JObject.Parse(await Helper.GetInteraction(taskFanpage.IdFacebook, taskFanpage.IdFanpageNavigation.Token));
                count += interaction["reactions"]["summary"]["total_count"].Value<int>();
                count += interaction["comments"]["summary"]["total_count"].Value<int>();

                if (!interaction.ContainsKey("shares"))
                {
                    count += 0;
                }
                else
                {
                    count += interaction["shares"]["count"].Value<int>();
                }
            }

            FacebookPageStatistics result = new FacebookPageStatistics { name = campaign.Title, interaction = count};

            return result;
        }
    }
}
