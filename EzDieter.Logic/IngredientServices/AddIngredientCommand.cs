using System;
using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Domain;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace EzDieter.Logic.IngredientServices
{
    public static class AddIngredientCommand
    {
        public record Command(
            string Name,
            float Calorie,
            float Carbohydrate,
            float Fat,
            float Protein,
            string VolumeType,
            float Volume) : IRequest<Response>;

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IIngredientRepository _ingredientRepository;

            public Handler(IIngredientRepository ingredientRepository)
            {
                _ingredientRepository = ingredientRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.Name.IsNullOrEmpty())
                    return new Response(null, false, "Name can't be empty!");
                if (request.Volume == 0)
                    return new Response(null, false, "Volume cannot be 0!");
                var ingredient = new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Calorie = request.Calorie,
                    Carbohydrate = request.Carbohydrate,
                    Fat = request.Fat,
                    Protein = request.Protein,
                    VolumeType = request.VolumeType,
                    Volume = request.Volume
                };
                await _ingredientRepository.Add(ingredient);
                return new Response(ingredient, true, "");
            }
        }

        public record Response(Ingredient? Ingredient, bool Success, string Message);
    }
}