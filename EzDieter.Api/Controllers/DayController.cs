﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EzDieter.Api.Helpers;
using EzDieter.Domain;
using EzDieter.Logic.DayServices;
using EzDieter.Logic.UserServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EzDieter.Api.Controllers
{    
    [Helpers.Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DayController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DayController(ISender sender, IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var user = (User)HttpContext.Items["User"];
            var response = await _mediator.Send(new GetDaysQuery.Query(user.Id));
            return Ok(response);
        }
        
        [HttpGet]
        [Route("GetByDate")]
        public async Task<IActionResult> GetByDate(string dateTime)
        {
            var user = (User)HttpContext.Items["User"];
            var response = await _mediator.Send(new GetDayByDateQuery.Query(user.Id, dateTime));
            if (response.Success)
            {
                return Ok(response.Day);
            }

            return NotFound(response);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(DayHelper day)
        {
            var user = (User)HttpContext.Items["User"];
            var response = await _mediator.Send(new AddDayCommand.Command(
                DateTime.Parse(day.Date), 
                user,
                day.DayDishes,
                day.Calorie,
                day.Carbohydrate,
                day.Fat,
                day.Protein,
                day.Weight
            ));
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok(response.Id);
        }
        
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(DayHelper2 day)
        {
            var user = (User)HttpContext.Items["User"];
            day.Id = user.Id;
            var dayData = new Day{
                Id = day.Id,
                UserId = user.Id,
                Date = DateTime.Parse(day.Date),
                DayDishes = day.DayDishes,
                Calorie = day.Calorie,
                Carbohydrate = day.Carbohydrate,
                Fat = day.Fat,
                Protein = day.Protein,
                Weight = day.Weight
            };
            var response = await _mediator.Send(new UpdateDayCommand.Command(dayData));
            return response.Day is null ? NotFound("The updated day wasn't found!") : Ok(response);
        }
        
        [HttpDelete]
        [Route("Delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _mediator.Send(new DeleteDayCommand.Command(id));
            if (response.Id == null)
                return NotFound("The day wasn't found!");
            return Ok(response.Id);
        }
    }
    
    

    public class DayHelper
    {
        public string Date { get; set; }
        public List<DayDish> DayDishes { get; set; }
        public float Calorie { get; set; }

        public float Carbohydrate { get; set; }

        public float Fat { get; set; }

        public float Protein { get; set; }

        public float Weight { get; set; }
    }
    public class DayHelper2
    {
        public Guid Id { get; set; }
        public string Date { get; set; }
        public List<DayDish> DayDishes { get; set; }
        public float Calorie { get; set; }

        public float Carbohydrate { get; set; }

        public float Fat { get; set; }

        public float Protein { get; set; }

        public float Weight { get; set; }
    }
}