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
        public void Can_Create_Recipe()
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
            Recipe? loadedRecipe = _recipeRepository.GetRecipeById(id);
            Assert.IsNotNull(loadedRecipe);
        }

        [DataRow(0)]
        [TestMethod]
        public void Can_Get_Recipe_By_Id(int position)
        {
            // Arrange
            var recipes = _recipeRepository.GetAllRecipes().ToList();
            Assert.IsNotNull(recipes);
            Assert.IsTrue(position < recipes.Count);
            Recipe recipeToGet = recipes[position];

            // Execute
            Recipe? loadedRecipe = _recipeRepository.GetRecipeById(recipeToGet.Id);

            // Assert
            Assert.IsNotNull(loadedRecipe);
        }
        
        public void Cannot_Get_Recipe_By_Invalid_Id()
        {
            // Arrange

            // Execute
            Recipe? loadedRecipe = _recipeRepository.GetRecipeById(Guid.Empty);

            // Assert
            Assert.IsNull(loadedRecipe);
        }
        
        [DataRow(0, "Ing. Luis Alejandro Perez Vazquez", "10/11/2024")]
        [TestMethod]
        public void Can_Validate_Recipe(int position, string expert, string validationDateString)
        {
            // Arrange
            DateTime validationDateTime = DateTime.Parse(validationDateString);
            var recipes = _recipeRepository.GetAllRecipes().ToList();
            Assert.IsNotNull(recipes);
            Assert.IsTrue(position < recipes.Count);
            Recipe recipeToValidate = recipes[position];

            // Execute
            _recipeRepository.ValidateRecipe(recipeToValidate, expert, validationDateTime);
            _unitOfWork.SaveChanges();

            // Assert
            Recipe? loadedRecipe = _recipeRepository.GetRecipeById(recipeToValidate.Id);
            Assert.IsNotNull(loadedRecipe);
            Assert.AreEqual(loadedRecipe.Expertname , expert);
            Assert.AreEqual(loadedRecipe.ValidationDate, validationDateTime);
        }

        [DataRow(0)]
        [TestMethod]
        public void Can_Delete_Recipe(int position)
        {
            // Arrange
            var recipes = _recipeRepository.GetAllRecipes().ToList();
            Assert.IsNotNull(recipes);
            Assert.IsTrue(position < recipes.Count);
            Recipe recipeToDelete = recipes[position];

            // Execute
            _recipeRepository.DeleteRecipe(recipeToDelete);
            _unitOfWork.SaveChanges();

            // Assert
            Recipe? loadedRecipe = _recipeRepository.GetRecipeById(recipeToDelete.Id);
            Assert.IsNull(loadedRecipe);
        }

        [DataRow(0)]
        [TestMethod]
        public void Can_Get_ProductId_From_Recipe(int position)
        {
            // Arrange
            var recipes = _recipeRepository.GetAllRecipes().ToList();
            Assert.IsNotNull(recipes);
            Assert.IsTrue(position < recipes.Count);
            Recipe recipeToGetProductIdFrom = recipes[position];

            // Execute
            Guid? ProductIdObtained = _recipeRepository.GetIdOfProductToMake(recipeToGetProductIdFrom.Id);

            // Assert
            Assert.IsNotNull(ProductIdObtained);
            Assert.AreEqual(recipeToGetProductIdFrom.ProductId, ProductIdObtained);
        }
    }

}