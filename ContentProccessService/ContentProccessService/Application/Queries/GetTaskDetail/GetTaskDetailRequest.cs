using ContentProccessService.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetTaskDetail
{
    public class GetTaskDetailRequest : IRequest<TasksViewModel>
    {
        public int IdTask { get; set; }
    }
}
