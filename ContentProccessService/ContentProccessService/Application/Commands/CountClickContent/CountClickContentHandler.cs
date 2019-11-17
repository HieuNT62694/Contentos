using ContentProccessService.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Commands.CountClickContent
{
    public class CountClickContentHandler : IRequestHandler<CountClickContentCommands>
    {
        private readonly ContentoDbContext contentodbContext;

        public CountClickContentHandler(ContentoDbContext contentodbContext)
        {
            this.contentodbContext = contentodbContext;
        }
        public async Task<Unit> Handle(CountClickContentCommands request, CancellationToken cancellationToken)
        {
            //contentodbContext.Personalizations.Where(x => request.Tags.Contains(x.IdTag) && x.IdUser == request.IdUser).ToList().ForEach(x => x.TimeInteraction  += 1);
            var checl = await contentodbContext.UsersInteractions.FirstOrDefaultAsync(x => x.IdTask == request.IdTask && x.IdUser == request.IdUser);
            if (checl == null)
            {
                var userInter = new UsersInteractions
                {
                    IdTask = request.IdTask,
                    IdUser = request.IdUser,
                    Interaction = 0
                };
                contentodbContext.Attach(userInter);
                contentodbContext.UsersInteractions.Add(userInter);
                await contentodbContext.SaveChangesAsync(cancellationToken);
            }
            contentodbContext.UsersInteractions.FirstOrDefault(x => x.IdTask == request.IdTask && x.IdUser == request.IdUser).Interaction += 1;
            await contentodbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
