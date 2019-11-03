using ContentProccessService.Entities;
using ContentProccessService.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetStatusPublish
{
    public class GetStatusPublishHandler : IRequestHandler<GetStatusPublishRequest, List<StatusModelsReturn>>
    {
        private readonly ContentoContext Context;

        public GetStatusPublishHandler(ContentoContext context)
        {
            Context = context;
        }
        public async Task<List<StatusModelsReturn>> Handle(GetStatusPublishRequest request, CancellationToken cancellationToken)
        {
            return Context.StatusTasks.Where(x => x.Id == 5 || x.Id == 6 || x.Id == 7).Select(x =>
           new StatusModelsReturn
           {
               Id = x.Id,
               Name = x.Name
           }).ToList();

        }
    }
}
