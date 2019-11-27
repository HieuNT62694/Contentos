using BatchjobService.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BatchjobService.Application.Command.DeleteFanpage
{
    public class DeleteFanpageHandler : IRequestHandler<DeleteFanpageCommand, Unit>
    {
        private readonly ContentoDbContext _context;

        public DeleteFanpageHandler(ContentoDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteFanpageCommand request, CancellationToken cancellationToken)
        {
            Fanpages fanpage = _context.Fanpages.Find(request.id);

            _context.Entry(fanpage).Collection(r => r.FanpagesTags).Load();

            _context.RemoveRange(fanpage.FanpagesTags);
            _context.Remove(fanpage);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
