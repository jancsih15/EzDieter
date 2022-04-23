using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Domain;
using MediatR;

namespace EzDieter.Logic.MealServices
{
    public static class GetAllDishesQuery
    {
        public record Query() : IRequest<Response>;

        public class Handler : IRequestHandler<Query,Response>
        {
            private readonly IDishRepository _dishRepository;

            public Handler(IDishRepository dishRepository)
            {
                _dishRepository = dishRepository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var dishes = await _dishRepository.GetAll();
                return new Response(dishes);
            }
        }
        
        public record Response(IEnumerable<Dish?> Dishes);
    }
}