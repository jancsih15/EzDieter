using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Domain;
using MediatR;

namespace EzDieter.Logic.DayServices
{
    public static class UpdateDayCommand
    {
        public record Command(Day Day) : IRequest<Response>;

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IDayRepository _dayRepository;

            public Handler(IDayRepository dayRepository)
            {
                _dayRepository = dayRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var day = await _dayRepository.GetById(request.Day.Id);
                if (!day)
                {
                    return new Response(request.Day, "The requested day wasn't found!");
                }
                await _dayRepository.Update(request.Day);
                return new Response(request.Day, "The requested day successfully updated!");
            }
        }

        public record Response(Day Day, string Message);
    }
}