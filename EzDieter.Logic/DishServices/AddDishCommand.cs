using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Domain;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace EzDieter.Logic.MealServices
{
    public static class AddDishCommand
    {
        public record Command(
            string Name,
            List<DishIngredient> MealIngs,
            float Calorie,
            float Carbohydrate,
            float Fat,
            float Protein,
            string VolumeType,
            float Volume) : IRequest<Response>;

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IDishRepository _dishRepository;

            public Handler(IDishRepository dishRepository)
            {
                _dishRepository = dishRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.Name.IsNullOrEmpty())
                    return new Response(Guid.Empty, false, "Name can't be empty!");
                if (request.Volume == 0)
                    return new Response(Guid.Empty, false, "Volume cannot be 0!");
                var dish = new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    DishIngredients = request.MealIngs,
                    Calorie = request.Calorie,
                    Carbohydrate = request.Carbohydrate,
                    Fat = request.Fat,
                    Protein = request.Protein,
                    VolumeType = request.VolumeType,
                    Volume = request.Volume
                };
                await _dishRepository.Add(dish);
                return new Response(dish.Id, true, "");
            }
        }

        public record Response(Guid Id, bool Success, string Message);
    }
}