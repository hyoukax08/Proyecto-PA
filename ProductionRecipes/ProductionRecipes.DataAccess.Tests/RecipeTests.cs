using ProductionRecipes.Domain.Types;
using ProductionRecipes.Contracts;
using ProductionRecipes.DataAccess.Contexts;
using ProductionRecipes.DataAccess.Repositories.Recipes;
using ProductionRecipes.Domain.Entities.Recipe;
using ProductionRecipes.Domain.Entities.Products;
using ProductionRecipes.Domain.Entities.AccionElements.Fases;
using ProductionRecipes.Domain.Entities.AccionElements.Operations;
using ProductionRecipes.Domain.ValueObjects.ControlActions;
using ProductionRecipes.DataAccess.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void Can_Create_Recipe(Product producttomake, List<Operation> execOperation)
        {
            // Arrange
            Guid id = Guid.NewGuid();
            List<Operation> listoperation1 = new List<Operation>();
            Product product1 = new Product("prueba", id);
            Recipe recipe1 = new(product1, listoperation1, id);

            // Execute
            _recipeRepository.AddRecipe(recipe1);
            _unitOfWork.SaveChanges();

            // Assert
            Recipe? loadedRecipe = _recipeRepository.GetRecipeById<Recipe>(id);
            Assert.IsNotNull(loadedRecipe);
        }

        [DataRow(1)]
        [TestMethod]
        public void Can_Get_Recipe_By_Id(int position)
        {
            // Arrange
            var recipes = _recipeRepository.GetAllRecipes<Recipe>().ToList();
            Assert.IsNotNull(recipes);
            Assert.IsTrue(position < recipes.Count);
            Recipe recipeToGet = recipes[position];

            // Execute
            Recipe? loadedRecipe = _recipeRepository.GetRecipeById<Recipe>(recipeToGet.Id);

            // Assert
            Assert.IsNotNull(loadedRecipe);
        }
        
        public void Cannot_Get_Recipe_By_Invalid_Id()
        {
            // Arrange

            // Execute
            Recipe? loadedRecipe = _recipeRepository.GetRecipeById<Recipe>(Guid.Empty);

            // Assert
            Assert.IsNull(loadedRecipe);
        }

        public void Can_Update_Recipe(Product producttomake, List<Operation> execOperation)
        {
            // Arrange
            var recipes = _recipeRepository.GetAllRecipes<Recipe>().ToList();
            Assert.IsNotNull(recipes);
            Assert.IsTrue(position < recipes.Count);
            Recipe recipeToUpdate = recipes[position];

            // Execute
            recipeToUpdate.UnityName = unityname;
            _recipeRepository.UpdateRecipe(recipeToUpdate);
            _unitOfWork.SaveChanges();

            // Assert
            Recipe? loadedRecipe = _recipeRepository.GetRecipeById<Recipe>(recipeToUpdate.Id);
            Assert.IsNotNull(loadedRecipe);
            Assert.AreEqual(loadedRecipe.UnityName, unityname);
        }

        [DataRow(0)]
        [TestMethod]
        public void Can_Delete_Recipe(int position)
        {
            // Arrange
            var recipes = _recipeRepository.GetAllRecipes<Recipe>().ToList();
            Assert.IsNotNull(recipes);
            Assert.IsTrue(position < recipes.Count);
            Recipe recipeToDelete = recipes[position];

            // Execute
            _recipeRepository.DeleteRecipe(recipeToDelete);
            _unitOfWork.SaveChanges();

            // Assert
            Recipe? loadedRecipe = _recipeRepository.GetRecipeById<Recipe>(recipeToDelete.Id);
            Assert.IsNull(loadedRecipe);
        }

    }

}