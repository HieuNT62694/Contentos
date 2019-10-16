using ContentProccessService.Application.Models;
using ContentProccessService.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Commands.CreateTask
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskRequest, Tasks>
    {
        private readonly ContentoContext contentodbContext;
        public CreateTaskHandler(ContentoContext contentodbContext)
        {
            this.contentodbContext = contentodbContext;
        }
        public async Task<Tasks> Handle(CreateTaskRequest request, CancellationToken cancellationToken)
        {
            var task = new Tasks
            {
                IdCampaign = request.Task.IdCampaign,
                IdWriter = request.Task.IdWriter,
                Description = request.Task.Description,
                Deadline = request.Task.Deadline,
                PublishTime = request.Task.PublishTime,
                Title = request.Task.Title,
                StartedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Status = 1
            };
            contentodbContext.Tasks.Add(task);
            await contentodbContext.SaveChangesAsync();
            return task;
        }


    }
}
