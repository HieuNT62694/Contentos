using CampaignService.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CampaignService.Application.Commands.UpdateCampaign
{
    public class UpdateCampaignHandler : IRequestHandler<UpdateCampaignCommand>
    {

        private readonly ContentoContext campaignDbContext;

        public UpdateCampaignHandler(ContentoContext campaignDbContext)
        {
            this.campaignDbContext = campaignDbContext;
        }

        public async Task<Unit> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            var upCampaign = campaignDbContext.Campaign.Find(request.Id);


            upCampaign.EndDate = request.EndDate;
            upCampaign.IdCustomer = request.IdCustomer;
            upCampaign.Description = request.Description;
            upCampaign.Title = request.Title;
            upCampaign.StartedDate = DateTime.UtcNow;
            campaignDbContext.Campaign.Update(upCampaign);
            await campaignDbContext.SaveChangesAsync(cancellationToken);
            var upTags = campaignDbContext.CampaignTags.Where(x => x.IdCampaign == request.Id).ToList();
    
            await campaignDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
