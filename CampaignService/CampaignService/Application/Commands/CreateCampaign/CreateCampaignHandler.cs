using AutoMapper;
using CampaignService.Entities;
using CampaignService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CampaignService.Application.Commands.CreateCampaign
{
    public class CreateCampaignHandler : IRequestHandler<CreateCampaignCommand, CampaignData>
    {
        private readonly ContentoContext _context;

        public CreateCampaignHandler(ContentoContext campaignDbContext)
        {
            _context = campaignDbContext;
        }

        public async Task<CampaignData> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
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

            _context.Campaign.Add(newCampaign);

            await _context.SaveChangesAsync(cancellationToken);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Campaign, CampaignData>()
            .ForMember(x => x.Status, opt => opt.Ignore()));
            var mapper = config.CreateMapper();

            CampaignData model = mapper.Map<CampaignData>(newCampaign);

            //Get Editor Name & Id
            model.Editor = new Models.Editor();
            model.Editor.Id = newCampaign.IdEditor;
            model.Editor.Name = _context.Users.Find(newCampaign.IdEditor).Name;

            //Get Customer Name & Id
            model.Customer = new Models.Customer();
            model.Customer.Id = newCampaign.IdCustomer;
            model.Customer.Name = _context.Users.Find(newCampaign.IdCustomer).Name;

            //Get Status Name & Id
            model.Status = new Models.Status();
            model.Status.Id = newCampaign.Status;
            model.Status.Name = _context.StatusCampaign.Find(newCampaign.Status).Name;

            //Get ListTag
            List<Models.Tag> ls = new List<Models.Tag>();
            foreach (var tag in newCampaign.CampaignTags)
            {
                var cTag = new Models.Tag { Id = tag.IdTags, Name = _context.Tags.Find(tag.IdTags).Name };
                ls.Add(cTag);
            }

            model.listTag = ls;

            return model;
        }
    }
}
