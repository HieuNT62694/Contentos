
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
        private readonly ContentoContext _context;

        public UpdateTaskChannelHandler(ContentoContext context)
        {
            _context = context;
        }

        public async Task<TasksChannels> Handle(UpdateTaskChannelRequest request, CancellationToken cancellationToken)
        {
            TasksChannels taskchannel = _context.TasksChannels.Find(request.IdTaskChannel);
            _context.TasksChannels.Remove(taskchannel);
            await _context.SaveChangesAsync(cancellationToken);
            return taskchannel;
        }
    }
}

