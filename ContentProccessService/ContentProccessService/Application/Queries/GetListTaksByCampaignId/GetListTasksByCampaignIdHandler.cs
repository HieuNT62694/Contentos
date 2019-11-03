using ContentProccessService.Entities;
using ContentProccessService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetListTaksByCampaignId
{
    public class GetListTasksByCampaignIdHandler : IRequestHandler<GetListTasksByCampaignIdRequest, List<TasksViewModel>>
    {
        private readonly ContentoDbContext _context;
        public GetListTasksByCampaignIdHandler(ContentoDbContext contentodbContext)
        {
            _context = contentodbContext;
        }
        public async Task<List<TasksViewModel>> Handle(GetListTasksByCampaignIdRequest request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks.AsNoTracking()
                .Where(x => x.IdCampaign == request.IdCampaign).ToListAsync();

            var lstTask = new List<TasksViewModel>();
            foreach (var item in task)
            {
                var wtn = _context.Users.FirstOrDefault(x => x.Id == item.IdWritter);
                var Writter = new UsersModels
                {
                    Id = item.IdWritter,
                    Name = wtn.FirstName + " " + wtn.LastName
                };
                var Status = new StatusModels
                {
                    Id = item.Status,
                    Name = _context.StatusTasks.FirstOrDefault(x => x.Id == item.Status).Name,
                    Color = _context.StatusTasks.FirstOrDefault(x => x.Id == item.Status).Color,
                };
                var taskView = new TasksViewModel()
                {
                    Title = item.Title,
                    Deadline = item.Deadline,
                    PublishTime = item.PublishTime,
                    Writer = Writter,
                    //Description = item.Description,
                    Status = Status,
                    StartedDate = item.StartDate,
                    Id = item.Id
                };
                lstTask.Add(taskView);
            }

            return lstTask;
        }
    }
}
