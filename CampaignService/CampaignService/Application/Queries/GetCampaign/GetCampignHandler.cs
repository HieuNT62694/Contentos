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

namespace CampaignService.Application.Queries.GetCampaign
{
    public class GetCampaignHandler : IRequestHandler<GetCampaignRequest, CampaignTaskDetail>
    {
        private readonly ContentoContext _context;
        public GetCampaignHandler(ContentoContext contentodbContext)
        {
            _context = contentodbContext;
        }

        public async Task<CampaignTaskDetail> Handle(GetCampaignRequest request, CancellationToken cancellationToken)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Campaign, CampaignTaskDetail>()
            .ForMember(x => x.Status, opt => opt.Ignore()));
            var mapper = config.CreateMapper();

            var entity = await _context.Campaign.AsNoTracking()
                .Include(i => i.CampaignTags).FirstAsync(x => x.Id == request.IdCampaign);

            CampaignTaskDetail model = mapper.Map<CampaignTaskDetail>(entity);

            //Get Editor Name & Id
            model.Editor = new Models.Editor();
            model.Editor.Id = entity.IdEditor;
            model.Editor.Name = _context.Users.Find(entity.IdEditor).Name;

            //Get Customer Name & Id
            model.Customer = new Models.Customer();
            model.Customer.Id = entity.IdCustomer;
            model.Customer.Name = _context.Users.Find(entity.IdCustomer).Name;

            //Get Status Name & Id
            model.Status = new Models.Status();
            model.Status.Id = entity.Status;
            var stat = _context.StatusCampaign.Find(entity.Status);
            model.Status.Name = stat.Name;
            model.Status.Color = stat.Color;

            //Get ListTag
            List<Tag> ls = new List<Tag>();
            foreach (var tag in entity.CampaignTags)
            {
                var cTag = new Tag { Id = tag.IdTags, Name = _context.Tags.Find(tag.IdTags).Name };
                ls.Add(cTag);
            }

            model.listTag = ls;

            return model;
        }


    }
}
