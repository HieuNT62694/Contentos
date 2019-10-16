using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailService.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MailService.Controllers
{
    public class MailController : BaseController
    {
        [HttpPost("mail")]
        public async Task<IActionResult> EmailSender(EmailSenderRequest queries)
        {
            var response = await Mediator.Send(queries);
            return Ok(response);
        }
    }
}