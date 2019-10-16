using ContentProccessService.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetContentByEditorId
{
    public class GetListContentByEditorIdRequest : IRequest<List<ContentViewModel>>
    {
        [Required]
        public int Id { get; set; }
    }
}
