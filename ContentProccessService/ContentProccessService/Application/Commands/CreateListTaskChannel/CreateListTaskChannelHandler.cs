using ContentProccessService.Entities;
using ContentProccessService.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Commands.CreateListTaskChannel
{
    public class CreateListTaskChannelHandler : IRequestHandler<CreateListTaskChannelRequest, List<TaskChannelModelRespone>>
    {
        private readonly ContentoContext contentodbContext;

        public CreateListTaskChannelHandler(ContentoContext contentodbContext)
        {
            this.contentodbContext = contentodbContext;
        }

        public async Task<List<TaskChannelModelRespone>> Handle(CreateListTaskChannelRequest request, CancellationToken cancellationToken)
        {
            List<TaskChannelModelRespone> Listtaskschannel = new List<TaskChannelModelRespone>();
            foreach (var item in request.ListTaskChannel)
            {
                var taskchannel = new TasksChannels
                {
                    IdChannel = item.IdChannel,
                    IdTask = item.IdTask,
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                };
                contentodbContext.Attach(taskchannel);
                contentodbContext.TasksChannels.Add(taskchannel);
                await contentodbContext.SaveChangesAsync(cancellationToken);
                TaskChannelModelRespone TaskChannels = new TaskChannelModelRespone
                {
                    id = taskchannel.Id,
                    IdChannel = taskchannel.IdChannel,
                    IdTask = taskchannel.IdTask
                };
                Listtaskschannel.Add(TaskChannels);

            }
            return Listtaskschannel;
        }

    }
}
