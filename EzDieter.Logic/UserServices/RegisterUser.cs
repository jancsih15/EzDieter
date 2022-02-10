using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Domain;
using MediatR;

namespace EzDieter.Logic.UserServices
{
    public static class RegisterUser
    {
        public record Command(string Username, string Password) : IRequest<Response>;

        public class Handler : IRequestHandler<Command,Response>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                //validation
                if (_userRepository.GetAll().Result.Any(x => x.Username == request.Username))
                    throw new Exception($"Username {request.Username} is already taken");
                
                
                
                var user = new User
                {
                    Id = new Guid(),
                    Username = request.Username,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
                };
                await _userRepository.Add(user);
                return new Response(user);
            }
        }
        
        public record Response(User User);
    }
}