﻿using System;
using System.Linq;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Logic;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace EzDieter.Api.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IJwtUtils jwtUtils, IMediator mediator)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                Guid? userId = jwtUtils.ValidateJwtToken(token);
                if (userId != null)
                {
                    // attach user to context on successful jwt validation
                    var response = await mediator.Send(new GetUserById.Query(userId));
                    var user = response.User;
                    context.Items["User"] = user;
                }
            }

            await _next(context);
        }
    }
}