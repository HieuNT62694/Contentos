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
        private readonly ContentoDbContext _context;
        public GetTagsByCampaignIdHandler(ContentoDbContext contentodbContext)
        {
            _context = contentodbContext;
        }
        public async Task<List<TagViewModel>> Handle(GetTagsByCampaignIdRequest request, CancellationToken cancellationToken)
        {
            var tmp = await _context.TagsCampaigns.AsNoTracking()
                .Include(t => t.IdTagNavigation).Where(w => w.IdCampaign == request.CampaignId).ToListAsync();

            List<TagViewModel> ls = new List<TagViewModel>();
            foreach(var item in tmp)
            {
                ls.Add(new TagViewModel { id = item.IdTagNavigation.Id, name = item.IdTagNavigation.Name });
            }
            return ls;
        }
    }
}
