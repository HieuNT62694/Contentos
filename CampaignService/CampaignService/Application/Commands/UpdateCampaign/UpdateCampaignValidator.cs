
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignService.Application.Commands.UpdateCampaign
{
    public class UpdateCampaignValidator : AbstractValidator<UpdateCampaignCommand>
    {
        public UpdateCampaignValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Please enter title");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Please enter title");
            RuleFor(x => x.IdCustomer).NotEmpty().WithMessage("Please chooser or add new customers");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter description");
            RuleFor(x => x.IdEditor).NotEmpty().WithMessage("Please choose Editor");
            RuleFor(x => x.EndDate).GreaterThan(DateTime.UtcNow).WithMessage("Please choose Enddate greater than date now");
        }
    }
}
