﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Commands.DeleteTask
{
    public class DeleteTaskRequest : IRequest
    {
        public int IdTask { get; set; }
    }
}
