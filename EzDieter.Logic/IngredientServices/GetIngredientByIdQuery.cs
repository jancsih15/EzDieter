using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Domain;
using MediatR;

namespace EzDieter.Logic.IngredientServices
{
    public static class GetIngredientByIdQuery
    {
        public record Query(Guid Id) : IRequest<Response>;

        public class Handler : IRequestHandler<Query,Response>
        {
            private readonly IIngredientRepository _ingredientRepository;

            public Handler(IIngredientRepository ingredientRepository)
            {
                _ingredientRepository = ingredientRepository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var ingredient = await _ingredientRepository.GetById(request.Id);
                return new Response(ingredient);
            }
        }
        
        public record Response(Ingredient? Ingredient);
    }
}