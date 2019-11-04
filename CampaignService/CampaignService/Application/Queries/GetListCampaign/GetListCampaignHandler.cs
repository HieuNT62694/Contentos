﻿using AutoMapper;
using CampaignService.Entities;
using CampaignService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CampaignService.Application.Queries.GetListCampaign
{
    public class GetListCampaignHandler : IRequestHandler<GetListCampaignRequest,IEnumerable<CampaignData>>
    {
        private readonly ContentoDbContext _context;
    
        public GetListCampaignHandler(ContentoDbContext contentodbContext)
        {
            _context = contentodbContext;
        }

        public async Task<IEnumerable<CampaignData>> Handle(GetListCampaignRequest request,CancellationToken cancellationToken)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Campaigns, CampaignData>().ForMember(x => x.Status , opt => opt.Ignore()));
            var mapper = config.CreateMapper();

            var returnResult = new List<CampaignData>();
 
            var listCampaign = await _context.Campaigns.AsNoTracking()
                .Include(i => i.TagsCampaigns).ToListAsync<Campaigns>();

            foreach (var item in listCampaign)
            {
                CampaignData model = mapper.Map<CampaignData>(item);

                var listTag = new List<string>();

                ////Get Editor Name & Id
                //model.Editor = new Models.Editor();
                //model.Editor.Id = item.IdEditor;
                //model.Editor.Name = _context.Users.Find(item.IdEditor).Name;

                //Get Customer Name & Id
                model.Customer = new Models.Customer();
                model.Customer.Id = item.IdCustomer;
                var cus = _context.Users.Find(item.IdCustomer);
                model.Customer.Name = cus.FirstName + " " + cus.LastName;

                //Get Status Name & Id
                model.Status = new Models.Status();
                model.Status.Id = item.Status;
                var stat = _context.StatusCampaigns.Find(item.Status);
                model.Status.Name = stat.Name;
                model.Status.Color = stat.Color;

                //Get ListTag
                List<Tag> ls = new List<Tag>();
                foreach (var tag in item.TagsCampaigns)
                {
                    var cTag = new Tag { Id = tag.IdTag, Name = _context.Tags.Find(tag.IdTag).Name };
                    ls.Add(cTag);
                }

                model.listTag = ls;



                returnResult.Add(model);
            }

            return  returnResult;
        }
    }
}
        

