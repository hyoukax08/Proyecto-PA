using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Domain.Entities.Recipe;

namespace ProductionRecipes.Application.Recipes.Commands.ValidateRecipe
{
    public record ValidateRecipeCommand(Recipe Recipe, string ExpertName, DateTime ValidationDate) : ICommand;
}
