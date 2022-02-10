using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Domain;
using MediatR;

namespace EzDieter.Logic.IngredientServices
{
    public static class GetAllIngredients
    {
        public record Query() : IRequest<Response>;

        public class Handler : IRequestHandler<Query,Response>
        {
            private readonly IIngredientRepository _ingredientRepository;

            public Handler(IIngredientRepository ingredientRepository)
            {
                _ingredientRepository = ingredientRepository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var ingredients = await _ingredientRepository.GetAll();
                return new Response(ingredients);
            }
        }
        
        public record Response(IEnumerable<Ingredient> Ingredients);
    }
}