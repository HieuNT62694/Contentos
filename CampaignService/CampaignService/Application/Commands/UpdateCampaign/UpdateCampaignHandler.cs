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

namespace CampaignService.Application.Commands.UpdateCampaign
{
    public class UpdateCampaignHandler : IRequestHandler<UpdateCampaignCommand, CampaignData>
    {

        private readonly ContentoContext _context;

        public UpdateCampaignHandler(ContentoContext campaignDbContext)
        {
            _context = campaignDbContext;
        }

        public async Task<CampaignData> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            var upCampaign = _context.Campaign.Include(x => x.CampaignTags).Single(s => s.Id == request.Id);

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

            _context.CampaignTags.RemoveRange(upCampaign.CampaignTags);

            upCampaign.CampaignTags = upTags;

            _context.Entry(upCampaign).State = EntityState.Modified;

            //_context.Campaign.Update(upCampaign);

            await _context.SaveChangesAsync(cancellationToken);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Campaign, CampaignData>()
            .ForMember(x => x.Status, opt => opt.Ignore()));
            var mapper = config.CreateMapper();

            CampaignData model = mapper.Map<CampaignData>(upCampaign);

            //Get Editor Name & Id
            model.Editor = new Models.Editor();
            model.Editor.Id = upCampaign.IdEditor;
            model.Editor.Name = _context.Users.Find(upCampaign.IdEditor).Name;

            //Get Customer Name & Id
            model.Customer = new Models.Customer();
            model.Customer.Id = upCampaign.IdCustomer;
            model.Customer.Name = _context.Users.Find(upCampaign.IdCustomer).Name;

            //Get Status Name & Id
            model.Status = new Models.Status();
            model.Status.Id = upCampaign.Status;
            model.Status.Name = _context.StatusCampaign.Find(upCampaign.Status).Name;

            //Get ListTag
            List<Models.Tag> ls = new List<Models.Tag>();
            foreach (var tag in upCampaign.CampaignTags)
            {
                var cTag = new Models.Tag { Id = tag.IdTags, Name = _context.Tags.Find(tag.IdTags).Name };
                ls.Add(cTag);
            }

            model.listTag = ls;

            return model;
        }
    }
}
