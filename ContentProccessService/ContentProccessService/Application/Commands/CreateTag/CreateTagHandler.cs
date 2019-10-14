using ContentProccessService.Application.Dtos;
using ContentProccessService.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Commands.CreateTag
{
    public class CreateTagHandler : IRequestHandler<CreateTagRequest, Unit>
    {
        private readonly ContentoContext contentodbContext;
        public CreateTagHandler(ContentoContext contentodbContext)
        {
            this.contentodbContext = contentodbContext;
        }
        public async Task<Unit> Handle(CreateTagRequest request, CancellationToken cancellationToken)
        {
            var tag = new Tags {Name = request.dto.Name, IsActive = request.dto.IsActive, CreatedDate = DateTime.Now};
            contentodbContext.Tags.Add(tag);
            await contentodbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
