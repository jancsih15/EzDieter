using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Domain;
using MediatR;

namespace EzDieter.Logic.DayServices
{
    public static class AddDayCommand
    {
        public record Command(
            DateTime Date,
            User User,
            List<DayDish> DayMeals,
            float Calorie,
            float Carbohydrate,
            float Fat,
            float Protein,
            float Weight) : IRequest<Response>;

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IDayRepository _dayRepository;
            private readonly IUserRepository _userRepository;

            public Handler(IDayRepository dayRepository, IUserRepository userRepository)
            {
                _dayRepository = dayRepository;
                _userRepository = userRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                // validate if days doesn't already exists
                var days = await _dayRepository.GetAll(request.User.Id);
                if (days.Any(x => x.Date.Date == request.Date.Date))
                {
                    return new Response(null, false, $"The given date {request.Date.Date} already exists!");
                }
                
                // create new day
                var newDay = new Day
                {
                    Id = Guid.NewGuid(),
                    UserId = request.User.Id,
                    Date = request.Date,
                    DayDishes = request.DayMeals,
                    Calorie = request.Calorie,
                    Carbohydrate = request.Carbohydrate,
                    Fat = request.Fat,
                    Protein = request.Protein,
                    Weight = request.Weight
                };
                await _dayRepository.Add(newDay);
                
                // calculate TDEE from all data available
                var daysForTDEE = (await _dayRepository.GetAll(request.User.Id)).OrderBy(x => x.Date).OrderBy(x => x.Date);
                if (daysForTDEE.Any())
                {
                    float startWeight = daysForTDEE.First().Weight;
                    DateTime startDate = daysForTDEE.First().Date;
                    float endWeight = daysForTDEE.Last().Weight;
                    DateTime endDate = daysForTDEE.Last().Date;
                    float weightDif = endWeight - startWeight;

                    float sumCal = daysForTDEE.Sum(x => x.Calorie);
                    float avgCal = sumCal / daysForTDEE.Count();
                    float avgCalDif = (float) (weightDif * 7716.179176 / (endDate-startDate).TotalDays);

                    request.User.TDEEs = avgCal - avgCalDif;

                    await _userRepository.Update(request.User);
                }
                
                
                return new Response(newDay.Id, true, "");
            }
        }

        public record Response(Guid? Id, bool Success, string Message);
    }
}