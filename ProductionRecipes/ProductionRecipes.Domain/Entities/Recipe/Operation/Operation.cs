using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Domain.Entities.Fases;

namespace ProductionRecipes.Domain.Entities.Operations
{
    /// <summary>
    /// Modela la operacion
    /// </summary>
    public class Operation
    {
       
        #region Properties
        /// <summary>
        /// Nombre de la Operacion
        /// </summary>
        public string Name { get;}
           
        /// <summary>
        /// Descripcion de la operacion
        /// </summary>
        public string Description { get;}

        /// <summary>
        /// Nombre de la unidad sobre la que actua
        /// </summary>
        public string UnityName { get;}
        #endregion

        /// <summary>
        /// Fases a ejecutar en un proceso productivo
        /// </summary>
        public List<Fase> ExecFases { get; set; }

        /// <summary>
        /// Incializa la operacion a ejecutar
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="unityName"></param>
        /// <param name="execFases"></param>
        public Operation(string name, string description, string unityName, List<Fase> execFases)
        {
            Name = name;
            Description = description;
            UnityName = unityName;
            ExecFases = execFases;
        }

    }
}