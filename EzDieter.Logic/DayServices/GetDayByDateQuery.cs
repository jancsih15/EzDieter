using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Domain;
using MediatR;

namespace EzDieter.Logic.DayServices
{
    public static class GetDayByDateQuery
    {
        public record Query(Guid UserId, string Date) : IRequest<Response>;

        public class Handler : IRequestHandler<Query,Response>
        {
            private readonly IDayRepository _dayRepository;

            public Handler(IDayRepository dayRepository)
            {
                _dayRepository = dayRepository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var day = await _dayRepository.GetByDate(request.UserId, DateTime.Parse(request.Date + "T00:00:00Z"));
                if (day is not null)
                {
                    return new Response(day, true, "");
                }

                return new Response(null, false, "The given day wasn't found!");
            }
        }
        
        public record Response(Day Day, bool Success, string Message);
    }
}