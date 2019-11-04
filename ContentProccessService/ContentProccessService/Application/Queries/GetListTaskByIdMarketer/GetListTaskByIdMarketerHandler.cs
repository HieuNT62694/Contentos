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
        private readonly ContentoDbContext _context;
        public GetListTaskByIdMarketerHandler(ContentoDbContext contentodbContext)
        {
            _context = contentodbContext;
        }
        public async Task<List<TasksViewModel>> Handle(GetListTaskByIdMarketerRequest request, CancellationToken cancellationToken)
        {
            var lstIdCampaign = await _context.Campaigns.AsNoTracking().Include(x => x.Tasks).Where(x => x.IdMarketer == request.IdMartketer && (x.Status == 5 || x.Status == 6 || x.Status == 7)).ToListAsync();
            var lstTask = new List<TasksViewModel>();
            foreach (var item in lstIdCampaign)
            {
                foreach (var itemtask in item.Tasks)
                {
                    var wtn = _context.Users.FirstOrDefault(x => x.Id == itemtask.IdWritter);
                    var Writter = new UsersModels
                    {
                        Id = itemtask.IdWritter,
                        Name = wtn.FirstName + " " +wtn.LastName
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
                        //Description = itemtask.Description,
                        Status = Status,
                        StartedDate = itemtask.StartDate,
                        Id = itemtask.Id
                    };
                    lstTask.Add(taskView);
                }

                
            }
            return lstTask;
        }
    }
}
