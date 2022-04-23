using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Domain;
using MediatR;
using BCryptNet = BCrypt.Net.BCrypt;

namespace EzDieter.Logic.UserServices
{
    public static class AuthenticateUserQuery
    {
        public record Query(string Username, string Password) : IRequest<Response>;
        
        public class Handler : IRequestHandler<Query,Response>
        {
            private readonly IUserRepository _userRepository;
            private readonly IJwtUtils _jwtUtils;

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
                    return new Response(null, "", "Username or password is incorrect!", false);
                
                //generate jwt token
                var jwtToken = _jwtUtils.GenerateJwtToken(user);

                user.PasswordHash = "";
                return new Response(user, jwtToken, "Login Successful", true);
            }
        }

        public record Response(User? User, string JwtToken, string Message, bool Success);
    }
}