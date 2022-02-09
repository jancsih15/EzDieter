using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EzDieter.Api.Helpers;
using EzDieter.Domain;
using EzDieter.Logic;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EzDieter.Api.Controllers
{
    [Helpers.Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private ISender _sender;
        private readonly IMediator _mediator;

        public UserController(ISender sender, IMediator mediator)
        {
            _sender = sender;
            _mediator = mediator;
        }
        
        [AllowAnonymous2]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<User>> GetAll()
        {
            //var user = (User)HttpContext.Items["User"]!;
            return await _sender.Send(new GetUsersQuery());
        }

        [AllowAnonymous2]
        [HttpGet]
        [Route("GetAll2")]
        public async Task<IActionResult> GetAll2()
        {
            var response = await _mediator.Send(new GetAllUsers.Query());
            return response == null ? NotFound() : Ok(response);
        }

        [AllowAnonymous2]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(string username, string password) => 
            Ok(await _mediator.Send(new RegisterUser.Command(username, password)));

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(string password)
        {
            var user = (User)HttpContext.Items["User"];
            return Ok(await _mediator.Send(new UpdateUser.Command(user, password)));
        }
        
        [AllowAnonymous2]
        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetUserById.Query(id));
            return response == null ? NotFound() : Ok(response);
        }
        
        
        [HttpGet]
        [Route("GetByIdAuth")]
        public async Task<IActionResult> GetByIdAuth()
        {
            var user = (User)HttpContext.Items["User"];
            return user == null ? NotFound() : Ok(user);
        }

        [AllowAnonymous2]
        [HttpPost]
        [Route("auth")]
        public async Task<IActionResult> Authenticate(string username, string password)
        {
            var response = await _mediator.Send(new AuthenticateUser.Query(username, password));
            return Ok(response);
        }
    }
}