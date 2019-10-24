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
        private readonly IUpdateBeforePublishingService _ubfPublish;
        public BatchJobController(IPublishFBService contentodbContext,IUpdateBeforePublishingService ubfPublish)
        {
            _context = contentodbContext;
            _ubfPublish = ubfPublish;
        }
        [HttpPost("Publish")]
        public IActionResult Index(PublishModels models)
        {
            _ubfPublish.UpdateStatusBeforePublishing(models.id, models.time);
            var jobId = BackgroundJob.Schedule(
                () => _context.PublishToFB(models.id),
                models.time);

            return Accepted();
        }
    }
}