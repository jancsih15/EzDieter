using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Domain;
using MediatR;

namespace EzDieter.Logic.DishServices
{
    public static class UpdateDishCommand
    {
        public record Command(Dish Dish) : IRequest<Response>;

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IDishRepository _dishRepository;

            public Handler(IDishRepository dishRepository)
            {
                _dishRepository = dishRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                await _dishRepository.Update(request.Dish);
                return new Response(await _dishRepository.GetById(request.Dish.Id));
            }
        }

        public record Response(Dish Dish);
    }
}