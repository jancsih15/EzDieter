using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Domain;
using MediatR;

namespace EzDieter.Logic
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery,IEnumerable<User>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetAll();
        }
    }

    public record GetUsersQuery() : IRequest<IEnumerable<User>>;
}