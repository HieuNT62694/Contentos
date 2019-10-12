using CampaignService.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CampaignService.Application.Commands.CreateCampaign
{
    public class CreateCampaignHandler : IRequestHandler<CreateCampaignCommand>
    {
        private readonly ContentoContext campaignDbContext;

        public CreateCampaignHandler(ContentoContext campaignDbContext)
        {
            this.campaignDbContext = campaignDbContext;
        }

        public async Task<Unit> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            var newCampaign = new Campaign
            {

                EndDate = request.EndDate,
                IdCustomer = request.IdCustomer,
                IdEditor = request.IdEditor,
                Description = request.Description,
                Title = request.Title,
                StartedDate = DateTime.UtcNow,
                Status = 1
            };
            campaignDbContext.Campaign.Add(newCampaign);
            await campaignDbContext.SaveChangesAsync(cancellationToken);
            foreach (var item in request.IdTag)
            {
                var newCampaignTags = new CampaignTags
                {
                    IdCampaign = campaignDbContext.Campaign.LastOrDefault().Id,
                    IdTags = item,
                    Created = DateTime.UtcNow
                };
                campaignDbContext.CampaignTags.Add(newCampaignTags);

            }
            await campaignDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
