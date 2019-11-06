using BatchjobService.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchjobService.Application.Queries.GetFanpagesByCustomerIdAndChannelId
{
    public class GetFanpagesByCustomerIdAndChannelIdRequest : IRequest<List<FanpageViewModel>>
    {
        public int customerId { get; set; }
        public int channelId { get; set; }
    }
}
