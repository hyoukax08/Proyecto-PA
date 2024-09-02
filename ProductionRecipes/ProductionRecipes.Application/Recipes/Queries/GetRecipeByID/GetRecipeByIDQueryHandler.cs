using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Contracts;
using ProductionRecipes.Domain.Entities.Recipe;

namespace ProductionRecipes.Application.Recipes.Queries.GetRecipeByID
{
    public class GetRecipeByIDQueryHandler : IQueryHandler<GetRecipeByIDQuery, Recipe?>
    {
        private readonly IRecipeRepository _recipeRepository;

        public GetRecipeByIDQueryHandler(
            IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public Task<Recipe?> Handle(GetRecipeByIDQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_recipeRepository.GetRecipeById(request.ID));
        }
    }
}
