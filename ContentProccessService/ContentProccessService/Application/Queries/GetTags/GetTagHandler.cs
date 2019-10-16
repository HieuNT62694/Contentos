using ContentProccessService.Entities;
using ContentProccessService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetTags
{
    public class GetTagHandler : IRequestHandler<GetTagRequest, IEnumerable<TagViewModel>>
    {
        private readonly ContentoContext _context;
        public GetTagHandler(ContentoContext contentodbContext)
        {
            _context = contentodbContext;
        }

        public async Task<IEnumerable<TagViewModel>> Handle(GetTagRequest request, CancellationToken cancellationToken)
        {
            var tags = await _context.Tags.AsNoTracking().ToListAsync();
            List<TagViewModel> list = new List<TagViewModel>();
            foreach(var item in tags)
            {
                list.Add( new TagViewModel { id = item.Id, name = item.Name });
            }

            return list;
        }
    }
}
