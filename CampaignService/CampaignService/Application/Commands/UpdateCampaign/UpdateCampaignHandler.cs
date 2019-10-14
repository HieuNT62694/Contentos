﻿using CampaignService.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var upCampaign = campaignDbContext.Campaign.Include(x => x.CampaignTags).Single(s => s.Id == request.Id);

            upCampaign.EndDate = request.EndDate;

            upCampaign.IdCustomer = request.Customer.Id;

            upCampaign.IdEditor = request.Editor.Id;

            upCampaign.Description = request.Description;

            upCampaign.Title = request.Title;

            var upTags = new List<CampaignTags>();

            foreach(var item in request.Tags)
            {
                var tag = new CampaignTags { IdTags = item.Id };
                upTags.Add(tag);
            }

            campaignDbContext.CampaignTags.RemoveRange(upCampaign.CampaignTags);

            upCampaign.CampaignTags = upTags;

            campaignDbContext.Campaign.Update(upCampaign);

            await campaignDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
