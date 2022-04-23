using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Domain;
using MediatR;
using BCryptNet = BCrypt.Net.BCrypt;

namespace EzDieter.Logic.UserServices
{
    public static class UpdateUserCommand
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

                // Validation of new password
                if (request.Password.Length < 8)
                {
                    return new Response(user.Id, "The given password is not long enough!");
                }
                if (!request.Password.Any(char.IsUpper))
                {
                    return new Response(user.Id, "The given password doesn't contains an upper case letter!");
                }
                if (!request.Password.Any(char.IsLower))
                {
                    return new Response(user.Id, "The given password doesn't contains a lower case letter!");
                }
                if (!request.Password.Any(char.IsDigit))
                {
                    return new Response(user.Id, "The given password doesn't contains a number!");
                }
                if (BCryptNet.Verify(request.Password, user.PasswordHash))
                {
                    return new Response(user.Id, "The new password is the same as the old one!");
                }
                
                // new password hashing
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
                // updating database
                await _userRepository.Update(user);
                // sending response
                return new Response(user.Id, "Password successfully changed!");
            }
        }
        
        public record Response(Guid Id, string Message);
    }
}