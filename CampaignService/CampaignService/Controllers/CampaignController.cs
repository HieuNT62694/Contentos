using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampaignService.Application.Queries.GetCampaign;
using Microsoft.AspNetCore.Mvc;

namespace CampaignService.Controllers
{
    public class CampaignController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCampaignDetailAsync(int id)
        {
            var response = await Mediator.Send(new GetCampaignRequest { IdCampaign = id });
            return Ok(response);
        }
        //[HttpPost("Create")]
        //public async Task<IActionResult> PostCampaignAsync(CreateCampaignCommand command)
        //{
        //    await Mediator.Send(command);
        //    return Accepted("Create Successful !!");
        //}
    }
}