using System;
using System.Collections.Generic;
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
                // Validation
                if (_userRepository.GetAll().Result.Any(x => x.Username == request.Username))
                    return new Response(null, $"The username {request.Username} is already taken!", false, true);
                if (request.Password.Length < 8)
                {
                    return new Response(null, "The given password is not long enough!", false, false);
                }
                if (!request.Password.Any(char.IsUpper))
                {
                    return new Response(null, "The given password doesn't contains an upper case letter!", false, false);
                }
                if (!request.Password.Any(char.IsLower))
                {
                    return new Response(null, "The given password doesn't contains a lower case letter!", false, false);
                }
                if (!request.Password.Any(char.IsDigit))
                {
                    return new Response(null, "The given password doesn't contains a number!", false, false);
                }
                
                
                // New user generation
                var user = new User
                {
                    Id = new Guid(),
                    Username = request.Username,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    TDEEs = 0
                };
                await _userRepository.Add(user);
                user.PasswordHash = "";
                return new Response(user, "Registration successful!", true, false);
            }
        }
        
        public record Response(User? User, string Message, bool Success, bool AlreadyExist);
    }
}