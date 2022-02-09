using System;
using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Domain;
using MediatR;

namespace EzDieter.Logic
{
    public static class GetUserById
    {
        public record Query(Guid? Id) : IRequest<Response>;
        
        public class Handler : IRequestHandler<Query,Response>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetById(request.Id);
                return user == null ? null : new Response(user);
            }
        }
        
        public record Response(User User);
        
    }
}