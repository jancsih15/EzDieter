using System;
using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Domain;
using MediatR;

namespace EzDieter.Logic.IngredientServices
{
    public static class UpdateIngredientCommand
    {
        public record Command(Ingredient Ingredient) : IRequest<Response>;

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IIngredientRepository _ingredientRepository;

            public Handler(IIngredientRepository ingredientRepository)
            {
                _ingredientRepository = ingredientRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                await _ingredientRepository.Update(request.Ingredient);
                return new Response(await _ingredientRepository.GetById(request.Ingredient.Id));
            }
        }

        public record Response(Ingredient Ingredient);
    }
}