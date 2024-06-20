using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Domain.Entities.AccionElements.Operations;
using ProductionRecipes.Domain.Entities.Products;
using ProductionRecipes.Domain.Common;

namespace ProductionRecipes.Domain.Entities.Recipe
{
    /// <summary>
    /// modela la receta
    /// </summary>
    public class Recipe : Entity
    {
        #region Properties
        /// <summary>
        /// fecha de creation de la receta
        /// </summary>
        public DateTime CreationDate { get; }
        /// <summary>
        /// Fecha de Validacion de la Receta
        /// </summary>
        public DateTime ValidationDate { get; set; }

        /// <summary>
        /// Nombre del Experto que realizo la validacion
        /// </summary>
        public string? Expertname { get; set; }

        /// <summary>
        /// producto a fabricar
        /// </summary>
        public Product ProductToMake { get; set; }

        public Guid ProductId { get; set; }
        #endregion

        /// <summary>
        /// lista de operaciones a ejecutar en un proceso
        /// </summary>
        public List<Operation> ExecOperation { get; set; }

        /// <summary>
        /// Inicializa la Receta de Produccion
        /// </summary>
        /// <param name="creationDate"></param>
        /// <param name="execOperation"></param>
        /// <param name="execFases"></param>
        public Recipe(DateTime creationDate, Product producttomake, List<Operation> execOperation)
        {
            CreationDate = DateTime.Now;
            ProductToMake = producttomake;
            ExecOperation = execOperation;
        }
    }
}