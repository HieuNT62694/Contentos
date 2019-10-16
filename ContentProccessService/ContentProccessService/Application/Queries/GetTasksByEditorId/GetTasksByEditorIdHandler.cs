using ContentProccessService.Application.Models;
using ContentProccessService.Entities;
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
            var ls = Context.Tasks.AsNoTracking().Include(t => t.IdCampaignNavigation).Where(t => t.IdCampaignNavigation.IdEditor == request.IdEditor).Where(t => t.Status == 3).ToList();
            
            foreach (var item in ls)
            {
                Tasks.Add(new TasksViewByEditorModel
                {
                    Id = item.Id,
                    Description = item.Description,
                    PublishTime = item.PublishTime,
                    StartDate = item.StartedDate,
                    Title = item.Title,
                    status = item.StatusNavigation,
                });
            }
            return Tasks;
        }


    }
}

