using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContentProccessService.Application.Queries.GetTags;
using Microsoft.AspNetCore.Mvc;

namespace ContentProccessService.Controllers
{
    public class ContentProcessController : BaseController
    {

        [HttpGet()]
        public async Task<IActionResult> GetListTagAsync()
        {
            var response = await Mediator.Send(new GetTagRequest());
            return Ok(response);
        }
    }
}