using ContentProccessService.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Commands.CreateTasksChannel
{
    public class CreateTaskChannelHandler : IRequestHandler<CreateTaskChannelRequest, TasksFanpages>
    {

        private readonly ContentoDbContext contentodbContext;
        public CreateTaskChannelHandler(ContentoDbContext contentodbContext)
        {
            this.contentodbContext = contentodbContext;
        }
        public async Task<TasksFanpages> Handle(CreateTaskChannelRequest request, CancellationToken cancellationToken)
        {
            var taskchannel = new TasksFanpages
            {
                IdFanpage = request.IdChannel,
                IdTask = request.IdTask,
                ModifiedDate = DateTime.Now,
                CreatedDate = DateTime.Now,
            };

            contentodbContext.TasksFanpages.Add(taskchannel);
            contentodbContext.Attach(taskchannel);
            await contentodbContext.SaveChangesAsync();
            return taskchannel;
        }
    }
}
