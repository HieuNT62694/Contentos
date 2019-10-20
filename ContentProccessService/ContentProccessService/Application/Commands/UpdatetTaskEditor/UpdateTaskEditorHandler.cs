using ContentProccessService.Entities;
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
    public class UpdateTaskEditorHandler : IRequestHandler<UpdateTaskEditorCommand, UpdateTaskModel>
    {
        private readonly ContentoContext contentodbContext;
        public UpdateTaskEditorHandler(ContentoContext contentodbContext)
        {
            this.contentodbContext = contentodbContext;
        }
        public async Task<UpdateTaskModel> Handle(UpdateTaskEditorCommand request, CancellationToken cancellationToken)
        {
            var transaction = contentodbContext.Database.BeginTransaction();
            try
            {
                var upTask = contentodbContext.Tasks.AsNoTracking().Include(y=>y.TasksTags).FirstOrDefault(x => x.Id == request.IdTask);
                var resultReturn = new UpdateTaskModel();
                if (upTask == null)
                {
                    return null;
                }
                if (upTask.Status == 1)
                {
                    var Tags = new List<TasksTags>();

                    foreach (var item in request.Tags)
                    {
                        var tag = new TasksTags { IdTag = item.Id, CreatedDate = DateTime.UtcNow };
                        Tags.Add(tag);
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
                    var writer = new UsersModels
                    {
                        Id = upTask.IdWriter,
                        Name = contentodbContext.Users.FirstOrDefault(x => x.Id == upTask.IdWriter).Name
                    };
                    resultReturn.Title = upTask.Title;
                    resultReturn.Writer = writer;
                    resultReturn.Description = upTask.Description;
                    resultReturn.Deadline = upTask.Deadline;
                    resultReturn.PublishTime = upTask.PublishTime;
                    resultReturn.Tags = Tags;
                    transaction.Commit();
                    return resultReturn;
                }
                //update with status !=1
                upTask.Title = request.Title;
                upTask.Description = request.Description;
                upTask.Deadline = request.Deadline;
                upTask.PublishTime = request.PublishTime;
                contentodbContext.Attach(upTask);
                contentodbContext.Entry(upTask).State = EntityState.Modified;
                await contentodbContext.SaveChangesAsync(cancellationToken);
                resultReturn.Title = upTask.Title;
                resultReturn.Description = upTask.Description;
                resultReturn.Deadline = upTask.Deadline;
                resultReturn.PublishTime = upTask.PublishTime;
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
