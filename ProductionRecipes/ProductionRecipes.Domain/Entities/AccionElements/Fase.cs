using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Domain.ValueObjects;
using ProductionRecipes.Domain.ValueObjects.ControlActions;

namespace ProductionRecipes.Domain.Entities.AccionElements.Fases
{
    /// <summary>
    /// Modela la fase
    /// </summary>
    public class Fase : AccionElement
    {
        #region Properties

        /// <summary>
        /// Duracion de la fase
        /// </summary>
        public int Duration { get; set; }
        

        /// <summary>
        /// Lista de acciones de control de la fase
        /// </summary>
        public List<ControlAction> ActionsList { get; set; }

        #endregion

        /// <summary>
        /// Requerido por EntityFramework
        /// </summary>
        protected Fase() { }
        

        /// <summary>
        /// inicializa una fase
        /// </summary>
        /// <param name="actionsList"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="duration"></param>
        public Fase(List<ControlAction> actionsList, string name, string description, Guid id) : base(name, description, id)
        {
            ActionsList = actionsList;
        }
       
    }
}