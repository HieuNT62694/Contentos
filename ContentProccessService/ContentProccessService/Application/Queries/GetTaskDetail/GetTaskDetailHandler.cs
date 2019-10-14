using ContentProccessService.Application.Dtos;
using ContentProccessService.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetTaskDetail
{
    public class GetTaskDetailHandler : IRequestHandler<GetTaskDetailRequest, TasksViewModel>
    {
        private readonly ContentoContext contentodbContext;
        public GetTaskDetailHandler(ContentoContext contentodbContext)
        {
            this.contentodbContext = contentodbContext;
        }
        public async Task<TasksViewModel> Handle(GetTaskDetailRequest request, CancellationToken cancellationToken)
        {
            var task = contentodbContext.Tasks.FirstOrDefault(x => x.Id == request.IdTask);

                var Writter = new UsersModels
                {
                    IdUser = task.IdWriter,
                    Name = contentodbContext.Users.FirstOrDefault(x => x.Id == task.IdWriter).Name
                };
                var Status = new StatusModels
                {
                    IdStatus = task.Status,
                    Name = contentodbContext.Status.FirstOrDefault(x => x.Id == task.Status).Name
                };
                var taskView = new TasksViewModel()
                {
                    Title = task.Title,
                    Deadline = task.Deadline,
                    PublishTime = task.PublishTime,
                    Writer = Writter,
                    Description = task.Description,
                    Status = Status,
                    StartedDate = task.StartedDate,
                    Id = task.Id
                };


            return taskView;
        }
    }
}
