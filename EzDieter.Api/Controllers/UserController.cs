using System.Collections.Generic;
using System.Threading.Tasks;
using EzDieter.Domain;
using EzDieter.Logic;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EzDieter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<User>> GetAll()
        {
            //var user = (User)HttpContext.Items["User"]!;
            return await _sender.Send(new GetUsersQuery());
        }
        
    }
}