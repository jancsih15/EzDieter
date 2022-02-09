using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Domain;
using MediatR;

namespace EzDieter.Logic
{
    public static class GetAllUsers
    {
        //Query
        //All the data we need to execute
        public record Query() : IRequest<Response>;

        //Handler
        //All the business logic. Returns a response
        public class Handler : IRequestHandler<Query,Response>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                //All the business logic
                var users = await _userRepository.GetAll();
                return users == null ? null : new Response(users);
            }
        }
        
        //Response
        //The data we want to return
        public record Response(IEnumerable<User> Users);
    }
}