using ContentProccessService.Entities;
using ContentProccessService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetTaskDetailUpdate
{
    public class GetTaskDetailUpdateHandler : IRequestHandler<GetTaskDetailUpdateRequest, TasksViewModel>
    {
        private readonly ContentoContext _context;
        public GetTaskDetailUpdateHandler(ContentoContext contentodbContext)
        {
            _context = contentodbContext;
        }
        public async Task<TasksViewModel> Handle(GetTaskDetailUpdateRequest request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.IdTask);
            var edtId = _context.Campaign.Find(task.IdCampaign).IdEditor;
            var lstTag = new List<TagsViewModel>();
            var lstTags = _context.TasksTags.Where(x => x.IdTask == request.IdTask).ToList();
            foreach (var item in lstTags)
            {
                var tag = new TagsViewModel();
                tag.Name = _context.Tags.FirstOrDefault(x => x.Id == item.IdTag).Name;
                tag.Id = item.IdTag;
                lstTag.Add(tag);
            }
            var Writter = new UsersModels
            {
                Id = task.IdWriter,
                Name = _context.Users.FirstOrDefault(x => x.Id == task.IdWriter).Name
            };
            var Status = new StatusModels
            {
                Id = task.Status,
                Name = _context.StatusTasks.FirstOrDefault(x => x.Id == task.Status).Name,
                Color = _context.StatusTasks.FirstOrDefault(x => x.Id == task.Status).Color
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
                Id = task.Id,
                Tags = lstTag
            };

            return taskView;
        }
    }
}
