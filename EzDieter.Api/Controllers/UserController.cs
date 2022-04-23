using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EzDieter.Api.Helpers;
using EzDieter.Domain;
using EzDieter.Logic;
using EzDieter.Logic.UserServices;
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
        private readonly IMediator _mediator;

        public UserController(ISender sender, IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // [AllowAnonymous2]
        // [HttpGet]
        // [Route("GetAll")]
        // public async Task<IActionResult> GetAll()
        // {
        //     var response = await _mediator.Send(new GetAllUsers.Query());
        //     return Ok(response);
        // }

        [AllowAnonymous2]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            var response = await _mediator.Send(new RegisterUser.Command(username, password));
            if (!response.Success)
            {
                if (response.AlreadyExist)
                {
                    return Conflict(response);
                }

                return BadRequest(response);
            }
            return Ok(response);
        } 

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(string password)
        {
            var user = (User)HttpContext.Items["User"];
            var response = await _mediator.Send(new UpdateUser.Command(user, password));
            return Ok(response);
        }
        
        // [AllowAnonymous2]
        // [HttpGet]
        // [Route("GetById/{id}")]
        // public async Task<IActionResult> GetById(Guid id)
        // {
        //     var response = await _mediator.Send(new GetUserById.Query(id));
        //     return response == null ? NotFound() : Ok(response);
        // }
        
        
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
            var response = await _mediator.Send(new AuthenticateUser.Query(username, password));
            if (!response.Success)
            {
                return Unauthorized(response);
            }
            return Ok(response);
        }
    }
}