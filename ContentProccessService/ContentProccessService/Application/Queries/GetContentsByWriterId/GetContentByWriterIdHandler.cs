using ContentProccessService.Entities;
using ContentProccessService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetContentsByWriterId
{
    public class GetContentByWriterIdHandler : IRequestHandler<GetContentsByWriterIdRequest, List<ContentsViewModel>>
    {
        private readonly ContentoContext context;

        public GetContentByWriterIdHandler(ContentoContext context)
        {
            this.context = context;
        }

        public async Task<List<ContentsViewModel>> Handle(GetContentsByWriterIdRequest request, CancellationToken cancellationToken)
        {
            var list =  await context.Contents.AsNoTracking().Include(c => c.IdTaskNavigation).Where(t => t.IdTaskNavigation.IdWriter == request.IdWriter
            && (t.IdTaskNavigation.Status == 2 || t.IdTaskNavigation.Status == 1)).Where(c => c.Version == 1).Where(c => c.IsActive == true).ToListAsync();
            List<ContentsViewModel> Contents = new List<ContentsViewModel>();
            foreach (var item in list)
            {
                ContentsViewModel Content = new ContentsViewModel
                {
                    Id = item.Id,
                    Comment = item.IdCommentNavigation,
                    Content = item.TheContent,
                    Name = item.Name
                };
                Contents.Add(Content);
            }
            return Contents;
        }
    }
}
