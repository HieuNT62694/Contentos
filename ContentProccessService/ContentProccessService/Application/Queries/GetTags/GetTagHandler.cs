using ContentProccessService.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetTags
{
    public class GetTagHandler : IRequestHandler<GetTagRequest, IEnumerable<Tags>>
    {
        private readonly ContentoContext contentodbContext;
        public GetTagHandler(ContentoContext contentodbContext)
        {
            this.contentodbContext = contentodbContext;
        }

        public async Task<IEnumerable<Tags>> Handle(GetTagRequest request, CancellationToken cancellationToken)
        {
            return await contentodbContext.Tags.Include(t => t.TasksTags).ToListAsync<Tags>();
        }
    }
}
