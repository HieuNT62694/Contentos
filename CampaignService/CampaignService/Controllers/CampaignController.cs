using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CampaignService.Application.Commands.CreateCampaign;
using CampaignService.Application.Commands.UpdateCampaign;
using CampaignService.Application.Queries.GetCampaign;
using CampaignService.Application.Queries.GetListCampaign;
using CampaignService.Application.Queries.GetListCampaignByEditorId;
using CampaignService.Application.Queries.GetListCampaignByMarketerId;
using CampaignService.Application.Queries.GetListCampaignByUserId;
using CampaignService.Entities;
using CampaignService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CampaignService.Controllers
{
    public class CampaignController : BaseController
    {
        //[Authorize(Roles = "Marketer")]
        public async Task<IActionResult> GetListCampaign()
        {
            var response = await Mediator.Send(new GetListCampaignRequest());
            if (response.Count() == 0)
            {
                return BadRequest("Don't have Campaign");
            }
            return Ok(response);
        }

        [HttpGet("campaigns/{id}")]
        //[Authorize(Roles = "Marketer")]
        public async Task<IActionResult> GetCampaignDetailAsync(int id)
        {
            var response = await Mediator.Send(new GetCampaignRequest { IdCampaign = id });
            return Ok(response);
        }

        [HttpGet("campaigns/customers/{id}")]
        //[Authorize(Roles = "Marketer")]
        public async Task<IActionResult> GetListCampaignByUserIdAsync(int id)
        {
            var response = await Mediator.Send(new GetListCampaignByUserIdRequest {IdCustomer   = id });
            if (response.Count() == 0)
            {
                return BadRequest("Don't have Campaign for Customer");
            }
            return Ok(response);
        }

        [HttpGet("campaigns/marketers/{id}")]
        //[Authorize(Roles = "Marketer")]
        public async Task<IActionResult> GetListCampaignByMarketerIdAsync(int id)
        {
            var response = await Mediator.Send(new GetCampaignByMarketerIdRequest { IdMarketer = id });
            if (response.Count() == 0)
            {
                return BadRequest("Don't have Campaign for Marketer");
            }
            return Ok(response);
        }

        [HttpPost("campaign")]
        //[Authorize(Roles = "Marketer")]
        public async Task<IActionResult> PostCampaignAsync(CreateCampaignCommand command)
        {
            var response= await Mediator.Send(command);
            return Accepted(response);
        }

        [HttpPut("campaign")]
        public async Task<IActionResult> PostCampaignAsync(UpdateCampaignCommand command)
        {
            var response = await Mediator.Send(command);
            return Accepted(response);
        }

        [HttpGet("campaigns/editor/{id}")]
        //[Authorize(Roles = "Marketer")]
        public async Task<IActionResult> GetListCampaignByEditorIdAsync(int id)
        {
            var response = await Mediator.Send(new GetCampaignByEditorIdRequest { IdEditor = id });
            return Ok(response);
        }
    }
}