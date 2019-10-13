
using System.Threading.Tasks;
using AuthenticationService.Application.Commands;
using AuthenticationService.Application.Commands.CreateCustomer;
using AuthenticationService.Application.Queries;
using AuthenticationService.Application.Queries.GetUser;
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
        public async Task<object> Register(RegisterAccountCommands command)
        {
            await Mediator.Send(command);
            return Accepted("Create Successful !!");

        }
        [HttpGet("List-Editor")]
        
        public async Task<IActionResult> GetListEditor()
        {
            var response = await Mediator.Send(new GetUserRequest());
            return Ok(response);

        }
        [HttpPost("customer-account")]
        public async Task<object> CreateCustomerAccounts(CreateCustomerAccountCommads command)
        {
            await Mediator.Send(command);
            return Accepted("Create Successful !!");

        }
    }
}