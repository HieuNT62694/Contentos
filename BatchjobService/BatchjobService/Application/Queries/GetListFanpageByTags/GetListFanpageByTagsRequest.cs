using BatchjobService.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchjobService.Application.Queries.GetListFanpageByTags
{
    public class GetListFanpageByTagsRequest : IRequest<List<FanpageViewModel>>
    {
        public List<int> lstTags { get; set; }
    }
}
