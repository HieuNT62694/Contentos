using ContentProccessService.Models;
using MediatR;
using System.Collections.Generic;

namespace ContentProccessService.Application.Queries.GetContentsByWriterId
{
    public class GetContentsByWriterIdRequest : IRequest<List<ContentsViewModel>>
    {
        public int  IdWriter{ get; set; }
    }
}
