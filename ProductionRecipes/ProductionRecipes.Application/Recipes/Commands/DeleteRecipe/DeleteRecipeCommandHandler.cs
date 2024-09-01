using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Contracts;

namespace ProductionRecipes.Application.Recipes.Commands.DeleteRecipe
{
    public class DeleteRecipeCommandHandler
    : ICommandHandler<DeleteRecipeCommand>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRecipeCommandHandler(
            IRecipeRepository recipeRepository,
            IUnitOfWork unitOfWork)
        {
            _recipeRepository = recipeRepository;
            _unitOfWork = unitOfWork;
        }

        public Task Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipeToDelete = _recipeRepository.GetRecipeById(request.Id);
            if (recipeToDelete is null)
                return Task.CompletedTask;
            _recipeRepository.DeleteRecipe(recipeToDelete);
            _unitOfWork.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
