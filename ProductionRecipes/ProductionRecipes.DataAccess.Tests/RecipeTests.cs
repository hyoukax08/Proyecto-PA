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
        public void Can_Create_Product(Product producttomake)
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
        [DataRow()]
        public void Can_Get_Id_Of_Product_To_Make(int position)
        {
            // Arrange
            var recipes = _accionElementRepository.GetAllRecipes().ToList();
            Assert.IsNotNull(recipes);
            Assert.IsTrue(position < recipes.Count);
            Recipe recipeToGetProductIdFrom = recipes[position];

            // Execute
            Recipe? loadedRecipe = _accionElementRepository.GetRecipeById(recipeToGetProductIdFrom.Id);
            Guid? productid = loadedRecipe.ProductId

            // Assert
            Assert.IsNotNull(productid);
        }
    }



    }