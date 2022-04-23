using System;
using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using MediatR;

namespace EzDieter.Logic.MealServices
{
    public static class DeleteDishCommand
    {
        public record Command(Guid Id) : IRequest<Response>;

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IDishRepository _dishRepository;

            public Handler(IDishRepository dishRepository)
            {
                _dishRepository = dishRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var meal = await _dishRepository.GetById(request.Id);
                await _dishRepository.Delete(request.Id);
                return new Response(meal.Id);
            }
        }

        public record Response(Guid? Id);
    }
}