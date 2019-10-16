using ContentProccessService.Entities;
using MediatR;


namespace ContentProccessService.Application.Commands.UpdateTaskChannel
{
    public class UpdateTaskChannelRequest : IRequest<TasksChannels>
    {
        public int IdTaskChannel { get; set; }
    }
}
