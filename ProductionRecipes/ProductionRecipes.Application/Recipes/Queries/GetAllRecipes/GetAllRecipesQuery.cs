using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Domain.Entities.Recipe;

namespace ProductionRecipes.Application.Recipes.Queries.GetAllRecipes
{
    public record GetAllRecipesQuery : IQuery<IEnumerable<Recipe>>;
}
