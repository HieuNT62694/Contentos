using MediatR;
using System.ComponentModel.DataAnnotations;

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
