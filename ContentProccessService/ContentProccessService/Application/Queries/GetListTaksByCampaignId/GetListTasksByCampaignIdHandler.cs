using ContentProccessService.Application.Dtos;
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
        private readonly ContentoContext contentodbContext;
        public GetListTasksByCampaignIdHandler(ContentoContext contentodbContext)
        {
            this.contentodbContext = contentodbContext;
        }
        public async Task<List<TasksViewModel>> Handle(GetListTasksByCampaignIdRequest request, CancellationToken cancellationToken)
        {
            var task = contentodbContext.Tasks.Where(x => x.IdCampaign == request.IdCampaign).ToList();
            var lstTask = new List<TasksViewModel>();
            foreach (var item in task)
            {
                var Writter = new UsersModels
                {
                    IdUser = item.IdWriter,
                    Name = contentodbContext.Users.FirstOrDefault(x => x.Id == item.IdWriter).Name
                };
                var Status = new StatusModels
                {
                    IdStatus = item.Status,
                    Name = contentodbContext.Status.FirstOrDefault(x => x.Id == item.Status).Name
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
