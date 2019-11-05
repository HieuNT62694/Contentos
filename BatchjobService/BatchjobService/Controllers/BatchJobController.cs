using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Controllers;
using BatchjobService.Application.Command.CreateFanpage;
using BatchjobService.Application.Command.UpdateFanpage;
using BatchjobService.Application.Queries.GetFanpages;
using BatchjobService.Application.Queries.GetFanpagesByCustomerId;
using BatchjobService.Application.Queries.GetFanpagesByMarketerId;
using BatchjobService.Entities;
using BatchjobService.HangFireService;
using BatchjobService.Models;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BatchjobService.Controllers
{
    public class BatchJobController : BaseController
    {
        private readonly IPublishFBService _publish;
        private readonly IUpdateBeforePublishingService _ubfPublish;
        private readonly ContentoDbContext _context;

        public BatchJobController(IPublishFBService publishFBService, IUpdateBeforePublishingService ubfPublish, ContentoDbContext contentodbContext)
        {
            _publish = publishFBService;
            _ubfPublish = ubfPublish;
            _context = contentodbContext;
        }

        [HttpPost("publish")]
        public IActionResult Index(PublishModels models)
        {
            _ubfPublish.UpdateStatusBeforePublishing(models.contentId, models.time);

            foreach(var item in models.listFanpage)
            {
                var fanpage = _context.Fanpages.Include(i => i.IdChannelNavigation).FirstOrDefault(f => f.Id == item).IdChannelNavigation.Id;
                switch (fanpage)
                {
                    case 2 :  PublishFB(item , models.contentId, models.time);
                        break;
                    case 3 :  PublishWP(item, models.contentId, models.time);
                        break;
                }
            }

            return Ok();
        }

        [HttpGet("fanpages")]
        public async Task<List<FanpageViewModel>> GetAllFanpageAsync()
        {
            var response = await Mediator.Send(new GetFanpagesRequest());

            return response;
        }

        [HttpGet("fanpages/customer/{channelId}/{customerId}")]
        public async Task<List<FanpageViewModel>> GetFanpageByCustomerIdAsync(int channelId, int customerId)
        {
            var response = await Mediator.Send(new GetFanpagesByCustomerIdRequest {customerId = customerId , channelId = channelId });

            return response;
        }

        [HttpGet("fanpages/marketer/{channelId}/{marketerId}")]
        public async Task<List<FanpageViewModel>> GetFanpageByMarketerIdAsync(int channelId, int marketerId)
        {
            var response = await Mediator.Send(new GetFanpagesByMarketerIdRequest { marketerId = marketerId, channelId = channelId });
            return response;
        }

        [HttpPost("fanpages")]
        public async Task<FanpageViewModel> CreateFanpageAsync(CreateFanpageCommand createFanpageCommand)
        {
            var response = await Mediator.Send(createFanpageCommand);
            return response;
        }

        [HttpPut("fanpages")]
        public async Task<FanpageViewModel> UpdateFanpageAsync(UpdateFanpageCommand updateFanpageCommand)
        {
            var response = await Mediator.Send(updateFanpageCommand);
            return response;
        }

        private void PublishFB(int fanpageId, int contentId, DateTime time)
        {
            var content = _context.Contents.FirstOrDefault(w => w.Id == contentId && w.IsActive == true);
            var task = _context.Tasks.FirstOrDefault(x => x.Id == content.IdTask);

            var taskFanpages = _context.TasksFanpages.FirstOrDefault(w => w.IdTask == task.Id && w.IdFanpage == fanpageId);
            

            var jobId = BackgroundJob.Schedule(
                () => _publish.PublishToFB(fanpageId, contentId),
                time);

            if (taskFanpages != null)
            {
                BackgroundJob.Delete(taskFanpages.IdJob);
                taskFanpages.IdJob = jobId;
                _context.TasksFanpages.Update(taskFanpages);
            }
            else
            {
                taskFanpages = new TasksFanpages();
                taskFanpages.IdFanpage = fanpageId;
                taskFanpages.IdTask = task.Id;
                taskFanpages.IdJob = jobId;
                _context.TasksFanpages.Add(taskFanpages);
            }
            _context.SaveChanges();
        }

        private void PublishWP(int fanpageId, int contentId, DateTime time)
        {
            var content = _context.Contents.FirstOrDefault(w => w.Id == contentId && w.IsActive == true);
            var task = _context.Tasks.FirstOrDefault(x => x.Id == content.IdTask);

            var taskFanpages = _context.TasksFanpages.FirstOrDefault(w => w.IdTask == task.Id && w.IdFanpage == fanpageId);


            var jobId = BackgroundJob.Schedule(
                () => _publish.PublishToWP(contentId),
                time);

            if (taskFanpages != null)
            {
                BackgroundJob.Delete(taskFanpages.IdJob);
                taskFanpages.IdJob = jobId;
                _context.TasksFanpages.Update(taskFanpages);
            }
            else
            {
                taskFanpages = new TasksFanpages();
                taskFanpages.IdFanpage = fanpageId;
                taskFanpages.IdTask = task.Id;
                taskFanpages.IdJob = jobId;
                _context.TasksFanpages.Add(taskFanpages);
            }
            _context.SaveChanges();
        }

        private async Task PublishContento(int fanpageId, int contentId, DateTime time)
        {
            var content = _context.Contents.FirstOrDefault(w => w.Id == contentId && w.IsActive == true);
            var task = _context.Tasks.FirstOrDefault(x => x.Id == content.IdTask);

            var taskFanpages = _context.TasksFanpages.FirstOrDefault(w => w.IdTask == task.Id && w.IdFanpage == fanpageId);

            var jobId = BackgroundJob.Schedule(
                () => _publish.PublishToContento(contentId),
                time);

            if (taskFanpages != null)
            {
                BackgroundJob.Delete(taskFanpages.IdJob);
                taskFanpages.IdJob = jobId;
                _context.TasksFanpages.Update(taskFanpages);
            }
            else
            {
                taskFanpages = new TasksFanpages();
                taskFanpages.IdFanpage = fanpageId;
                taskFanpages.IdTask = task.Id;
                taskFanpages.IdJob = jobId;
                _context.TasksFanpages.Add(taskFanpages);
            }
            await _context.SaveChangesAsync();
        }
    }
}