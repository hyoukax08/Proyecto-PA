using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Domain.Entities.ControlActions;

namespace ProductionRecipes.Domain.Entities.Fases
{
    /// <summary>
    /// Modela la fase
    /// </summary>
    public class Fase
    {
        #region Properties
        /// <summary>
        /// Nombre de la fase
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Descripcion de la fase
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Duracion de la fase
        /// </summary>
        public int Duration { get; }
        #endregion

        /// <summary>
        /// Lista de acciones de control de la fase
        /// </summary>
        public List<ControlAction> ActionsList { get; set; }

        /// <summary>
        /// inicializa una fase
        /// </summary>
        /// <param name="actionsList"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="duration"></param>
        public Fase(List<ControlAction> actionsList, string name, string description, int duration)
        {
            ActionsList = actionsList;
            Name = name;
            Description = description;
            Duration = duration;
        }
    }
}