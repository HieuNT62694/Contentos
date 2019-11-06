
using System.Threading.Tasks;
using AuthenticationService.Application.Commands;
using AuthenticationService.Application.Commands.ChangePassword;
using AuthenticationService.Application.Commands.CreateCustomer;
using AuthenticationService.Application.Commands.Notify;
using AuthenticationService.Application.Commands.UpdateCustomer;
using AuthenticationService.Application.Commands.UpdateProfile;
using AuthenticationService.Application.Queries;
using AuthenticationService.Application.Queries.GetAllWriterByIdMarketer;
using AuthenticationService.Application.Queries.GetCustomer;
using AuthenticationService.Application.Queries.GetCustomerByIdEditor;
using AuthenticationService.Application.Queries.GetcustomerDetail;
using AuthenticationService.Application.Queries.GetProfile;
using AuthenticationService.Application.Queries.GetUser;
using AuthenticationService.Application.Queries.GetWriter;
using AuthenticationService.Entities;
using AuthenticationService.Models;
using AuthenticationService.RabbitMQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AuthenticationService.Controllers
{
    public class AuthenticationController : BaseController
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthenticationRequest queries)
        {
            var response = await Mediator.Send(queries);
            if (response == null)
            {
                return BadRequest("Invalid User Name of Password");
            }
            return Ok(response);
        }
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommands commands)
        {
            var response = await Mediator.Send(commands);
            if (response == false)
            {
                return BadRequest("Change Password Fail");
            }
            return Accepted("Change Success ");
        }
        [HttpPost("Register")]
        ////[Authorize(Roles = "Guest")]
        public async Task<object> Register(RegisterAccountCommands command)
        {
            await Mediator.Send(command);
            return Accepted("Create Successful !!");

        }
        [HttpGet("Editors/Marketers/{id}")]
       [Authorize(Roles = "Marketer")]
        public async Task<IActionResult> GetListEditor(int id)
        {
            var response = await Mediator.Send(new GetUserRequest { IdMarketer = id });
            if (response.Count == 0)
            {
                return BadRequest("Don't have Editor For Marketer");
            }
            return Ok(response);

        }

        [HttpGet("Writers/Editors/{id}")]
       [Authorize(Roles = "Marketer,Editor")]
        public async Task<IActionResult> GetListWriter(int id)
        {
            var response = await Mediator.Send(new GetWriterRequest { EditorId = id });
            if (response.Count == 0)
            {
                return BadRequest("Don't have Writter For Marketer");
            }
            return Ok(response);
        }

        [HttpGet("Customers/Marketers-Basic/{id}/")]
       [Authorize(Roles = "Marketer,Editor")]
        public async Task<IActionResult> GetListCustomerBasic(int id)
        {
            var response = await Mediator.Send(new GetCustomerBasicRequest { MarketerId = id });
            return Ok(response);
        }

        [HttpGet("Customers/Marketers/{id}")]
       [Authorize(Roles = "Marketer")]
        public async Task<IActionResult> GetListCustomer(int id)
        {
            var response = await Mediator.Send(new GetCustomerRequest { MarketerId = id });
            if (response.Count == 0)
            {
                return BadRequest("Don't have Customer For Marketer");
            }
            return Ok(response);
        }

        [HttpPost("Customers")]
       [Authorize(Roles = "Marketer")]
        public async Task<IActionResult> CreateCustomerAccounts(CreateCustomerAccountCommads command)
        {
            var result = await Mediator.Send(command);
            //Create exchange
            //Producer producer = new Producer();
            //MessageAccountDTO messageDTO = new MessageAccountDTO{
            //   FullName = result.FullName,Password = result.Password,Email = result.Email };
            //producer.PublishMessage(message: JsonConvert.SerializeObject(messageDTO), "AccountToEmail");]
            if (result == null)
            {
                return BadRequest("Duplicate Email !!");
            }
            return Accepted(result);

        }
        [HttpPut("Customers")]
       [Authorize(Roles = "Marketer")]
        public async Task<IActionResult> UpdateCustomerAccounts(UpdateCustomerAccountCommads command)
        {
            var result = await Mediator.Send(command);
            if (result == null)
            {
                return BadRequest("Update fail");
            }
            return Accepted(result);
        }
		[HttpPost("send-notify")]
        public async Task<IActionResult> Notify (NotifyCommands Command) 
        { 
            var result = await Mediator.Send(Command);
            return Accepted(result);
        }
        [HttpGet("Writer/Marketers/{id}")]
        ////[Authorize(Roles = "Marketer")]
        public async Task<IActionResult> GetListWriterByIdMarketer(int id)
        {
            var response = await Mediator.Send(new GetAllWriterByIdMarketerRequest { MarketerId = id });
            if (response.Count == 0)
            {
                return BadRequest("Don't have Editor For Marketer");
            }
            return Ok(response);
        }
        [HttpGet("customer/editor/{id}")]
        ////[Authorize(Roles = "Marketer")]
        public async Task<IActionResult> GetcustomerByIdEditor(int id)
        {
            var response = await Mediator.Send(new GetCustomerByIdEditorRequest { EditorId = id });
            if (response.Count == 0)
            {
                return BadRequest("Don't have Editor For Marketer");
            }
            return Ok(response);

        }
        [HttpGet("customer-detail/{id}")]
        ////[Authorize(Roles = "Marketer")]
        public async Task<IActionResult> GetcustomerDetail(int id)
        {
            var response = await Mediator.Send(new GetCustomerDetailRequest { IdCustomer = id });
            if (response == null)
            {
                return BadRequest("Don't have Account");
            }
            return Ok(response);

        }
        [HttpGet("profile/{id}")]
        ////[Authorize(Roles = "Marketer")]
        public async Task<IActionResult> GetProfileUser(int id)
        {
            var response = await Mediator.Send(new GetProfileRequest { IdUser = id });
            if (response == null)
            {
                return BadRequest("Don't have Account");
            }
            return Ok(response);

        }
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfileUser(UpdateProfileCommands command)
        {
            var result = await Mediator.Send(command);
            if (result == null)
            {
                return BadRequest("Create Fail");
            }
            return Accepted(result);
        }
    }
}