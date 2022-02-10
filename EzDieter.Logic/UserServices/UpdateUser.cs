using System;
using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Domain;
using MediatR;

namespace EzDieter.Logic.UserServices
{
    public static class UpdateUser
    {
        public record Command(User User, string Password) : IRequest<Response>;
        
        public class Handler : IRequestHandler<Command,Response>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = request.User;
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
                await _userRepository.Update(user);
                return new Response(user.Id);
            }
        }
        
        public record Response(Guid Id);
    }
}