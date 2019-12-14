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
    public class GetCountInteractionByCampaignIdHandler : IRequestHandler<GetCountInteractionByCampaignIdRequest, List<FacebookPageStatistics>>
    {
        private readonly ContentoDbContext _context;

        public GetCountInteractionByCampaignIdHandler(ContentoDbContext context)
        {
            _context = context;
        }

        public async Task<List<FacebookPageStatistics>> Handle(GetCountInteractionByCampaignIdRequest request, CancellationToken cancellationToken)
        {
            var taskFanpages = _context.TasksFanpages.Include(i => i.IdFanpageNavigation).Include(i => i.IdTaskNavigation)
               .Where(w => w.IdFanpageNavigation.IdChannel == 2 && w.IdTaskNavigation.IdCampaign == request.campaignId).ToList();

            Dictionary<string, int> map = new Dictionary<string, int>();

            foreach (var taskFanpage in taskFanpages)
            {
                if (!map.ContainsKey(taskFanpage.IdFanpageNavigation.Name))
                {
                    map.Add(taskFanpage.IdFanpageNavigation.Name, 0);
                }

                var interaction = JObject.Parse(await Helper.GetInteraction(taskFanpage.IdFacebook, taskFanpage.IdFanpageNavigation.Token));

                int count = 0;

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

                map[taskFanpage.IdFanpageNavigation.Name] += count;
            }

            List<FacebookPageStatistics> result = new List<FacebookPageStatistics>();

            foreach (var item in map)
            {
                result.Add(new FacebookPageStatistics { name = item.Key, interaction = item.Value});
            }

            return result;
        }
    }
}
