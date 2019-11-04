using CampaignService.Entities;
using CampaignService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CampaignService.Application.Queries.GetListCampaignByWriterId
{
    public class GetListCampaignByWriterIdHandler : IRequestHandler<GetListCampaignByWriterIdRequest, List<CampaignModels>>
    {

        private readonly ContentoContext _context;
        public GetListCampaignByWriterIdHandler(ContentoContext contentodbContext)
        {
            _context = contentodbContext;
        }
        public async Task<List<CampaignModels>> Handle(GetListCampaignByWriterIdRequest request, CancellationToken cancellationToken)
        {
            return await _context.Tasks.AsNoTracking().Include(x => x.IdCampaignNavigation).Where(x => x.IdWriter == request.IdWriter)
                .Select(x => new CampaignModels { Id = x.IdCampaignNavigation.Id,Name = x.IdCampaignNavigation.Title }).Distinct()
                .ToListAsync();
        }
    }
}
