using System;
using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Domain;
using EzDieter.Logic.UserServices;
using MediatR;

namespace EzDieter.Logic.IngredientServices
{
    public static class DeleteIngredientCommand
    {
        public record Command(Guid Id) : IRequest<Response>;

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IIngredientRepository _ingredientRepository;

            public Handler(IIngredientRepository ingredientRepository)
            {
                _ingredientRepository = ingredientRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var ingredient = await _ingredientRepository.GetById(request.Id);
                await _ingredientRepository.Delete(request.Id);
                return new Response(ingredient?.Id);
            }
        }

        public record Response(Guid? Id);
    }
}