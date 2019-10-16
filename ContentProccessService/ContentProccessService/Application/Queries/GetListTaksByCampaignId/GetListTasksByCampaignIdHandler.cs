using ContentProccessService.Application.Models;
using ContentProccessService.Entities;
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
        private readonly ContentoContext _context;
        public GetListTasksByCampaignIdHandler(ContentoContext contentodbContext)
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
                var Writter = new UsersModels
                {
                    IdUser = item.IdWriter,
                    Name = _context.Users.FirstOrDefault(x => x.Id == item.IdWriter).Name
                };
                var Status = new StatusModels
                {
                    IdStatus = item.Status,
                    Name = _context.StatusTasks.FirstOrDefault(x => x.Id == item.Status).Name
                };
                var taskView = new TasksViewModel()
                {
                    Title = item.Title,
                    Deadline = item.Deadline,
                    PublishTime = item.PublishTime,
                    Writer = Writter,
                    Description = item.Description,
                    Status = Status,
                    StartedDate = item.StartedDate,
                    Id = item.Id
                };
                lstTask.Add(taskView);
            }

            return lstTask;
        }
    }
}
