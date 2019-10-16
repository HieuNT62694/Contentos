using FluentValidation;


namespace ContentProccessService.Application.Commands.CreateTask
{
    public class CreateTaskValidator:AbstractValidator<CreateTaskRequest>
    {
        public CreateTaskValidator()
        {
            RuleFor(x => x.task.Description).NotEmpty().WithMessage("Desciption is not empty");
            RuleFor(x => x.task.IdCampaign).NotEmpty().WithMessage("Id Campaign is missing");
            RuleFor(x => x.task.IdWriter).NotEmpty().WithMessage("Id writer is missing");
        }
    }
}
