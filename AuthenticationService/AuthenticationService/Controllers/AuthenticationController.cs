
using System.Threading.Tasks;
using AuthenticationService.Application.Commands;
using AuthenticationService.Application.Commands.CreateCustomer;
using AuthenticationService.Application.Commands.UpdateCustomer;
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
        [HttpGet("Editors/Marketers/{id}")]
        
        public async Task<IActionResult> GetListEditor(int id)
        {
            var response = await Mediator.Send(new GetUserRequest { IdMarketer = id }) ;
            return Ok(response);

        }

        [HttpGet("Writers/Editors/{id}")]

        public async Task<IActionResult> GetListWriter(int id)
        {
            var response = await Mediator.Send(new GetWriterRequest {EditorId = id });
            return Ok(response);
        }

         [HttpGet("Customers/Marketers/{id}")]

        public async Task<IActionResult> GetListCustomer(int id)
        {
            var response = await Mediator.Send(new GetCustomerRequest { MarketerId = id });
            return Ok(response);
        }


        [HttpPost("Customers")]
        public async Task<IActionResult> CreateCustomerAccounts(CreateCustomerAccountCommads command)
        {
            var result = await Mediator.Send(command);
            return Accepted(result);

        }
        [HttpPut("Customers")]
        public async Task<IActionResult> UpdateCustomerAccounts(UpdateCustomerAccountCommads command)
        {
            var result = await Mediator.Send(command);
            if (result == null)
            {
                return BadRequest();
            }
            return Accepted(result);
        }
    }
}