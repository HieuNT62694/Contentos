using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetContentsByWriterId
{
    public class GetContentsByWriterIdValidator : AbstractValidator<GetContentsByWriterIdRequest>
    {
        public GetContentsByWriterIdValidator()
        {
            RuleFor(x => x.IdWriter).NotEmpty().WithMessage("IdWriter is requied");
        }

    }
}
