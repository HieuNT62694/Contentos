using ContentProccessService.Entities;
using ContentProccessService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetTaskbyTagInteraction
{
    public class GetTaskbyTagInteractionHandler : IRequestHandler<GetTaskbyTagInteractionRequest, List<ListTaskStaticModel>>
    {
        private readonly ContentoDbContext _context;
        public GetTaskbyTagInteractionHandler(ContentoDbContext contentodbContext)
        {
            _context = contentodbContext;
        }
        public async Task<List<ListTaskStaticModel>> Handle(GetTaskbyTagInteractionRequest request, CancellationToken cancellationToken)
        {
            var lstTask = await _context.Tasks.Include(x => x.TasksTags).Where(x => x.TasksTags.Any(y => y.IdTag == request.Id))
            .Select(x => new ListTaskStaticModel
            {
                IdTask = x.Id,
                Published = x.Status == 7 ? true : false,
                Title = x.Contents == null ? null : x.Contents.FirstOrDefault(y => y.IsActive == true).Name
            }).ToListAsync();
            var lstTaskInter = _context.Statistics.Where(x => lstTask.Select(y => y.IdTask).Contains(x.IdTask) && x.CreatedDate >= DateTime.UtcNow.AddDays(-7) && x.CreatedDate < DateTime.UtcNow).ToList();
            var lstTaskInerCount = new List<ListTaskModel>();
            foreach (var item in lstTaskInter)
            {
                if (lstTaskInerCount.Any(x => x.IdTask == item.IdTask))
                {
                    lstTaskInerCount.FirstOrDefault(x => x.IdTask == item.IdTask).TimeInTeraction += item.Views ?? 0;
                }
                else
                {
                    var Taskcnt = new ListTaskModel
                    {
                        IdTask = item.IdTask,
                        TimeInTeraction = item.Views ?? 0
                    };
                    lstTaskInerCount.Add(Taskcnt);
                }
            }
            foreach (var item2 in lstTask)
            {
                item2.View = lstTaskInerCount.FirstOrDefault(x => x.IdTask == item2.IdTask) == null ? 0 : lstTaskInerCount.FirstOrDefault(x => x.IdTask == item2.IdTask).TimeInTeraction;
            }
            return lstTask.OrderByDescending(x=>x.View).ToList();
        }
    }
}
