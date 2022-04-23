using System;
using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using MediatR;

namespace EzDieter.Logic.DayServices
{
    public static class DeleteDayCommand
    {
        public record Command(Guid Id) : IRequest<Response>;

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IDayRepository _dayRepository;

            public Handler(IDayRepository dayRepository)
            {
                _dayRepository = dayRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var day = await _dayRepository.GetById(request.Id);
                if (!day)
                {
                    return new Response(null, "The requested day wasn't found!");
                }
                await _dayRepository.Delete(request.Id);
                return new Response(request.Id, "The requested day was successfully deleted!");
            }
        }

        public record Response(Guid? Id, string Message);
    }
}