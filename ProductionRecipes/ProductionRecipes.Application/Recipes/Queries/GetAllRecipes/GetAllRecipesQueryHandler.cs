using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Contracts;
using ProductionRecipes.Domain.Entities.Recipe;

namespace ProductionRecipes.Application.Recipes.Queries.GetAllRecipes
{
    public class GetAllRecipesQueryHandler
        : IQueryHandler<GetAllRecipesQuery, IEnumerable<Recipe>>
    {
        private readonly IRecipeRepository _recipeRepository;

        public GetAllRecipesQueryHandler(
            IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public Task<IEnumerable<Recipe>> Handle(GetAllRecipesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_recipeRepository.GetAllRecipes());
        }

    }
}
