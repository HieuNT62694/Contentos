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

            var returnResult = new List<CampaignData>();
 
            var listCampaign = contentodbContext.Campaign.Include(i => i.CampaignTags).ToList<Campaign>();
            foreach (var item in listCampaign)
            {
                var result = new CampaignData();
                var listTag = new List<string>();
                result.Id = item.Id;
                result.IdCustomer = item.IdCustomer;
                result.Title = item.Title;
                result.idStatus = item.Status;
                result.IdEditor = item.IdEditor;
                result.editorName = contentodbContext.Users.Find(item.IdEditor).Name;
                result.Status = contentodbContext.Status.Find(item.Status).Name;
                result.StartedDate = item.StartedDate;
                result.EndDate = item.EndDate;
                result.customerName = contentodbContext.Users.Find(item.IdCustomer).Name;
                foreach (var tag in item.CampaignTags)
                {
                    var tagName = contentodbContext.Tags.Find(tag.IdTags).Name;
                    listTag.Add(tagName);
                }
                result.listTag = listTag;
                returnResult.Add(result);
            }
            return  returnResult;
        }
    }
}
        

