using ContentProccessService.Application.Models;
using ContentProccessService.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Commands.CreateTasks
{
    public class CreateTasksHandler : IRequestHandler<CreateTasksRequest, List<TasksViewModel>>
    {
        private readonly ContentoContext contentodbContext;
        public CreateTasksHandler(ContentoContext contentodbContext)
        {
            this.contentodbContext = contentodbContext;
        }
        public async Task<List<TasksViewModel>> Handle(CreateTasksRequest request, CancellationToken cancellationToken)
        {
            List<TasksViewModel> tasks = new List<TasksViewModel>();

            foreach (var item in request.tasks)
            {
                var task = new Tasks
                {
                    IdCampaign = item.IdCampaign,
                    IdWriter = item.IdWriter,
                    Deadline = item.Deadline,
                    Description = item.Description,
                    PublishTime = item.PublishTime,
                    Title = item.Title,
                    StartedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = 1
                };
                contentodbContext.Attach(task);
                contentodbContext.Tasks.Add(task);
                await contentodbContext.SaveChangesAsync(cancellationToken);
                var taskModel = new TasksViewModel
                {
                    Deadline = task.Deadline,
                    Id = task.Id,
                    Description = task.Description,
                    StartedDate = task.StartedDate,
                    PublishTime = task.PublishTime,
                    Title = task.Title,
                };
                tasks.Add(taskModel);
            }
            return tasks;
        }
    }
}
