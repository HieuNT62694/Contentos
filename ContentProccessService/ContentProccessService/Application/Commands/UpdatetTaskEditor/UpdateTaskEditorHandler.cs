﻿using ContentProccessService.Entities;
using ContentProccessService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Commands.UpdatetTaskEditor
{
    public class UpdateTaskEditorHandler : IRequestHandler<UpdateTaskEditorCommand, ReturnUpdateTaskModel>
    {
        private readonly ContentoContext contentodbContext;
        public UpdateTaskEditorHandler(ContentoContext contentodbContext)
        {
            this.contentodbContext = contentodbContext;
        }
        public async Task<ReturnUpdateTaskModel> Handle(UpdateTaskEditorCommand request, CancellationToken cancellationToken)
        {
            var transaction = contentodbContext.Database.BeginTransaction();
            try
            {
                var upTask = contentodbContext.Tasks.AsNoTracking().Include(y=>y.TasksTags).FirstOrDefault(x => x.Id == request.IdTask);
                var resultReturn = new ReturnUpdateTaskModel();
                var Tags = new List<TasksTags>();
                var TagsReturn = new List<TagsViewModel>();
                var writer = new UsersModels();
                if (upTask == null)
                {
                    return null;
                }
                if (upTask.Status == 1)
                {
                   

                    foreach (var item in request.Tags)
                    {
                        var tag = new TasksTags { IdTag = item.Id,ModifiedDate = DateTime.UtcNow};
                        var tagReturn = new TagsViewModel { Id = item.Id ,Name = contentodbContext.Tags.FirstOrDefault(x => x.Id == item.Id).Name };
                        Tags.Add(tag);
                        TagsReturn.Add(tagReturn);
                    }
                    contentodbContext.TasksTags.RemoveRange(upTask.TasksTags);
                    upTask.Title = request.Title;
                    upTask.IdWriter = request.IdWriter;
                    upTask.Description = request.Description;
                    upTask.Deadline = request.Deadline;
                    upTask.PublishTime = request.PublishTime;
                    upTask.TasksTags = Tags;

                    upTask.ModifiedDate = DateTime.UtcNow;
                    contentodbContext.Attach(upTask);
                    contentodbContext.Entry(upTask).State = EntityState.Modified;

                    await contentodbContext.SaveChangesAsync(cancellationToken);

                    writer.Id = upTask.IdWriter;
                    writer.Name = contentodbContext.Users.FirstOrDefault(x => x.Id == upTask.IdWriter).Name;
                    
                    resultReturn.Title = upTask.Title;
                    resultReturn.Writer = writer;
                    resultReturn.Description = upTask.Description;
                    resultReturn.Deadline = upTask.Deadline;
                    resultReturn.PublishTime = upTask.PublishTime;
                    resultReturn.Tags = TagsReturn;
                    resultReturn.Id = request.IdTask;
                    transaction.Commit();
                    return resultReturn;
                }
                //update with status !=1
                foreach (var item in request.Tags)
                {
                    var tag = new TasksTags { IdTag = item.Id, ModifiedDate = DateTime.UtcNow };
                    var tagReturn = new TagsViewModel { Id = item.Id, Name = contentodbContext.Tags.FirstOrDefault(x => x.Id == item.Id).Name };
                    Tags.Add(tag);
                    TagsReturn.Add(tagReturn);
                }
                upTask.Deadline = request.Deadline;
                upTask.PublishTime = request.PublishTime;
                if (request.Deadline > DateTime.UtcNow)
                {
                    upTask.Status = 2;
                }
                contentodbContext.Attach(upTask);
                contentodbContext.Entry(upTask).State = EntityState.Modified;
                await contentodbContext.SaveChangesAsync(cancellationToken);
                var Status = new StatusModels()
                {
                    Id = upTask.Status,
                    Name = contentodbContext.StatusTasks.FirstOrDefault(x => x.Id == upTask.Status).Name,
                    Color = contentodbContext.StatusTasks.FirstOrDefault(x => x.Id == upTask.Status).Color
                };
                writer.Id = upTask.IdWriter;
                writer.Name = contentodbContext.Users.FirstOrDefault(x => x.Id == upTask.IdWriter).Name;

                resultReturn.Title = upTask.Title;
                resultReturn.Writer = writer;
                resultReturn.Description = upTask.Description;
                resultReturn.Deadline = upTask.Deadline;
                resultReturn.PublishTime = upTask.PublishTime;
                resultReturn.Tags = TagsReturn;
                resultReturn.Status = Status;
                resultReturn.Id = request.IdTask;
                resultReturn.Deadline = upTask.Deadline;
                transaction.Commit();
                return resultReturn;

            }
            catch (Exception e)
            {
                transaction.Rollback();
                return null;
            }
        }
    }
}
