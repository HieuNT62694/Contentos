using ContentProccessService.Application.Models;
using ContentProccessService.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Commands.CreateTask
{
    public class CreateTaskRequest :IRequest<Tasks>
    {
        public CreateTaskModel task { get; set; }
    }
}
