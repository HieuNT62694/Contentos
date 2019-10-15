using ContentProccessService.Entities;
using ContentProccessService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetTagsByCampaignId
{
    public class GetTagsByCampaignIdHandler : IRequestHandler<GetTagsByCampaignIdRequest, List<TagViewModel>>
    {
        private readonly ContentoContext _context;
        public GetTagsByCampaignIdHandler(ContentoContext contentodbContext)
        {
            _context = contentodbContext;
        }
        public async Task<List<TagViewModel>> Handle(GetTagsByCampaignIdRequest request, CancellationToken cancellationToken)
        {
            var tmp = await _context.CampaignTags.AsNoTracking()
                .Include(t => t.IdTagsNavigation).Where(w => w.IdCampaign == request.CampaignId).ToListAsync();

            List<TagViewModel> ls = new List<TagViewModel>();
            foreach(var item in tmp)
            {
                ls.Add(new TagViewModel { id = item.IdTagsNavigation.Id, name = item.IdTagsNavigation.Name });
            }
            return ls;
        }
    }
}
