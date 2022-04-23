using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Domain;
using MediatR;

namespace EzDieter.Logic.DishServices
{
    public static class GetDishByNameQuery
    {
        public record Query(string Name) : IRequest<Response>;

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly IDishRepository _dishRepository;

            public Handler(IDishRepository dishRepository)
            {
                _dishRepository = dishRepository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var response = await _dishRepository.GetByName(request.Name);
                return new Response(response);
            }
        }

        public record Response(IEnumerable<Dish> Dishes);
    }
}