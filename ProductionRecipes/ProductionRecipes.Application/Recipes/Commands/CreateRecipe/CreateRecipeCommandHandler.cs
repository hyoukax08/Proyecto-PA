using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Domain.Entities.Recipe;
using ProductionRecipes.Domain.Entities.Products;
using ProductionRecipes.Domain.Entities.AccionElements.Operations;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Contracts;


namespace ProductionRecipes.Application.Recipes.Commands.CreateRecipe
{
    public class CreateRecipeCommandHandler
     : ICommandHandler<CreateRecipeCommand, Recipe>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRecipeCommandHandler(
            IRecipeRepository recipeRepository,
            IProductRepository productRepository,
            IUnitOfWork unitOfWork)
        {
            _recipeRepository = recipeRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Recipe> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            Product temp = _productRepository.GetProductById(request.Producttomake.Id);
            Recipe result = new Recipe(
                temp,
                Guid.NewGuid());

            _recipeRepository.AddRecipe(result);
            _unitOfWork.SaveChanges();

            return Task.FromResult(result);
        }
    }
}
