﻿using ContentProccessService.Entities;
using ContentProccessService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetListTaskByIdWriter
{

    public class GetListTaskByIdWriterHandler : IRequestHandler<GetListTaskByIdWriterRequest, List<TasksViewByEditorModel>>
    {
        private readonly ContentoContext _context;
        public GetListTaskByIdWriterHandler(ContentoContext contentodbContext)
        {
            _context = contentodbContext;
        }
        public async Task<List<TasksViewByEditorModel>> Handle(GetListTaskByIdWriterRequest request, CancellationToken cancellationToken)
        {
            List<TasksViewByEditorModel> Tasks = new List<TasksViewByEditorModel>();
            var ls = await _context.Tasks.AsNoTracking().Include(t => t.IdCampaignNavigation).Include(g => g.StatusNavigation).Include(f => f.Contents).Where(x => x.IdWriter == request.IdWriter).ToListAsync();

            foreach (var item in ls)
            {
                var Status = new StatusTaskModels
                {
                    Name = item.StatusNavigation.Name,
                    Color = item.StatusNavigation.Color,
                    Id = item.StatusNavigation.Id
                };
                var Campaign = new CampaignModels
                {
                    Id = item.IdCampaignNavigation.Id,
                    Name = item.IdCampaignNavigation.Title
                };
                Tasks.Add(new TasksViewByEditorModel
                {
                    Id = item.Id,
                    //Description = item.Description,
                    Campaign = Campaign,
                    ModifiedDate = item.Contents.Count == 0  ? null : item.Contents.FirstOrDefault().ModifiedDate,
                    Deadline = item.Deadline,
                    Title = item.Title,
                    Status = Status
                });
            }
            return Tasks;
        }
    }
}
