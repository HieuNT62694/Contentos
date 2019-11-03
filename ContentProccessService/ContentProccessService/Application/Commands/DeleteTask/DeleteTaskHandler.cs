using ContentProccessService.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Commands.DeleteTask
{
    public class DeleteTaskHandler : IRequestHandler<DeleteTaskRequest>
    {
        private readonly ContentoDbContext contentodbContext;
        public DeleteTaskHandler(ContentoDbContext contentodbContext)
        {
            this.contentodbContext = contentodbContext;
        }
        public async Task<Unit> Handle(DeleteTaskRequest request, CancellationToken cancellationToken)
        {
            var transactaion = contentodbContext.Database.BeginTransaction();
            try
            {
                var delTask = contentodbContext.Tasks.AsNoTracking().Include(x => x.Contents).Include(x => x.TasksTags).Include(x => x.TasksFanpages).FirstOrDefault(y => y.Id == request.IdTask);
                contentodbContext.TasksTags.RemoveRange(delTask.TasksTags);
                contentodbContext.TasksFanpages.RemoveRange(delTask.TasksFanpages);
                contentodbContext.Contents.RemoveRange(delTask.Contents);
                contentodbContext.Tasks.Remove(delTask);
                await contentodbContext.SaveChangesAsync();
                transactaion.Commit();
                return Unit.Value;
            }
            catch (Exception e)
            {
                transactaion.Rollback();
                return Unit.Value;
            }
           
        }
    }
}
