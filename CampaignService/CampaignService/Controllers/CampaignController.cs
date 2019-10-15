using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CampaignService.Application.Commands.CreateCampaign;
using CampaignService.Application.Commands.UpdateCampaign;
using CampaignService.Application.Queries.GetCampaign;
using CampaignService.Application.Queries.GetListCampaign;
using CampaignService.Application.Queries.GetListCampaignByMarketerId;
using CampaignService.Application.Queries.GetListCampaignByUserId;
using CampaignService.Entities;
using CampaignService.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CampaignService.Controllers
{
    public class CampaignController : BaseController
    {

        public async Task<IActionResult> GetListCampaign()
        {
            var response = await Mediator.Send(new GetListCampaignRequest());
            return Ok(response);
        }

        [HttpGet("campaigns/{id}")]
        public async Task<IActionResult> GetCampaignDetailAsync(int id)
        {
            var response = await Mediator.Send(new GetCampaignRequest { IdCampaign = id });
            return Ok(response);
        }

        [HttpGet("campaigns/customers/{id}")]
        public async Task<IActionResult> GetListCampaignByUserIdAsync(int id)
        {
            var response = await Mediator.Send(new GetListCampaignByUserIdRequest {IdCustomer   = id });
            return Ok(response);
        }

        [HttpGet("campaigns/marketers/{id}")]
        public async Task<IActionResult> GetListCampaignByMarketerIdAsync(int id)
        {
            var response = await Mediator.Send(new GetCampaignByMarketerIdRequest { IdMarketer = id });
            return Ok(response);
        }

        [HttpPost("campaign")]
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
    }
}