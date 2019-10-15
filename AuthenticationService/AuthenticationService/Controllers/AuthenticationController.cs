
using System.Threading.Tasks;
using AuthenticationService.Application.Commands;
using AuthenticationService.Application.Commands.CreateCustomer;
using AuthenticationService.Application.Queries;
using AuthenticationService.Application.Queries.GetCustomer;
using AuthenticationService.Application.Queries.GetUser;
using AuthenticationService.Application.Queries.GetWriter;
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
        [HttpGet("List-Editor/{id}")]
        
        public async Task<IActionResult> GetListEditor(int id)
        {
            var response = await Mediator.Send(new GetUserRequest { IdMarketer = id }) ;
            return Ok(response);

        }

        [HttpGet("List-Writer/{id}")]

        public async Task<IActionResult> GetListWriter(int id)
        {
            var response = await Mediator.Send(new GetUserRequest {IdMarketer = id });
            return Ok(response);

        }

         [HttpGet("List-Customer/{id}")]

        public async Task<IActionResult> GetListCustomer(int id)
        {
            var response = await Mediator.Send(new GetCustomerRequest { MarketerId = id });
            return Ok(response);

        }

        [HttpPost("customer-account")]
        public async Task<IActionResult> CreateCustomerAccounts(CreateCustomerAccountCommads command)
        {
            var result = await Mediator.Send(command);
            return Accepted(result);

        }
    }
}