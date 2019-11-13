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
            contentodbContext.Personalizations.Where(x => request.Tags.Contains(x.IdTag) && x.IdUser == request.Id).ToList().ForEach(x => x.TimeInteraction  += 1);
            await contentodbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
