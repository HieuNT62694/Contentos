using ContentProccessService.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetTags
{
    public class GetTagRequest : IRequest<IEnumerable<Tags>>
    {
    }
}
