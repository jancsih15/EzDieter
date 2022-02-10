using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Domain;
using MediatR;

namespace EzDieter.Logic.IngredientServices
{
    public static class GetIngredientByNameQuery
    {
        public record Query(string Name) : IRequest<Response>;

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly IIngredientRepository _ingredientRepository;

            public Handler(IIngredientRepository ingredientRepository)
            {
                _ingredientRepository = ingredientRepository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var response = await _ingredientRepository.GetByName(request.Name);
                return new Response(response);
            }
        }

        public record Response(IEnumerable<Ingredient> Ingredients);
    }
}