using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EzDieter.Api.Helpers;
using EzDieter.Domain;
using EzDieter.Logic.IngredientServices;
using EzDieter.Logic.MealServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EzDieter.Api.Controllers
{
    [Helpers.Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DishController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DishController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // While testing they will be anonymous2

        [AllowAnonymous2]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllDishesQuery.Query());
            return Ok(response);

        }
        
        [AllowAnonymous2]
        [HttpGet]
        [Route("GetById/{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetDishByIdQuery.Query(id));
            return response.Dish == null ? NotFound() : Ok(response);
        }

        [AllowAnonymous2]
        [HttpGet]
        [Route("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var response = await _mediator.Send(new GetDishByNameQuery.Query(name));
            return response.Dishes.IsNullOrEmpty() ? NotFound() : Ok(response);
        }

        [AllowAnonymous2]
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(
            string name,
            List<DishIngredient> dishIngredients,
            float calorie,
            float carbohydrate,
            float fat,
            float protein,
            string volumeType,
            float volume
        )
        {
            var response = await _mediator.Send(new AddDishCommand.Command(
                name,
                dishIngredients,
                calorie,
                carbohydrate,
                fat,
                protein,
                volumeType,
                volume
            ));
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok(response.Id);
        }
        
        [AllowAnonymous2]
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Dish dish)
        {
            var response = await _mediator.Send(new UpdateDishCommand.Command(dish));
            return response.Dish is null ? NotFound("The updated ingredient wasn't found!") : Ok(response);
        }

        [AllowAnonymous2]
        [HttpDelete]
        [Route("Delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _mediator.Send(new DeleteDishCommand.Command(id));
            if (response.Id == null)
                return NotFound("The dish wasn't found!");
            return Ok(response.Id);
        }
    }
}