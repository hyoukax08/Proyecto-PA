using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Domain.Entities.Recipe;

namespace ProductionRecipes.Contracts
{
    public interface IRecipeRepository
    {
        /// <summary>
        /// Adiciona una receta al soporte de datos
        /// </summary>
        /// <param name="recipe"></param>
        void AddRecipe(Recipe recipe);

        /// <summary>
        /// Obtiene una receta del soporte de datos por su id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T? GetRecipeById<T>(Guid id) where T : Recipe;
    
        /// <summary>
        /// Obtiene todas las recetas del soporte de datos.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetAllRecipes<T>() where T : Recipe;

        /// <summary>
        /// Obtiene el id del producto a fabricar de una receta del soporte de datos
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        Guid? GetIdOfProductToMake(Guid Id);

        /// <summary>
        /// Actualiza el valor de una receta
        /// </summary>
        /// <param name="recipe"></param>
        void UpdateRecipe(Recipe recipe);

        /// <summary>
        /// Elimina un producto
        /// </summary>
        /// <param name="recipe"></param>
        void DeleteRecipe(Recipe recipe);
    }
}
