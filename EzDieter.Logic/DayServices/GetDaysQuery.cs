using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Domain;
using MediatR;

namespace EzDieter.Logic.DayServices
{
    public static class GetDaysQuery
    {
        public record Query(Guid UserId) : IRequest<Response>;

        public class Handler : IRequestHandler<Query,Response>
        {
            private readonly IDayRepository _dayRepository;

            public Handler(IDayRepository dayRepository)
            {
                _dayRepository = dayRepository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var days = await _dayRepository.GetAll(request.UserId);
                return new Response(days);
            }
        }
        
        public record Response(IEnumerable<Day> Days);
    }
}