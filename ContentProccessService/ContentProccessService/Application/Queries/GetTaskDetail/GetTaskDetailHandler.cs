using ContentProccessService.Application.Models;
using ContentProccessService.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetTaskDetail
{
    public class GetTaskDetailHandler : IRequestHandler<GetTaskDetailRequest, TasksViewModel>
    {
        private readonly ContentoContext _context;
        public GetTaskDetailHandler(ContentoContext contentodbContext)
        {
            _context = contentodbContext;
        }
        public async Task<TasksViewModel> Handle(GetTaskDetailRequest request, CancellationToken cancellationToken)
        {
            var task = _context.Tasks.AsNoTracking()
                .FirstOrDefault(x => x.Id == request.IdTask);

                var Writter = new UsersModels
                {
                    IdUser = task.IdWriter,
                    Name = _context.Users.FirstOrDefault(x => x.Id == task.IdWriter).Name
                };
                var Status = new StatusModels
                {
                    IdStatus = task.Status,
                    Name = _context.StatusTasks.FirstOrDefault(x => x.Id == task.Status).Name
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
