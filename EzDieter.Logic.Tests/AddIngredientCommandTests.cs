using System;
using System.Threading;
using System.Threading.Tasks;
using EzDieter.Database;
using EzDieter.Domain;
using EzDieter.Logic.IngredientServices;
using Moq;
using Xunit;

namespace EzDieter.Logic.Tests
{
    public class AddIngredientCommandTests
    {
        [Fact]
        public async Task Handle_WhenCalledWithCorrectRequest_CallsIngredientRepositoryWithProperInput()
        {
            var request = new AddIngredientCommand.Command("name",1, 2, 3, 4, "gram", 5);
            var ingredientRepositoryMock = new Mock<IIngredientRepository>();
            
            //system under test
            var sut = new AddIngredientCommand.Handler(ingredientRepositoryMock.Object);
            await sut.Handle(request, CancellationToken.None);

            ingredientRepositoryMock
                .Verify(ir => ir.Add(
                    It.Is<Ingredient>(x => IngredientsAreEqual(x, request))),
                    Times.Once());
        }

        [Fact]
        public async Task Handle_WhenCalledWithoutName_ShouldReturnFalseResponse()
        {
            var request = new AddIngredientCommand.Command("",1, 2, 3, 4, "gram", 5);
            var ingredientRepositoryMock = new Mock<IIngredientRepository>();
            
            var sut = new AddIngredientCommand.Handler(ingredientRepositoryMock.Object);
            var result = await sut.Handle(request, CancellationToken.None);
            
            ingredientRepositoryMock
                .Verify(x => x.Add(
                    It.IsAny<Ingredient>()),
                    Times.Never);
            
            Assert.False(result.Success);
            Assert.Equal("Name can't be empty!", result.Message);
        }

        

        private static bool IngredientsAreEqual(Ingredient x, AddIngredientCommand.Command request)
        {
            return x.Name == request.Name
                   && Math.Abs(x.Calorie - request.Calorie) < float.Epsilon
                   && Math.Abs(x.Carbohydrate - request.Carbohydrate) < float.Epsilon
                   && Math.Abs(x.Fat - request.Fat) < float.Epsilon
                   && Math.Abs(x.Protein - request.Protein) < float.Epsilon
                   && Math.Abs(x.Volume - request.Volume) < float.Epsilon
                   && x.VolumeType == request.VolumeType
                   && x.Id != Guid.Empty;
        }
    }
}