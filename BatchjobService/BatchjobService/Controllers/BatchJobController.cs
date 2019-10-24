using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Controllers;
using BatchjobService.HangFireService;
using BatchjobService.Models;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace BatchjobService.Controllers
{
    public class BatchJobController : BaseController
    {
        private readonly IPublishFBService _context;
        public BatchJobController(IPublishFBService contentodbContext)
        {
            _context = contentodbContext;
        }
        [HttpPost("Publish")]
        public IActionResult Index(PublishModels models)
        {
            var jobId = BackgroundJob.Schedule(
                () => _context.PublishToFB(20),
                models.time);

            return Accepted();
        }
    }
}