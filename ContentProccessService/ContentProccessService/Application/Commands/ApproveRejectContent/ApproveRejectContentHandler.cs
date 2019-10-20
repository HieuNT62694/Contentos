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
        private readonly ContentoContext contentodbContext;
        public ApproveRejectContentHandler(ContentoContext contentodbContext)
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
                    contentodbContext.Attach(upStatus);
                    contentodbContext.Entry(upStatus).Property(x => x.Status).IsModified = true;
                }
                else
                {
                    //change status task
                    var upStatus = contentodbContext.Tasks.AsNoTracking().FirstOrDefault(y => y.Id == request.IdTask);
                    upStatus.Status = 2;
                    contentodbContext.Attach(upStatus);
                    contentodbContext.Entry(upStatus).Property(x => x.Status).IsModified = true;
                    // add commment
                    var addComment = new Comments
                    {
                        Comment = request.Comments,
                        CreateDate = DateTime.UtcNow
                    };
                    contentodbContext.Comments.Add(addComment);
                    await contentodbContext.SaveChangesAsync(cancellationToken);
                    //change id comment in contents
                    var upContent = contentodbContext.Contents.FirstOrDefault(y => y.Id == request.IdContent);
                    upContent.IsActive = false;
                    contentodbContext.Attach(upContent);
                    contentodbContext.Entry(upContent).Property(x => x.IdComment).IsModified = true;
                    ////insert new content
                    //var addContent = contentodbContext.Contents.FirstOrDefault(y => y.Id == request.IdContent);
                    //addContent.IdComment = addComment.Id;
                    //addContent.Version += 1;
                    //contentodbContext.Add
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
