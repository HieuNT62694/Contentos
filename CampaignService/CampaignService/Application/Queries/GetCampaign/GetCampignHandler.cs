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

namespace CampaignService.Application.Queries.GetCampaign
{
    public class GetCampaignHandler : IRequestHandler<GetCampaignRequest, CampaignData>
    {
        private readonly ContentoContext contentodbContext;
        public GetCampaignHandler(ContentoContext contentodbContext)
        {
            this.contentodbContext = contentodbContext;
        }

        public async Task<CampaignData> Handle(GetCampaignRequest request, CancellationToken cancellationToken)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Campaign, CampaignData>());
            var mapper = config.CreateMapper();

            var entity = contentodbContext.Campaign.Include(i => i.CampaignTags).First(x => x.Id == request.IdCampaign);

            CampaignData model = mapper.Map<CampaignData>(entity);

            //Get Editor Name
            model.editorName = contentodbContext.Users.Find(entity.IdEditor).Name;

            //Get Customer Name
            model.customerName = contentodbContext.Users.Find(entity.IdCustomer).Name;

            //Get Status Name
            model.Status = contentodbContext.Status.Find(entity.Status).Name;

            model.idStatus = entity.Status;

            //Get ListTag
            List<Tag> ls = new List<Tag>();
            foreach (var tag in entity.CampaignTags)
            {
                var cTag = new Tag { Id = tag.IdTags, Name = contentodbContext.Tags.Find(tag.IdTags).Name };
                ls.Add(cTag);
            }

            model.listTag = ls;

            return model;
        }


    }
}
