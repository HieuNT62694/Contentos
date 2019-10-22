using ContentProccessService.Entities;
using ContentProccessService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Commands.StartTask
{
    public class StartTaskHandler : IRequestHandler<StartTaskCommand, TasksViewModel>
    {
        private readonly ContentoContext _context;

        public StartTaskHandler(ContentoContext context)
        {
            _context = context;
        }
        public async Task<TasksViewModel> Handle(StartTaskCommand request, CancellationToken cancellationToken)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                var newContent = new Contents
                {
                    CreatedDate = DateTime.UtcNow,
                    Version = 1,
                    IdTask = request.IdTask,
                    IsActive = true
                };
                _context.Contents.Add(newContent);
                var upTask = _context.Tasks.AsNoTracking().Include(y => y.Contents).FirstOrDefault(x => x.Id == request.IdTask);
                upTask.Status = 2;
                _context.Attach(upTask);
                _context.Entry(upTask).State = EntityState.Modified;
                await _context.SaveChangesAsync(cancellationToken);
                // get detail task
                var task = await _context.Tasks.AsNoTracking().Include(i => i.Contents).Where(n => n.Contents.Any(z => z.IsActive == true))
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
                var Editor = new UsersModels
                {
                    Id = edtId,
                    Name = _context.Users.FirstOrDefault(x => x.Id == edtId).Name
                };
                var Content = new ContentModels
                {
                    Id = task.Contents.FirstOrDefault().Id,
                    Content = task.Contents.FirstOrDefault().TheContent,
                    Name = task.Contents.FirstOrDefault().Name
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
                    Editor = Editor,
                    Content = Content,
                    Id = task.Id,
                    Tags = lstTag
                };
                transaction.Commit();
                return taskView;
                
            }
            catch (Exception)
            {
                transaction.Rollback();
                return null;
            }
            
            
        }
    }
}
