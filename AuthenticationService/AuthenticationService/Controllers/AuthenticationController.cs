
using System.Threading.Tasks;
using AuthenticationService.Application.Commands;
using AuthenticationService.Application.Queries;
using AuthenticationService.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    public class AuthenticationController : BaseController
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthenticationRequest queries)
        {
            var response = await Mediator.Send(queries);
            return Ok(response);
        }
        [HttpPost("Register")]
        public async Task<object> Register( RegisterAccountCommands command)
        {
            await Mediator.Send(command);
            return Accepted("Create Successful !!");

        }
    }
}