using System;
using System.IdentityModel.Tokens.Jwt;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Domain;
using MediatR;
using EzDieter.Api.Helpers;

namespace EzDieter.Logic
{
    public static class AuthenticateUser
    {
        public record Query(string Username, string Password) : IRequest<Response>;
        
        public class Handler : IRequestHandler<Query,Response>
        {
            private readonly IUserRepository _userRepository;
            private IJwtUtils _jwtUtils;

            public Handler(IUserRepository userRepository, IJwtUtils jwtUtils)
            {
                _userRepository = userRepository;
                _jwtUtils = jwtUtils;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var users = await _userRepository.GetAll();
                var user = users.SingleOrDefault(x => x.Username == request.Username);
                
                //validation
                if (user is null || !BCryptNet.Verify(request.Password, user.PasswordHash))
                    //TODO create own middleware for exceptions
                    throw new Exception("Username or password is incorrect");
                //generate jwt token
                var jwtToken = _jwtUtils.GenerateJwtToken(user);

                return new Response(user, jwtToken);
            }
        }

        public record Response(User User, string JwtToken);
    }
}