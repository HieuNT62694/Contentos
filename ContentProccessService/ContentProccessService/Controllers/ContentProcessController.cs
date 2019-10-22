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
using ContentProccessService.Application.Commands.CreateTasks;
using ContentProccessService.Application.Commands.CreateListTaskChannel;
using ContentProccessService.Entities;
using Microsoft.AspNetCore.Authorization;
using ContentProccessService.Application.Commands.ApproveRejectContent;
using ContentProccessService.Application.Commands.UpdatetTaskEditor;
using ContentProccessService.Application.Queries.GetTaskDetailUpdate;
using ContentProccessService.Application.Commands.DeleteTask;
using ContentProccessService.Application.Commands.SaveContent;
using ContentProccessService.Application.Commands.SubmitContent;
using ContentProccessService.Application.Commands.StartTask;

namespace ContentProccessService.Controllers
{
    public class ContentProcessController : BaseController
    {

        [HttpGet("tags")]
        [Authorize(Roles = "Marketer")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTag([FromBody]TagsDto dto)
        {
            var response = await Mediator.Send(new CreateTagRequest { dto = dto });
            return Ok(response);
        }

        [HttpGet("tags/campaign/{id}")]
        [Authorize(Roles = "Marketer,Editor")]
        public async Task<IActionResult> GetTagsByCampaignId(int id)
        {
            var response = await Mediator.Send(new GetTagsByCampaignIdRequest {CampaignId = id});
            return Ok(response);
        }
        [HttpGet("task/campaign/{id}")]
        [Authorize(Roles = "Marketer,Editor")]
        public async Task<IActionResult> GetTasksByCampaignId(int id)
        {
            var response = await Mediator.Send(new GetListTasksByCampaignIdRequest { IdCampaign = id });
            return Ok(response);
        }
        [HttpGet("task-detail/campaign/{id}")]
        [Authorize(Roles = "Marketer,Editor")]
        public async Task<IActionResult> GetTasksDetail(int id)
        {
            var response = await Mediator.Send(new GetTaskDetailRequest { IdTask = id });
            return Ok(response);
        }
        [HttpGet("task/marketer/{id}")]
        [Authorize(Roles = "Marketer")]
        public async Task<IActionResult> GetTasksByMarketerId(int id)
        {
            var response = await Mediator.Send(new GetListTaskByIdMarketerRequest { IdMartketer = id });
            return Ok(response);
        }

        [HttpGet("contents/editors/{id}")]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> GetContentByEditorId(int id)
        {
            var response = await Mediator.Send(new GetListContentByEditorIdRequest { Id = id });
            return Ok(response);
        }

        [HttpGet("task/editor/{id}")]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> GetTasksByEditorId(int id)
        {
            var response = await Mediator.Send(new GetTasksByEditorIdRequest { IdEditor = id });
            return Ok(response);
        }

        [HttpGet("content/writter/{id}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> GetContentsByWriterId(int id)
        {
            var response = await Mediator.Send(new GetContentsByWriterIdRequest { IdWriter = id });
            return Ok(response);
        }

        [HttpPost("taskschannels")]
        [Authorize(Roles = "Marketer")]
        public async Task<IActionResult> CreateTaskChannel([FromBody]TaskChannelModel taskchannel)
        {
            var response = await Mediator.Send(new CreateTaskChannelRequest { IdTask = taskchannel.IdTask, IdChannel = taskchannel.IdChannel });
            return Ok(response);
        }

        [HttpDelete("taskschannels/{id}")]
        [Authorize(Roles = "Marketer")]
        public async Task<IActionResult> UpdateTaskChannel(int id)
        {
            var response = await Mediator.Send(new UpdateTaskChannelRequest { IdTaskChannel = id });
            return Ok(response);
        }
        [HttpPost("task")]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskModel taskchannel)
        {
            var response = await Mediator.Send(new CreateTaskRequest { Task = taskchannel });
            return Accepted(response);
        }
        [HttpPost("tasks")]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> CreateTasks([FromBody] List<CreateTaskModel> taskchannel)
        {
            var response = await Mediator.Send(new CreateTasksRequest { tasks = taskchannel });
            return Ok(response);
        }
        [HttpPost("approvals")]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> ApprovalsContent(ApproveRejectContentRequest command)
        {
            await Mediator.Send(command);
            return Accepted("Successful!!");
        }
        [HttpPut("task")]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> UpdateTaskEditor(UpdateTaskEditorCommand command)
        {
            var response = await Mediator.Send(command);
            if (response == null)
            {
                return Conflict("Update Fail");
            }
            return Accepted(response);
        }
        [HttpGet("task-detail-update/campaign/{id}")]
        [Authorize(Roles = "Marketer,Editor")]
        public async Task<IActionResult> GetTaskUpdateDetail(int id)
        {
            var response = await Mediator.Send(new GetTaskDetailUpdateRequest { IdTask = id });
            return Ok(response);
        }
        [HttpDelete("task/campaign/{id}")]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var response = await Mediator.Send(new DeleteTaskRequest {IdTask = id });
            return Ok(response);
        }
        [HttpPut("content/task/campaign")]
        [Authorize(Roles = "Writter")]
        public async Task<IActionResult> SaveContent(SaveContentCommand command)
        {
            var response = await Mediator.Send(command);
            if (response == null)
            {
                return Conflict("Save Fail!!");
            }
            return Ok(response);
        }
        [HttpPut("content/task/campaign/submit")]
        [Authorize(Roles = "Writter")]
        public async Task<IActionResult> SubmitContent(SubmitContentCommand command)
        {
            await Mediator.Send(command);
            return Accepted("Successfull!!");
        }
        [HttpPut("content/task/campaign/start")]
        [Authorize(Roles = "Writter")]
        public async Task<IActionResult> StartTask(StartTaskCommand command)
        {
            var response = await Mediator.Send(command);
            return Accepted(response);
        }
    }
}