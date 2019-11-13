using ContentProccessService.Entities;
using ContentProccessService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Commands.ApproveRejectContent
{
    public class ApproveRejectContentHandler : IRequestHandler<ApproveRejectContentRequest>
    {
        private readonly ContentoDbContext contentodbContext;
        public ApproveRejectContentHandler(ContentoDbContext contentodbContext)
        {
            this.contentodbContext = contentodbContext;
        }
        public async Task<Unit> Handle(ApproveRejectContentRequest request, CancellationToken cancellationToken)
        {
            var transaction = contentodbContext.Database.BeginTransaction();
            try
            {

                if (request.Button == true)
                {
                    //change status task
                    var upStatus = contentodbContext.Tasks.AsNoTracking().FirstOrDefault(y => y.Id == request.IdTask);
                    upStatus.Status = 5;
                    upStatus.ModifiedDate = DateTime.UtcNow;
                    contentodbContext.Attach(upStatus);
                    contentodbContext.Entry(upStatus).Property(x => x.Status).IsModified = true;
                    var upContent = contentodbContext.Contents.FirstOrDefault(y => y.Id == request.IdContent);
                    upContent.TheContent = request.Comments;
                    upContent.Name = request.Name;
                    upContent.ModifiedDate = DateTime.UtcNow;
                    contentodbContext.Attach(upContent);
                    contentodbContext.Entry(upContent).State = EntityState.Modified;
                }
                else
                {
                    //change status task
                    var upStatus = contentodbContext.Tasks.AsNoTracking().FirstOrDefault(y => y.Id == request.IdTask);
                    upStatus.Status = 2;
                    upStatus.ModifiedDate = DateTime.UtcNow;
                    contentodbContext.Attach(upStatus);
                    contentodbContext.Entry(upStatus).Property(x => x.Status).IsModified = true;
                    // add commment
                    var addComment = new Comments
                    {
                        Comment = request.Comments,
                        IdContent = request.IdContent,
                        CreatedDate = DateTime.UtcNow,
                        IsActive = true,
                        
                    };
                    contentodbContext.Comments.Add(addComment);
                    await contentodbContext.SaveChangesAsync(cancellationToken);
                    //change id comment in contents
                    var upContent = contentodbContext.Contents.FirstOrDefault(y => y.Id == request.IdContent);
                    upContent.IsActive = false;
                    contentodbContext.Attach(upContent);
                    //contentodbContext.Entry(upContent).Property(x => x.IdComment).IsModified = true;
                    ////insert new content
                    var addContent = new Contents();
                    addContent.IdTask = upContent.IdTask;
                    addContent.IsActive = true;
                    addContent.CreatedDate = DateTime.UtcNow;
                    //addContent.IdComment = addComment.Id;
                    addContent.Version = upContent.Version + 1;
                    addContent.TheContent = upContent.TheContent;
                    addContent.Name = request.Name;
                    contentodbContext.Contents.Add(addContent);
                }
                await contentodbContext.SaveChangesAsync(cancellationToken);
                transaction.Commit();
                return Unit.Value;
            }
            catch(Exception e)
            {
                transaction.Rollback();
                return Unit.Value;
                
            }
        }
    }
}
