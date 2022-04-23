using System.Threading.Tasks;
using EzDieter.Api.Helpers;
using EzDieter.Domain;
using EzDieter.Logic;
using EzDieter.Logic.UserServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EzDieter.Api.Controllers
{
    [Helpers.Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous2]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            var response = await _mediator.Send(new RegisterUserCommand.Command(username, password));
            if (response.Success) return Ok(response);
            if (response.AlreadyExist)
            {
                return Conflict(response);
            }

            return BadRequest(response);
        } 

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(string password)
        {
            var user = (User)HttpContext.Items["User"];
            var response = await _mediator.Send(new UpdateUserCommand.Command(user, password));
            return Ok(response);
        }
        
        [HttpGet]
        [Route("GetUserByToken")]
        public async Task<IActionResult> GetUserByToken()
        {
            var user = (User)HttpContext.Items["User"];
            return user == null ? NotFound() : Ok(user);
        }

        [AllowAnonymous2]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var response = await _mediator.Send(new AuthenticateUserQuery.Query(username, password));
            if (!response.Success)
            {
                return Unauthorized(response);
            }
            return Ok(response);
        }
    }
}