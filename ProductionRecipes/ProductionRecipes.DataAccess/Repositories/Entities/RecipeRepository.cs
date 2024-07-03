using ProductionRecipes.Contracts;
using ProductionRecipes.DataAccess.Contexts;
using ProductionRecipes.DataAccess.Repositories.Common;
using ProductionRecipes.Domain.Entities.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionRecipes.DataAccess.Repositories.Recipes
{
    /// <summary>
    /// Implementación del repositorio <see cref="IRecipeRepository"/>.
    /// </summary>
    public class RecipeRepository
        : RepositoryBase, IRecipeRepository
    {
        public RecipeRepository(ApplicationContext context) : base(context)
        {
        }

        public void AddRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
        }

        public void DeleteRecipe(Recipe recipe)
        {
            _context.Recipes.Remove(recipe);
        }
        
        public IEnumerable<Recipe> GetAllRecipes()
        {
            return _context.Recipes.ToList();
        }

        public Recipe? GetRecipeById(Guid id)
        {
            return _context.Recipes.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateRecipe(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
        }

        public Guid? GetIdOfProductToMake(Guid id)
        {
            Recipe? recipe1 = _context.Recipes.FirstOrDefault(x => x.Id == id);
            if (recipe1 == null)
            {
                Console.WriteLine("La receta no existe)");
                return null;
            }
            return recipe1.ProductId;
        }
    }
}