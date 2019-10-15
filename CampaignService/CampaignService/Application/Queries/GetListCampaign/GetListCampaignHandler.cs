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
        private readonly ContentoContext contentodbContext;
    
        public GetListCampaignHandler(ContentoContext contentodbContext)
        {
            this.contentodbContext = contentodbContext;
        }

        public async Task<IEnumerable<CampaignData>> Handle(GetListCampaignRequest request,CancellationToken cancellationToken)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Campaign, CampaignData>().ForMember(x => x.Status , opt => opt.Ignore()));
            var mapper = config.CreateMapper();

            var returnResult = new List<CampaignData>();
 
            var listCampaign = contentodbContext.Campaign.AsNoTracking()
                .Include(i => i.CampaignTags).ToList<Campaign>();

            foreach (var item in listCampaign)
            {
                CampaignData model = mapper.Map<CampaignData>(item);

                var listTag = new List<string>();

                //Get Editor Name & Id
                model.Editor = new Models.Editor();
                model.Editor.Id = item.IdEditor;
                model.Editor.Name = contentodbContext.Users.Find(item.IdEditor).Name;

                //Get Customer Name & Id
                model.Customer = new Models.Customer();
                model.Customer.Id = item.IdCustomer;
                model.Customer.Name = contentodbContext.Users.Find(item.IdCustomer).Name;

                //Get Status Name & Id
                model.Status = new Models.Status();
                model.Status.Id = item.Status;
                model.Status.Name = contentodbContext.StatusCampaign.Find(item.Status).Name;

                //Get ListTag
                List<Tag> ls = new List<Tag>();
                foreach (var tag in item.CampaignTags)
                {
                    var cTag = new Tag { Id = tag.IdTags, Name = contentodbContext.Tags.Find(tag.IdTags).Name };
                    ls.Add(cTag);
                }

                model.listTag = ls;

                returnResult.Add(model);
            }

            return  returnResult;
        }
    }
}
        

