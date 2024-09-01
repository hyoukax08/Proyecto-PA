using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Domain.Entities.Recipe;
using ProductionRecipes.Domain.Entities.Products;
using ProductionRecipes.Domain.Entities.AccionElements.Operations;
using ProductionRecipes.Application.Abstract;

namespace ProductionRecipes.Application.Recipes.Commands.CreateRecipe
{
    public record CreateRecipeCommand(Product Producttomake, List<Operation> ExecOperation):ICommand<Recipe>;
}
