using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampaignService.Application.Commands.CreateCampaign;
using CampaignService.Application.Commands.UpdateCampaign;
using CampaignService.Application.Queries.GetCampaign;
using CampaignService.Application.Queries.GetListCampaign;
using CampaignService.Application.Queries.GetListCampaignByUserId;
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

        [HttpGet("campaign-detail")]
        public async Task<IActionResult> GetCampaignDetailAsync(int id)
        {
            var response = await Mediator.Send(new GetCampaignRequest { IdCampaign = id });
            return Ok(response);
        }

        [HttpGet("list-campaign-by-user-id")]
        public async Task<IActionResult> GetListCampaignByUserIdAsync(int idCustomer)
        {
            var response = await Mediator.Send(new GetListCampaignByUserIdRequest {IdCustomer   = idCustomer });
            return Ok(response);
        }

        [HttpPost("campaign")]
        public async Task<IActionResult> PostCampaignAsync(CreateCampaignCommand command)
        {
            await Mediator.Send(command);
            return Accepted("Create Successful !!");
        }

        [HttpPut("campaign")]
        public async Task<IActionResult> PostCampaignAsync(UpdateCampaignCommand command)
        {
            await Mediator.Send(command);
            return Accepted("Update Successful !!");
        }
    }
}