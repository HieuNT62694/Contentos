using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BatchjobService.HangFireService;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace BatchjobService.Controllers
{
    [Route("api/hangfire")]
    [ApiController]
    public class HangfireController : Controller
    {
        private IUpdateStatusService service;
        public HangfireController(IUpdateStatusService service)
        {
            this.service = service;
        }
        [HttpGet]
        public IActionResult Index()
        {
            RecurringJob.AddOrUpdate("UpdateStatusDeadline", () => service.UpdateStatus(), "0 1 * * *",TimeZoneInfo.Utc);
            //RecurringJob.AddOrUpdate("UpdateStatusDeadline", () => service.UpdateStatus(), Cron.Minutely);
            return Ok();
        }
    }
}