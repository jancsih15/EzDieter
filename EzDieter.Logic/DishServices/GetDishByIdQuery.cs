using System;
using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Domain;
using EzDieter.Logic.IngredientServices;
using MediatR;

namespace EzDieter.Logic.MealServices
{
    public static class GetDishByIdQuery
    {
        public record Query(Guid Id) : IRequest<Response>;

        public class Handler : IRequestHandler<Query,Response>
        {
            private readonly IDishRepository _dishRepository;

            public Handler(IDishRepository dishRepository)
            {
                _dishRepository = dishRepository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var meal = await _dishRepository.GetById(request.Id);
                return new Response(meal);
            }
        }
        
        public record Response(Dish Dish);
    }
}