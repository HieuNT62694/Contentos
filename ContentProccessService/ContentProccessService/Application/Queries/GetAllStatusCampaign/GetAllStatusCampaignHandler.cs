using ContentProccessService.Entities;
using ContentProccessService.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetAllStatusCampaign
{
    public class GetAllStatusCampaignHandler : IRequestHandler<GetAllStatusCampaignRequest, List<StatusModelsReturn>>
    {
        private readonly ContentoContext Context;

        public GetAllStatusCampaignHandler(ContentoContext context)
        {
            Context = context;
        }
        public async Task<List<StatusModelsReturn>> Handle(GetAllStatusCampaignRequest request, CancellationToken cancellationToken)
        {
            return Context.StatusCampaign.Select(x =>
           new StatusModelsReturn
           {
               Id = x.Id,
               Name = x.Name
           }).ToList();

        }
    }
}
