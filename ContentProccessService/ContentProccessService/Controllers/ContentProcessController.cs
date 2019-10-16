using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContentProccessService.Application.Dtos;
using ContentProccessService.Application.Queries.GetTags;
using Microsoft.AspNetCore.Mvc;
using ContentProccessService.Application.Commands.CreateTag;
using ContentProccessService.Application.Queries.GetTagsByCampaignId;
using ContentProccessService.Application.Queries.GetListTaksByCampaignId;
using ContentProccessService.Application.Queries.GetTaskDetail;
using ContentProccessService.Application.Queries.GetListTaskByIdMarketer;
using ContentProccessService.Application.Queries.GetContentByEditorId;


namespace ContentProccessService.Controllers
{
    public class ContentProcessController : BaseController
    {

        [HttpGet("tags")]
        public async Task<IActionResult> GetListTagAsync()
        {
            var response = await Mediator.Send(new GetTagRequest());
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
    }
}