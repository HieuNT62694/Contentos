using BatchjobService.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchjobService.Application.Command.UpdateFanpage
{
    public class UpdateFanpageCommand : IRequest<FanpageViewModel>
    {
        public int fanpageId { get; set; } = 0;
        public int channelId { get; set; } = 0;
        public int customerId { get; set; } = 0;
        public string name { get; set; }
        public string token { get; set; }
    }
}
