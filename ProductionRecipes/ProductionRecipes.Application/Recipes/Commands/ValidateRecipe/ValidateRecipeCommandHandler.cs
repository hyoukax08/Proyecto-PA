using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Contracts;
using ProductionRecipes.Domain.Entities.Recipe;

namespace ProductionRecipes.Application.Recipes.Commands.ValidateRecipe
{
    public class ValidateRecipeCommandHandler
   : ICommandHandler<ValidateRecipeCommand>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ValidateRecipeCommandHandler(
            IRecipeRepository recipeRepository,
            IUnitOfWork unitOfWork)
        {
            _recipeRepository = recipeRepository;
            _unitOfWork = unitOfWork;
        }

        public Task Handle(ValidateRecipeCommand request, CancellationToken cancellationToken)
        {
            _recipeRepository.ValidateRecipe(request.Recipe, request.ExpertName, request.ValidationDate);
            _unitOfWork.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
