using ContentProccessService.Application.Dtos;
using ContentProccessService.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetListTaskByIdMarketer
{
    public class GetListTaskByIdMarketerHandler : IRequestHandler<GetListTaskByIdMarketerRequest, List<TasksViewModel>>
    {
        private readonly ContentoContext _context;
        public GetListTaskByIdMarketerHandler(ContentoContext contentodbContext)
        {
            _context = contentodbContext;
        }
        public async Task<List<TasksViewModel>> Handle(GetListTaskByIdMarketerRequest request, CancellationToken cancellationToken)
        {
            var lstIdCampaign = await _context.Campaign.AsNoTracking().Where(x => x.IdMarketer == request.IdMartketer).ToListAsync();
            var lstTask = new List<TasksViewModel>();
            foreach (var item in lstIdCampaign)
            {
                var task = _context.Tasks.Where(x => x.IdCampaign == item.Id && x.Status == 5).ToList();
                foreach (var itemtask in task)
                {
                    var Writter = new UsersModels
                    {
                        Id = itemtask.IdWriter,
                        Name = _context.Users.FirstOrDefault(x => x.Id == itemtask.IdWriter).Name
                    };
                    var Status = new StatusModels
                    {
                        Id = itemtask.Status,
                        Name = _context.StatusTasks.FirstOrDefault(x => x.Id == itemtask.Status).Name,
                        Color = _context.StatusTasks.FirstOrDefault(x => x.Id == itemtask.Status).Color
                    };
                    var taskView = new TasksViewModel()
                    {
                        Title = itemtask.Title,
                        Deadline = itemtask.Deadline,
                        PublishTime = itemtask.PublishTime,
                        Writer = Writter,
                        Description = itemtask.Description,
                        Status = Status,
                        StartedDate = itemtask.StartedDate,
                        Id = itemtask.Id
                    };
                    lstTask.Add(taskView);
                }

                
            }
            return lstTask;
        }
    }
}
