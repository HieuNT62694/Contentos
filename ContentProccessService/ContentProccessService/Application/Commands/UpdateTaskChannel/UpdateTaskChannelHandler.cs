
using ContentProccessService.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Commands.UpdateTaskChannel
{
    public class UpdateTaskChannelHandler : IRequestHandler<UpdateTaskChannelRequest, TasksChannels>
    {
        private readonly ContentoDbContext _context;

        public UpdateTaskChannelHandler(ContentoDbContext context)
        {
            _context = context;
        }

        public async Task<TasksChannels> Handle(UpdateTaskChannelRequest request, CancellationToken cancellationToken)
        {
            //TasksChannels taskchannel = _context.TasksFanpages.Find(request.IdTaskChannel);
            //_context.TasksFanpages.Remove(taskchannel);
            TasksChannels taskchannel = new TasksChannels();
            await _context.SaveChangesAsync(cancellationToken);
            return taskchannel;
        }
    }
}

