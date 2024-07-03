using ProductionRecipes.Domain.Entities;
using ProductionRecipes.Domain.Entities.Types;
using ProductionRecipes.Domain.Entities.AcccionElements;
using ProductionRecipes.DataAccess.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System;

namespace ProductionRecipes.DataAccess.Tests
{
    [TestClass]
    public class RecipeTests
    {

        private IRecipeRepository _recipeRepository;
        private IUnitOfWork _unitOfWork;

        public RecipeTests()
        {
            ApplicationContext context =
            new ApplicationContext(ConnectionStringProvider.GetConnectionString());
            _recipeRepository = new RecipeRepository(context);
            _unitOfWork = new UnitOfWork(context);
        }

        [TestMethod]
        [DataRow(?)]
        [DataRow(?)]
        public void Can_Create_Product(Product producttomake, List<Operation> execOperation)
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Recipe recipe = new Recipe(producttomake, execOperation, id);

            // Execute
            _recipeRepository.AddRecipe(recipe);
            _unitOfWork.SaveChanges();

            // Assert
            Recipe? loadedRecipe = _recipeRepository.GetProductById<Recipe>(id);
            Assert.IsNotNull(loadedRecipe);
        }





    }