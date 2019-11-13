﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Controllers;
using BatchjobService.Application.Command.CheckToken;
using BatchjobService.Application.Command.CreateFanpage;
using BatchjobService.Application.Command.DeleteFanpage;
using BatchjobService.Application.Command.UpdateFanpage;
using BatchjobService.Application.Queries.GetContentByFanpageId;
using BatchjobService.Application.Queries.GetDetailFanpage;
using BatchjobService.Application.Queries.GetFanpages;
using BatchjobService.Application.Queries.GetFanpagesByCustomerId;
using BatchjobService.Application.Queries.GetFanpagesByCustomerIdAndChannelId;
using BatchjobService.Application.Queries.GetFanpagesByMarketerId;
using BatchjobService.Application.Queries.GetListFanpageByTags;
using BatchjobService.Application.Queries.GetTaskFanpageByContentId;
using BatchjobService.Entities;
using BatchjobService.HangFireService;
using BatchjobService.Models;
using BatchjobService.Utulity;
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
            var content = _context.Contents.FirstOrDefault(w => w.Id == models.contentId && w.IsActive == true);
            var task = _context.Tasks.FirstOrDefault(x => x.Id == content.IdTask);     
            if(task.Status == 7)
            {
                return Conflict();
            }
            var fanpages = _context.Fanpages.Include(i => i.IdChannelNavigation).ToList();

            _ubfPublish.UpdateStatusBeforePublishing(task.Id, models.time, models.listTag);

            foreach (var item in models.listFanpage)
            {
                var fanpage = fanpages.FirstOrDefault(f => f.Id == item).IdChannelNavigation.Id;
                switch (fanpage)
                {
                    case 1 :  PublishContento(item, models.contentId, models.time, task.Id);
                        break;
                    case 2 :  PublishFB(item , models.contentId, models.time, task.Id);
                        break;
                    case 3 :  PublishWP(item, models.contentId, models.time, task.Id);
                        break;
                }
            }

            return Accepted();
        }

        [HttpGet("fanpages/{id}")]
        public async Task<List<FanpageViewModel>> GetAllFanpageAsync(int id)
        {
            var response = await Mediator.Send(new GetFanpagesRequest {Id = id});

            return response;
        }

        [HttpGet("fanpage-detail/{id}")]
        public async Task<EditViewModel> GetDetailFanpageAsync(int id)
        {
            var response = await Mediator.Send(new GetDetailFanpageRequest { Id = id });

            return response;
        }

        [HttpPost("fanpage/tags")]
        public async Task<Dictionary<string, List<int>>> GetListFanpageByTagsAsync(List<int> id)
        {
            var response = await Mediator.Send(new GetListFanpageByTagsRequest { lstTags = id });

            return response;
        }

        [HttpGet("fanpages/customer/{channelId}/{customerId}")]
        public async Task<List<FanpageViewModel>> GetFanpageByCustomerIdAndChannelIdAsync(int channelId, int customerId)
        {
            var response = await Mediator.Send(new GetFanpagesByCustomerIdAndChannelIdRequest { CustomerId = customerId , ChannelId = channelId });

            return response;
        }

        [HttpGet("fanpages/customer/{customerId}")]
        public async Task<List<FanpageViewModel>> GetFanpageByCustomerIdAsync(int customerId)
        {
            var response = await Mediator.Send(new GetFanpagesByCustomerIdRequest { CustomerId = customerId});

            return response;
        }

        [HttpGet("fanpages/marketer/{channelId}/{marketerId}")]
        public async Task<List<FanpageViewModel>> GetFanpageByMarketerIdAsync(int channelId, int marketerId)
        {
            var response = await Mediator.Send(new GetFanpagesByMarketerIdRequest { MarketerId = marketerId, ChannelId = channelId });
            return response;
        }

        [HttpGet("contents/fanpages/{id}")]
        public async Task<List<ContentModel>> GetContentByFanpageIdAsync(int id)
        {
            var response = await Mediator.Send(new GetContentByFanpageIdRequest { id = id });
            return response;
        }

        [HttpPost("fanpages")]
        public async Task<IActionResult> CreateFanpageAsync(CreateFanpageCommand createFanpageCommand)
        {
            string check = "";
            switch (createFanpageCommand.ChannelId)
            {
                case 2: check = await Helper.FBTokenValidate(createFanpageCommand.Token);
                    break;
                case 3: check = await Helper.WPTokenValidate(createFanpageCommand.Token);
                    break;
            }
            if (check == "")
            {
                return Conflict();
            }
            createFanpageCommand.Link = check;
            var response = await Mediator.Send(createFanpageCommand);
            return Accepted(response);
        }

        [HttpPost("fanpages/token")]
        public async Task<IActionResult> CheckTokenAsync(CheckToken checkToken)
        {
            string check = "";
            switch (checkToken.ChannelId)
            {
                case 2:
                    check = await Helper.FBTokenValidate(checkToken.Token);
                    break;
                case 3:
                    check = await Helper.WPTokenValidate(checkToken.Token);
                    break;
            }
            if (check == "")
            {
                return Conflict();
            }
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("link", check);
            return Accepted(values);
        }

        [HttpPut("fanpages")]
        public async Task<IActionResult> UpdateFanpageAsync(UpdateFanpageCommand updateFanpageCommand)
        {
            string check = "";
            switch (updateFanpageCommand.ChannelId)
            {
                case 2:
                    check = await Helper.FBTokenValidate(updateFanpageCommand.Token);
                    break;
                case 3:
                    check = await Helper.WPTokenValidate(updateFanpageCommand.Token);
                    break;
            }
            if (check == "")
            {
                return Conflict();
            }
            updateFanpageCommand.Link = check;
            var response = await Mediator.Send(updateFanpageCommand);
            return Accepted(response);
        }

        [HttpDelete("fanpages/{id}")]
        public async Task<IActionResult> DeleteFanpageAsync(int id)
        {
            try
            {
                var response = await Mediator.Send(new DeleteFanpageCommand {id = id});
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet("taskfanpages/content/{id}")]
        public async Task<IActionResult> GetTaskFanpagesAsync(int id)
        {
            var response = await Mediator.Send(new GetTaskFanpageByContentIdRequest { Id = id });
            return Ok(response);
        }

        private void PublishFB(int fanpageId, int contentId, DateTime time, int taskId)
        {
            var jobId = BackgroundJob.Schedule(
                () => _publish.PublishToFB(fanpageId, contentId),
                time);

            var taskFanpages = new TasksFanpages();
            taskFanpages.IdFanpage = fanpageId;
            taskFanpages.IdTask = taskId;
            taskFanpages.IdJob = jobId;
            _context.TasksFanpages.Add(taskFanpages);
            _context.SaveChanges();
        }

        private void PublishWP(int fanpageId, int contentId, DateTime time, int taskId)
        {
            var jobId = BackgroundJob.Schedule(
                () => _publish.PublishToWP(fanpageId, contentId),
                time);

            var taskFanpages = new TasksFanpages();
            taskFanpages.IdFanpage = fanpageId;
            taskFanpages.IdTask = taskId;
            taskFanpages.IdJob = jobId;
            _context.TasksFanpages.Add(taskFanpages);
            _context.SaveChanges();
        }

        private void PublishContento(int fanpageId, int contentId, DateTime time, int taskId)
        {
            var jobId = BackgroundJob.Schedule(
                () => _publish.PublishToContento(taskId),
                time);

            var taskFanpages = new TasksFanpages();
            taskFanpages.IdFanpage = fanpageId;
            taskFanpages.IdTask = taskId;
            taskFanpages.IdJob = jobId;
            _context.TasksFanpages.Add(taskFanpages);
            _context.SaveChanges();
        }
    }
}