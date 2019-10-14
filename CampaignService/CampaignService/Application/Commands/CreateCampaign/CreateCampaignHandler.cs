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
            var Tags = new List<CampaignTags>();

            foreach (var item in request.Tags)
            {
                var tag = new CampaignTags { IdTags = item.Id , CreatedDate = DateTime.UtcNow};
                Tags.Add(tag);
            }

            var newCampaign = new Campaign
            {

                EndDate = request.EndDate,
                IdCustomer = request.Customer.Id,
                IdEditor = request.Editor.Id,
                Description = request.Description,
                Title = request.Title,
                StartedDate = DateTime.UtcNow,
                Status = 1,
                CampaignTags = Tags
            };

            campaignDbContext.Campaign.Add(newCampaign);
            await campaignDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
