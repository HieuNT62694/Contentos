using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Application.Commands.Notify
{
    public class NotifyCommands : IRequest<bool>
    {
     
        [Required]
        public string To { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }


    }
}
