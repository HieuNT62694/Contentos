using ContentProccessService.Application.Models;
using MediatR;

namespace ContentProccessService.Application.Queries.GetTaskDetail
{
    public class GetTaskDetailRequest : IRequest<TasksViewModel>
    {
        public int IdTask { get; set; }
    }
}
