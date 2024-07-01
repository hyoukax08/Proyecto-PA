using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Domain.Entities.AccionElements.Fases;

namespace ProductionRecipes.Domain.Entities.AccionElements.Operations
{
    /// <summary>
    /// Modela la operacion
    /// </summary>
    public class Operation : AccionElement
    {

        #region Properties

        /// <summary>
        /// Nombre de la unidad sobre la que actua
        /// </summary>
        public string? UnityName { get; set; }
    
        /// <summary>
        /// Fases a ejecutar en un proceso productivo
        /// </summary>
        public List<Fase> ExecFases { get; set; }
        #endregion

        /// <summary>
        /// Requerido por EntityFramework
        /// </summary>
        protected Operation() { }

        /// <summary>
        /// Incializa la operacion a ejecutar
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="unityName"></param>
        /// <param name="execFases"></param>
        public Operation(List<Fase> execFases, string name, string description, Guid id) : base(name, description, id)
        {
            ExecFases = execFases;
        }
    }
}