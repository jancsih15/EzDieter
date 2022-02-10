using System;
using System.Collections;
using System.Threading.Tasks;
using EzDieter.Api.Helpers;
using EzDieter.Domain;
using EzDieter.Logic.IngredientServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EzDieter.Api.Controllers
{
    [Helpers.Authorize]
    [ApiController]
    [Route("[controller]")]
    public class IngredientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IngredientController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // While testing they will be anonymous2

        [AllowAnonymous2]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllIngredients.Query());
            return Ok(response);

        }
        
        [AllowAnonymous2]
        [HttpGet]
        [Route("GetById/{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetIngredientById.Query(id));
            return response.Ingredient == null ? NotFound() : Ok(response);
        }

        [AllowAnonymous2]
        [HttpGet]
        [Route("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var response = await _mediator.Send(new GetIngredientByNameQuery.Query(name));
            return response.Ingredients.IsNullOrEmpty() ? NotFound() : Ok(response);
        }

        [AllowAnonymous2]
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(
            string name,
            float calorie,
            float carbohydrate,
            float fat,
            float protein,
            string volumeType,
            float volume
        )
        {
            var response = await _mediator.Send(new AddIngredientCommand.Command(
                name,
                calorie,
                carbohydrate,
                fat,
                protein,
                volumeType,
                volume
                ));
            return Ok(response);
        }

        
        // TODO check update if data already deleted
        [AllowAnonymous2]
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Ingredient ingredient)
        {
            var response = await _mediator.Send(new UpdateIngredientCommand.Command(ingredient));
            return response.Ingredient is null ? NotFound("The updated ingredient wasn't found!") : Ok(response);
        }

        [AllowAnonymous2]
        [HttpDelete]
        [Route("Delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _mediator.Send(new DeleteIngredientCommand.Command(id));
            //if (response.Ingredient == null)
            //    NotFound("There ingredient wasn't found!");
            // TODO why can't i check for null
            return Ok(response.Ingredient);
        }
    }
}