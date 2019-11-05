using BatchjobService.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BatchjobService.Application.Queries.GetFanpagesByMarketerId
{
    public class GetFanpagesByMarketerIdRequest : IRequest<List<FanpageViewModel>>
    {
        public int marketerId { get; set; }
        public int channelId { get; set; }
    }
}
