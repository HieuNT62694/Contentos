using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContentProccessService.Application.Dtos;
using ContentProccessService.Application.Queries.GetTags;
using Microsoft.AspNetCore.Mvc;
using ContentProccessService.Application.Commands.CreateTag;
using ContentProccessService.Application.Commands.CreateTask;
using ContentProccessService.Application.Commands.CreateTasksChannel;
using ContentProccessService.Application.Commands.UpdateTaskChannel;
using ContentProccessService.Application.Queries.GetTagsByCampaignId;
using ContentProccessService.Application.Queries.GetListTaksByCampaignId;
using ContentProccessService.Application.Queries.GetTaskDetail;
using ContentProccessService.Application.Queries.GetListTaskByIdMarketer;
using ContentProccessService.Application.Queries.GetContentByEditorId;
using ContentProccessService.Application.Queries.GetTasksByEditorId;
using ContentProccessService.Application.Queries.GetContentsByWriterId;
using ContentProccessService.Models;
using ContentProccessService.Application.Models;


namespace ContentProccessService.Controllers
{
    public class ContentProcessController : BaseController
    {

        [HttpGet("tags")]
        public async Task<IActionResult> GetListTagAsync()
        {
            var response = await Mediator.Send(new GetTagRequest());
            if (response.Count() == 0)
            {
                return BadRequest("Don't have tags");
            }
            return Ok(response);
        }

        [HttpPost("tags")]
        public async Task<IActionResult> CreateTag([FromBody]TagsDto dto)
        {
            var response = await Mediator.Send(new CreateTagRequest { dto = dto });
            return Ok(response);
        }

        [HttpGet("tags/campaign/{id}")]
        public async Task<IActionResult> GetTagsByCampaignId(int id)
        {
            var response = await Mediator.Send(new GetTagsByCampaignIdRequest {CampaignId = id});
            return Ok(response);
        }
        [HttpGet("task/campaign/{id}")]
        public async Task<IActionResult> GetTasksByCampaignId(int id)
        {
            var response = await Mediator.Send(new GetListTasksByCampaignIdRequest { IdCampaign = id });
            return Ok(response);
        }
        [HttpGet("task-detail/campaign/{id}")]
        public async Task<IActionResult> GetTasksDetail(int id)
        {
            var response = await Mediator.Send(new GetTaskDetailRequest { IdTask = id });
            return Ok(response);
        }
        [HttpGet("task/marketer/{id}")]
        public async Task<IActionResult> GetTasksByMarketerId(int id)
        {
            var response = await Mediator.Send(new GetListTaskByIdMarketerRequest { IdMartketer = id });
            return Ok(response);
        }

        [HttpGet("contents/editors/{id}")]
        public async Task<IActionResult> GetContentByEditorId(int id)
        {
            var response = await Mediator.Send(new GetListContentByEditorIdRequest { Id = id });
            return Ok(response);
        }

        [HttpGet("task/editor/{id}")]
        public async Task<IActionResult> GetTasksByEditorId(int id)
        {
            var response = await Mediator.Send(new GetTasksByEditorIdRequest { IdEditor = id });
            return Ok(response);
        }

        [HttpGet("content/writter/{id}")]
        public async Task<IActionResult> GetContentsByWriterId(int id)
        {
            var response = await Mediator.Send(new GetContentsByWriterIdRequest { IdWriter = id });
            return Ok(response);
        }

        [HttpPost("taskschannels")]
        public async Task<IActionResult> CreateTaskChannel([FromBody]TaskChannelModel taskchannel)
        {
            var response = await Mediator.Send(new CreateTaskChannelRequest { IdTask = taskchannel.IdTask, IdChannel = taskchannel.IdChannel });
            return Ok(response);
        }

        [HttpDelete("taskschannels/{id}")]
        public async Task<IActionResult> UpdateTaskChannel(int id)
        {
            var response = await Mediator.Send(new UpdateTaskChannelRequest { IdTaskChannel = id });
            return Ok(response);
        }
        [HttpPost("task")]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskModel taskchannel)
        {
            var response = await Mediator.Send(new CreateTaskRequest { Task = taskchannel });
            return Ok(response);
        }
    }
}