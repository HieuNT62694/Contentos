using ContentProccessService.Entities;
using ContentProccessService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetTasksByEditorId
{
    public class GetTasksByEditorIdHandler : IRequestHandler<GetTasksByEditorIdRequest, List<TasksViewByEditorModel>>
    {
        private readonly ContentoContext Context;

        public GetTasksByEditorIdHandler(ContentoContext context)
        {
            Context = context;
        }

        public async Task<List<TasksViewByEditorModel>> Handle(GetTasksByEditorIdRequest request, CancellationToken cancellationToken)
        {
            List<TasksViewByEditorModel> Tasks = new List<TasksViewByEditorModel>();
            var ls = await Context.Tasks.AsNoTracking().Include(t => t.IdCampaignNavigation).Include(g=>g.StatusNavigation).Include(f => f.Contents).Where(t => t.IdCampaignNavigation.IdEditor == request.IdEditor && t.Contents.Any(x=>x.IsActive == true)).Where(t => t.Status == 3).ToListAsync();
            
            foreach (var item in ls)
            {
                var Status = new StatusTaskModels
                {
                    Name = item.StatusNavigation.Name,
                    Color = item.StatusNavigation.Color,
                    Id = item.StatusNavigation.Id
                };
                Tasks.Add(new TasksViewByEditorModel
                {
                    Id = item.Id,
                    //Description = item.Description,
                    Campaign = item.IdCampaignNavigation.Title,
                    ModifiedDate = item.Contents.FirstOrDefault().ModifiedDate,
                    Deadline = item.Deadline,
                    Title = item.Title,
                    Status = Status
                });
            }
            return Tasks;
        }


    }
}

