using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Contracts;
using ProductionRecipes.Domain.Entities.Recipe;

namespace ProductionRecipes.Application.Recipes.Commands.UpdateRecipe
{
    public class UpdateRecipeCommandHandler
   : ICommandHandler<UpdateRecipeCommand>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRecipeCommandHandler(
            IRecipeRepository recipeRepository,
            IUnitOfWork unitOfWork)
        {
            _recipeRepository = recipeRepository;
            _unitOfWork = unitOfWork;
        }

        public Task Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            _recipeRepository.UpdateRecipe(request.Recipe);
            _unitOfWork.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
