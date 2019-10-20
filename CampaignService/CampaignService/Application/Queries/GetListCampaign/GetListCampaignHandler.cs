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

namespace CampaignService.Application.Queries.GetListCampaign
{
    public class GetListCampaignHandler : IRequestHandler<GetListCampaignRequest,IEnumerable<CampaignData>>
    {
        private readonly ContentoContext _context;
    
        public GetListCampaignHandler(ContentoContext contentodbContext)
        {
            _context = contentodbContext;
        }

        public async Task<IEnumerable<CampaignData>> Handle(GetListCampaignRequest request,CancellationToken cancellationToken)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Campaign, CampaignData>().ForMember(x => x.Status , opt => opt.Ignore()));
            var mapper = config.CreateMapper();

            var returnResult = new List<CampaignData>();
 
            var listCampaign = await _context.Campaign.AsNoTracking()
                .Include(i => i.CampaignTags).ToListAsync<Campaign>();

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
                model.Customer.Name = _context.Users.Find(item.IdCustomer).Name;

                //Get Status Name & Id
                model.Status = new Models.Status();
                model.Status.Id = item.Status;
                var stat = _context.StatusCampaign.Find(item.Status);
                model.Status.Name = stat.Name;
                model.Status.Color = stat.Color;

                //Get ListTag
                List<Tag> ls = new List<Tag>();
                foreach (var tag in item.CampaignTags)
                {
                    var cTag = new Tag { Id = tag.IdTags, Name = _context.Tags.Find(tag.IdTags).Name };
                    ls.Add(cTag);
                }

                model.listTag = ls;



                returnResult.Add(model);
            }

            return  returnResult;
        }
    }
}
        

