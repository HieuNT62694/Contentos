using ContentProccessService.Entities;
using MediatR;


namespace ContentProccessService.Application.Commands.CreateTasksChannel
{
    public class CreateTaskChannelRequest : IRequest<TasksChannels>
    {
        public int IdTask { get; set; }
        public int IdChannel { get; set; }
    }
}
