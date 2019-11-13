using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Commands.CountClickContent
{
    public class CountClickContentCommands : IRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public List<int> Tags { get; set; }
    }
}
